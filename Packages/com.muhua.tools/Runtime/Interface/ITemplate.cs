using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public interface ITemplate<T> {
        void SetValue(T value);
    }
}