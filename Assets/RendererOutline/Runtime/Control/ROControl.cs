using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ROControl {
    public static List<Transform> RenderObjs {
        get => ROModel.I.RenderObjs;
        set => ROModel.I.RenderObjs = value;
    }
    public static void Enqueue(Transform obj) {
        if (!RenderObjs.Contains(obj)) { RenderObjs.Add(obj); }
    }
    public static void Dequeue(Transform obj) {
        if (RenderObjs.Contains(obj)) { RenderObjs.Remove(obj); }
    }
}
