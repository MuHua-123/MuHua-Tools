using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSMouseScrollWheel {
    public bool IsOver;
    public event Action<float> OnScroll;

    public Action<float> Scroll => (value) => OnScroll?.Invoke(value);
}
