using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public interface ITemplate<T> {
        void SetValue(T value);
    }
    public static class TemplateFactory {
        public static void DestroySon(Transform parent, Transform temp = null) {
            foreach (Transform item in parent) {
                if (item == temp) continue;
                GameObject.Destroy(item.gameObject);
            }
        }
        public static void SonActive(this Transform parent, bool active) {
            foreach (Transform item in parent) {
                item.gameObject.SetActive(active);
            }
        }
        public static Transform Instantiate<T>(Transform temp, Transform parent, T value, bool active) {
            Transform template = Transform.Instantiate(temp, parent);
            template.gameObject.SetActive(active);
            ITemplate<T> item = template.GetComponent<ITemplate<T>>();
            item.SetValue(value);
            return template;
        }
        public static List<Transform> Instantiate<T>(Transform temp, Transform parent, List<T> list, bool destroy = true, bool active = true) {
            if (destroy) { DestroySon(parent, temp); }
            List<Transform> transforms = new List<Transform>();
            foreach (T item in list) {
                transforms.Add(Instantiate(temp, parent, item, active));
            }
            return transforms;
        }
        public static List<Transform> Instantiate<T>(Transform temp, Transform parent, T[,] array, bool destroy = true, bool active = true) {
            if (destroy) { DestroySon(parent, temp); }
            List<Transform> transforms = new List<Transform>();
            for (int y = 0; y < array.GetLength(1); y++) {
                for (int x = 0; x < array.GetLength(0); x++) {
                    transforms.Add(Instantiate(temp, parent, array[x, y], active));
                }
            }
            return transforms;
        }
    }
}