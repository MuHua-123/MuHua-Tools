using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSModel {
    private static OSModel model;
    public static OSModel I {
        get { if (model == null) { model = new OSModel(); } return model; }
    }

    public Dictionary<int, OSMouse> MouseDictionary = new Dictionary<int, OSMouse>();
    public Dictionary<int, OSMouse> MouseOverDictionary = new Dictionary<int, OSMouse>();
    public Dictionary<bool, OSMouseScrollWheel> MouseSWDictionary = new Dictionary<bool, OSMouseScrollWheel>();
    public Dictionary<KeyCode, OSKeyboard> KeyboardDictionary = new Dictionary<KeyCode, OSKeyboard>();
}
