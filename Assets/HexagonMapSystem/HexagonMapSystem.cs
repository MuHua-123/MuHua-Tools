using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class HexagonMapSystem : MonoBehaviour {
    public const float InnerDiam = 1f;
    public const float OuterDiam = InnerDiam * 1.154700538379252f;
    public const float InnerRadius = InnerDiam * 0.5f;
    public const float OuterRadius = OuterDiam * 0.5f;

    public int wide, high;
    public Transform prefab;
    public GameObject[,] array;
    private void Awake() {
        array = new GameObject[wide, high];
        Loop((x, y) => { array[x, y] = Generate(x, y); });
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0) && RayTool.GetMouseToWorldPosition(out Vector3 worldPosition)) {
            Vector2Int xy = GetXY(worldPosition);
            Debug.Log(xy);
        }
    }
    public void Loop(Action<int, int> action) {
        for (int y = 0; y < high; y++) {
            for (int x = 0; x < wide; x++) { action?.Invoke(x, y); }
        }
    }
    public GameObject Generate(int x, int y) {
        Transform temp = Instantiate(prefab, transform);
        temp.position = GetWorldPosition(x, y);
        temp.gameObject.SetActive(true);
        return temp.gameObject;
    }
    public Vector3 GetWorldPosition(int x, int y) {
        float offset = (y % 2) == 1 ? InnerRadius : 0;
        float xPosition = x * InnerDiam + offset;
        float zPosition = y * OuterDiam * 0.75f;
        return new Vector3(xPosition, 0, zPosition);
    }
    public Vector2Int GetXY(Vector3 worldPosition) {
        float offsetX = worldPosition.x / (InnerRadius * 2f);
        float offsetZ = worldPosition.z / (OuterRadius * 3f);
        float originalX = offsetX - offsetZ;
        float originalY = -offsetX - offsetZ;
        int iX = Mathf.RoundToInt(originalX);
        int iY = Mathf.RoundToInt(originalY);
        int iZ = Mathf.RoundToInt(-originalX - originalY);
        if (iX + iY + iZ != 0) {
            float differenceX = Mathf.Abs(originalX - iX);
            float differenceY = Mathf.Abs(originalY - iY);
            float differenceZ = Mathf.Abs(-originalX - originalY - iZ);
            if (differenceX > differenceY && differenceX > differenceZ) { iX = -iY - iZ; }
            else if (differenceZ > differenceY) { iZ = -iX - iY; }
        }
        int offset = iZ > 0 ? 0 : (iZ % 2);
        return new Vector2Int(iX + ((iZ + offset) / 2), iZ);
    }
}
