using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OSControl {
    private static Dictionary<int, OSMouse> MouseDictionary => OSModel.I.MouseDictionary;
    private static Dictionary<int, OSMouse> MouseOverDictionary => OSModel.I.MouseOverDictionary;
    private static Dictionary<bool, OSMouseScrollWheel> MouseSWDictionary => OSModel.I.MouseSWDictionary;
    private static Dictionary<KeyCode, OSKeyboard> KeyboardDictionary => OSModel.I.KeyboardDictionary;

    /// <summary> 鼠标输入模块 </summary>
    public static OSMouse Mouse(OSMouseID mouseID) {
        return Mouse((int)mouseID);
    }
    /// <summary> 鼠标输入模块 </summary>
    public static OSMouse Mouse(int index) {
        if (MouseDictionary.TryGetValue(index, out OSMouse mouse)) { return mouse; }
        mouse = new OSMouse { Index = index };
        MouseDictionary.Add(index, mouse);
        return mouse;
    }

    /// <summary> 鼠标输入模块(在UI上不执行Down和Press) </summary>
    public static OSMouse MouseOver(OSMouseID mouseID) {
        return MouseOver((int)mouseID);
    }
    /// <summary> 鼠标输入模块(在UI上不执行Down和Press) </summary>
    public static OSMouse MouseOver(int index) {
        if (MouseOverDictionary.TryGetValue(index, out OSMouse mouse)) { return mouse; }
        mouse = new OSMouse { Index = index };
        MouseOverDictionary.Add(index, mouse);
        return mouse;
    }

    /// <summary> 鼠标滚轮输入模块(IsOver == true 在UI上不执行) </summary>
    public static OSMouseScrollWheel MouseScrollWheel(bool IsOver) {
        if (MouseSWDictionary.TryGetValue(IsOver, out OSMouseScrollWheel mouse)) { return mouse; }
        mouse = new OSMouseScrollWheel { IsOver = IsOver };
        MouseSWDictionary.Add(IsOver, mouse);
        return mouse;
    }
    /// <summary> 键盘输入模块 </summary>
    public static OSKeyboard Keyboard(KeyCode keyCode) {
        if (KeyboardDictionary.TryGetValue(keyCode, out OSKeyboard keyboard)) { return keyboard; }
        keyboard = new OSKeyboard { KeyCode = keyCode };
        KeyboardDictionary.Add(keyCode, keyboard);
        return keyboard;
    }
}
