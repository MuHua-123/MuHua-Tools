using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ROModel {
    private static ROModel model;
    public static ROModel I {
        get { if (model == null) { model = new ROModel(); } return model; }
    }

    public RTHandle OutlineRT;
    public float OutlineSize = 5;
    public Material OutlineColor;
    public List<Transform> RenderObjs = new List<Transform>();
}
