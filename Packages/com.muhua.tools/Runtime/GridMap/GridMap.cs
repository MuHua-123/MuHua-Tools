using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap<Unit> where Unit : GridUnit {
    public Unit[,] array;
    private bool isYNormal;
    private int wide, high;
    private Vector3 originPosition;
    public GridMap(Vector3 originPosition, int wide, int high, Func<int, int, Unit> generate, bool isYNormal = false) {
        this.isYNormal = isYNormal;
        this.wide = wide;
        this.high = high;
        this.originPosition = originPosition;
        array = new Unit[wide, high];
        Loop((x, y) => { array[x, y] = generate(x, y); });
    }
    public void Loop(Action<int, int> action) {
        for (int y = 0; y < high; y++) {
            for (int x = 0; x < wide; x++) { action?.Invoke(x, y); }
        }
    }
    //世界坐标修正
    public Vector3 GetWorldPosition(Vector3 worldPosition) {
        GetXY(worldPosition, out int x, out int y);
        return GetWorldPosition(x, y);
    }
    public Vector3 GetWorldPosition(int x, int y) {
        float xOffset = wide % 2 == 1 ? 0.5f : 0;
        float yOffset = high % 2 == 1 ? 0.5f : 0;
        if (isYNormal) { return new Vector3(x + xOffset, 0, y + yOffset) + originPosition; }
        else { return new Vector3(x + xOffset, y + yOffset, 0) + originPosition; }
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y) {
        float xOffset = wide % 2 == 1 ? 0 : 0.5f;
        float yOffset = high % 2 == 1 ? 0 : 0.5f;
        x = Mathf.FloorToInt((worldPosition - originPosition).x + xOffset);
        if (isYNormal) { y = Mathf.FloorToInt((worldPosition - originPosition).z + yOffset); }
        else { y = Mathf.FloorToInt((worldPosition - originPosition).y + yOffset); }
    }
    //校验范围
    public bool TryWorldPosition(Vector3 worldPosition) {
        GetXY(worldPosition, out int x, out int y);
        return TryGetXY(x, y);
    }
    public bool TryGetXY(int x, int y) {
        return x >= 0 && x < wide && y >= 0 && y < high;
    }
    //单元操作
    public Unit GetMapUnit(Vector3 worldPosition) {
        GetXY(worldPosition, out int x, out int y);
        return GetMapUnit(x, y);
    }
    public Unit GetMapUnit(int x, int y) {
        x = Mathf.Clamp(x, 0, wide - 1);
        y = Mathf.Clamp(y, 0, high - 1);
        return array[x, y];
    }
    public void SetMapUnit(Vector3 worldPosition, Unit mapUnit) {
        GetXY(worldPosition, out int x, out int y);
        SetMapUnit(x, y, mapUnit);
    }
    public void SetMapUnit(int x, int y, Unit mapUnit) {
        x = Mathf.Clamp(x, 0, wide - 1);
        y = Mathf.Clamp(y, 0, high - 1);
        array[x, y] = mapUnit;
    }
    //校验单元
    public bool TryGetMapUnit(Vector3 worldPosition, out Unit unit) {
        GetXY(worldPosition, out int x, out int y);
        return TryGetMapUnit(x, y, out unit);
    }
    public bool TryGetMapUnit(int x, int y, out Unit unit) {
        unit = GetMapUnit(x, y);
        return TryGetXY(x, y);
    }
    public bool TrySetMapUnit(Vector3 worldPosition, Unit mapUnit) {
        GetXY(worldPosition, out int x, out int y);
        return TrySetMapUnit(x, y, mapUnit);
    }
    public bool TrySetMapUnit(int x, int y, Unit mapUnit) {
        if (TryGetXY(x, y)) { array[x, y] = mapUnit; return true; }
        else { return false; }
    }
    //获取邻近节点列表
    public List<Unit> GetNeighbourList(Unit currentNode) {
        List<Unit> neighbourList = new List<Unit>();

        if (currentNode.x - 1 >= 0) {
            //Left 左
            neighbourList.Add(array[currentNode.x - 1, currentNode.y]);
            //Left Down 左下
            if (currentNode.y - 1 >= 0) { neighbourList.Add(array[currentNode.x - 1, currentNode.y - 1]); }
            //Left Up 左上
            if (currentNode.y + 1 < array.GetLength(1)) { neighbourList.Add(array[currentNode.x - 1, currentNode.y + 1]); }
        }
        if (currentNode.x + 1 < array.GetLength(0)) {
            //Right 右
            neighbourList.Add(array[currentNode.x + 1, currentNode.y]);
            //Right Down 右下
            if (currentNode.y - 1 >= 0) { neighbourList.Add(array[currentNode.x + 1, currentNode.y - 1]); }
            //Right Up 右上
            if (currentNode.y + 1 < array.GetLength(1)) { neighbourList.Add(array[currentNode.x + 1, currentNode.y + 1]); }
        }
        //Down 下
        if (currentNode.y - 1 >= 0) { neighbourList.Add(array[currentNode.x, currentNode.y - 1]); }
        //Up 上
        if (currentNode.y + 1 < array.GetLength(1)) { neighbourList.Add(array[currentNode.x, currentNode.y + 1]); }

        return neighbourList;
    }
}
