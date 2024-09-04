using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OSInputBehaviour : MonoBehaviour {
    private Dictionary<int, OSMouse> MouseDictionary => OSModel.I.MouseDictionary;
    private Dictionary<int, OSMouse> MouseOverDictionary => OSModel.I.MouseOverDictionary;
    private static Dictionary<bool, OSMouseScrollWheel> MouseSWDictionary => OSModel.I.MouseSWDictionary;
    private Dictionary<KeyCode, OSKeyboard> KeyboardDictionary => OSModel.I.KeyboardDictionary;

    private bool IsEventSystem {
        get { return EventSystem.current != null; }
    }
    private bool IsPointerOverGameObject {
#if UNITY_STANDALONE
        //电脑平台
        get { return EventSystem.current.IsPointerOverGameObject(); }
#elif UNITY_WEBGL
        //WebGL平台
        get { return EventSystem.current.IsPointerOverGameObject(); }
#elif UNITY_ANDROID
        //安卓平台
        get { return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId); }
#elif UNITY_IOS
        //苹果平台
        get { return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId); }
#endif
    }
    private void Update() {
        foreach (KeyValuePair<int, OSMouse> item in MouseDictionary) {
            UpdateMouseInput(item.Key, item.Value);
        }
        foreach (KeyValuePair<int, OSMouse> item in MouseOverDictionary) {
            UpdateMouseOverInput(item.Key, item.Value);
        }
        foreach (KeyValuePair<bool, OSMouseScrollWheel> item in MouseSWDictionary) {
            float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
            UpdateMouseOverInput(scrollWheelValue, item.Key, item.Value);
        }
        foreach (KeyValuePair<KeyCode, OSKeyboard> item in KeyboardDictionary) {
            UpdateMouseInput(item.Key, item.Value);
        }
    }
    private void UpdateMouseInput(int index, OSMouse mouse) {
        if (Input.GetMouseButtonUp(index)) { mouse.Up(); mouse.IsDown = false; }
        if (mouse.IsDown) { mouse.Press(); }
        if (Input.GetMouseButtonDown(index)) { mouse.Down(); mouse.IsDown = true; }
    }
    private void UpdateMouseOverInput(int index, OSMouse mouse) {
        if (Input.GetMouseButtonUp(index)) { mouse.Up(); mouse.IsDown = false; }
        if (IsEventSystem && IsPointerOverGameObject) { return; }
        if (mouse.IsDown) { mouse.Press(); }
        if (Input.GetMouseButtonDown(index)) { mouse.Down(); mouse.IsDown = true; }
    }
    private void UpdateMouseOverInput(float scrollWheelValue, bool IsOver, OSMouseScrollWheel mouse) {
        if (IsEventSystem && IsPointerOverGameObject && IsOver) { return; }
        mouse.Scroll(scrollWheelValue);
    }
    private void UpdateMouseInput(KeyCode keyCode, OSKeyboard keyboard) {
        if (Input.GetKeyDown(keyCode)) { keyboard.Up(); keyboard.IsDown = false; }
        if (keyboard.IsDown) { keyboard.Press(); }
        if (Input.GetKeyUp(keyCode)) { keyboard.Down(); keyboard.IsDown = true; }
    }
}
