using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
        private static T instance;
        public static T Instance { get { return instance; } }
        public static bool isInstance { get { return instance != null; } }
        protected virtual void Awake() {
            if (instance == null) { instance = (T)this; }
            else { Destroy(gameObject); }
        }
        protected virtual void OnDestroy() {
            if (instance == this) { Destroy(gameObject); }
        }
    }
}