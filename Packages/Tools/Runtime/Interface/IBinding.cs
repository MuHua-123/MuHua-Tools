using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public interface IBinding {
        public void Initialize();
        public void Release();
    }
}