using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ROControl {
    public static List<Transform> RenderObjs {
        get => ROModel.I.RenderObjs;
        set => ROModel.I.RenderObjs = value;
    }
    /// <summary> 加入轮廓渲染队列 </summary>
    public static void Enqueue(Transform obj) {
        if (!RenderObjs.Contains(obj)) { RenderObjs.Add(obj); }
    }
    /// <summary> 退出轮廓渲染队列 </summary>
    public static void Dequeue(Transform obj) {
        if (RenderObjs.Contains(obj)) { RenderObjs.Remove(obj); }
    }
}
