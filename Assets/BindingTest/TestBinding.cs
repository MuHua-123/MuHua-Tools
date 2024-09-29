using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;


public class TestBinding : MonoBehaviour {
    private void Awake() {
        BindingTool.Initialize();
        
    }
    private void OnDestroy() {
        BindingTool.Release();
    }
}
