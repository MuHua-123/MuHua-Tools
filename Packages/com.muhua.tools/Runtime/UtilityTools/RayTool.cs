using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public static class RayTool {
        public static RaycastHit hitInfo;
        public static LayerMask DefaultLayerMask = ~(1 << 0) | 1 << 0;

        /// <summary> 屏幕坐标转世界坐标 </summary>
        public static bool GetScreenToWorldPosition(Vector3 screen, out Vector3 position) {
            return GetScreenToWorldPosition(screen, out position, DefaultLayerMask);
        }
        /// <summary> 屏幕坐标转世界坐标 </summary>
        public static bool GetScreenToWorldPosition(Camera camera, Vector3 screen, out Vector3 position) {
            return GetScreenToWorldPosition(camera, screen, out position, DefaultLayerMask);
        }
        /// <summary> 屏幕坐标转世界坐标 </summary>
        public static bool GetScreenToWorldPosition(Vector3 screen, out Vector3 position, LayerMask planeLayerMask) {
            return GetScreenToWorldPosition(Camera.main, screen, out position, planeLayerMask);
        }
        /// <summary> 屏幕坐标转世界坐标 </summary>
        public static bool GetScreenToWorldPosition(Camera camera, Vector3 screen, out Vector3 position, LayerMask planeLayerMask) {
            Ray ray = camera.ScreenPointToRay(screen);
            Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
            position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            if (hitInfo.transform != null) { position = hitInfo.point; }
            return hitInfo.transform != null;
        }

        /// <summary> 鼠标坐标转世界坐标 </summary>
        public static bool GetMouseToWorldPosition(out Vector3 position) {
            return GetScreenToWorldPosition(Input.mousePosition, out position);
        }
        /// <summary> 鼠标坐标转世界坐标 </summary>
        public static bool GetMouseToWorldPosition(Camera camera, out Vector3 position) {
            return GetScreenToWorldPosition(camera, Input.mousePosition, out position);
        }
        /// <summary> 鼠标坐标转世界坐标 </summary>
        public static bool GetMouseToWorldPosition(out Vector3 position, LayerMask planeLayerMask) {
            return GetScreenToWorldPosition(Input.mousePosition, out position, planeLayerMask);
        }
        /// <summary> 鼠标坐标转世界坐标 </summary>
        public static bool GetMouseToWorldPosition(Camera camera, out Vector3 position, LayerMask planeLayerMask) {
            return GetScreenToWorldPosition(camera, Input.mousePosition, out position, planeLayerMask);
        }

        public static bool GetMouseWorldSnappedPosition(out Vector3 position) {
            return GetMouseWorldSnappedPosition(out position, DefaultLayerMask);
        }
        public static bool GetMouseWorldSnappedPosition(out Vector3 position, LayerMask planeLayerMask) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
            position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            if (hitInfo.transform != null) { position = hitInfo.point; }
            return hitInfo.transform != null;
        }
        public static bool GetMouseWorldSnappedPosition(out RaycastHit hitInfo, LayerMask planeLayerMask) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
        }

        public static bool GetMouseClickObject<T>(out T value) where T : Object {
            return GetMouseClickObject(DefaultLayerMask, out value);
        }
        public static bool GetMouseClickObject<T>(LayerMask planeLayerMask, out T value) where T : Object {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
            value = hitInfo.transform?.GetComponent<T>();
            return value != null;
        }
    }
}