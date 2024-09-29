using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuHua {
    public static class BindingTool {
        public static void Initialize() {
            Object[] objs = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour));
            IEnumerable<IBinding> types = objs.OfType<IBinding>();
            foreach (IBinding binding in types) { binding.Initialize(); }
        }
        public static void Release() {
            Object[] objs = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour));
            IEnumerable<IBinding> types = objs.OfType<IBinding>();
            foreach (IBinding binding in types) { binding.Release(); }
        }

        public static void Initialize(this GameObject obj) {
            obj.GetComponent<IBinding>()?.Initialize();
        }
        public static void Initialize(this Transform obj) {
            obj.GetComponent<IBinding>()?.Initialize();
        }

        public static void Release(this GameObject obj) {
            obj.GetComponent<IBinding>()?.Release();
        }
        public static void Release(this Transform obj) {
            obj.GetComponent<IBinding>()?.Release();
        }
    }
}
