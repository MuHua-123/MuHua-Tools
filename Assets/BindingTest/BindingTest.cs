using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class BindingTest : MonoBehaviour, IBinding {
    public void Initialize() {
        Debug.Log("初始化" + gameObject.name);
    }
    public void Release() {
        Debug.Log("释放" + gameObject.name);
    }
}
