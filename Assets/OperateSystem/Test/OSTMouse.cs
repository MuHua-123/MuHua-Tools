using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSTMouse : MonoBehaviour {
    public bool isOver;
    public OSMouseID mouse;
    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private void Awake() {
        OSControl.MouseOver(mouse).OnDown += OSTMouseLeft_OnDown;
        OSControl.MouseOver(mouse).OnPress += OSTMouseLeft_OnPress;
        OSControl.MouseScrollWheel(isOver).OnScroll += OSTMouse_OnScroll;
    }
    private void OnDestroy() {
        OSControl.MouseOver(mouse).OnDown -= OSTMouseLeft_OnDown;
        OSControl.MouseOver(mouse).OnPress -= OSTMouseLeft_OnPress;
        OSControl.MouseScrollWheel(isOver).OnScroll -= OSTMouse_OnScroll;
    }
    private void OSTMouseLeft_OnDown() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        originalPosition = transform.position;
    }
    private void OSTMouseLeft_OnPress() {
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = mousePosition - v3;
        Vector3 position = originalPosition - offset;
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 10);
    }
    private void OSTMouse_OnScroll(float obj) {
        float localScale = transform.localScale.x;
        float size = Mathf.Max(1, localScale + obj);
        transform.localScale = new Vector3(size, size, size);
    }
}
