using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ROOutlineBlendRendererFeature))]
public class ROOutlineBlendRendererFeatureEditor : Editor {
    private float OutlineSize = 5;
    private Material OutlineColor;
    private ROOutlineBlendRendererFeature value;
    private void Awake() {
        value = (ROOutlineBlendRendererFeature)target;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (OutlineSize != value.OutlineSize) {
            OutlineSize = ROModel.I.OutlineSize = value.OutlineSize;
        }
        if (OutlineColor != value.OutlineColor) {
            OutlineColor = ROModel.I.OutlineColor = value.OutlineColor;
        }

        if (OutlineSize != ROModel.I.OutlineSize) {
            OutlineSize = value.OutlineSize = ROModel.I.OutlineSize;
        }
        if (OutlineColor != ROModel.I.OutlineColor) {
            OutlineColor = value.OutlineColor = ROModel.I.OutlineColor;
        }
    }
}
