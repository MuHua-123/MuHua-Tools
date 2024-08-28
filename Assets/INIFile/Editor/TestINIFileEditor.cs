using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestINIFile))]
public class TestINIFileEditor : Editor {
    private TestINIFile value;
    private void Awake() {
        value = (TestINIFile)target;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("读取数据")) { value.ReadTest(); }
        if (GUILayout.Button("写入数据")) { value.WriteTest(); }
    }
}
