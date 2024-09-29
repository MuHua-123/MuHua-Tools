using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public static class RayTool {
        public static RaycastHit hitInfo;
        public static readonly LayerMask DefaultLayerMask = ~(1 << 0) | 1 << 0;

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

        /// <summary> 从鼠标坐标获取对象 </summary>
        public static bool GetMouseToWorldObject<T>(out T value) where T : Object {
            return GetScreenToWorldObject(Input.mousePosition, out value);
        }
        /// <summary> 从鼠标坐标获取对象 </summary>
        public static bool GetMouseToWorldObject<T>(Camera camera, out T value) where T : Object {
            return GetScreenToWorldObject(camera, Input.mousePosition, out value);
        }
        /// <summary> 从鼠标坐标获取对象 </summary>
        public static bool GetMouseToWorldObject<T>(out T value, LayerMask planeLayerMask) where T : Object {
            return GetScreenToWorldObject(Input.mousePosition, out value, planeLayerMask);
        }
        /// <summary> 从鼠标坐标获取对象 </summary>
        public static bool GetMouseToWorldObject<T>(Camera camera, out T value, LayerMask planeLayerMask) where T : Object {
            return GetScreenToWorldObject(camera, Input.mousePosition, out value, planeLayerMask);
        }

        /// <summary> 从屏幕坐标获取对象 </summary>
        public static bool GetScreenToWorldObject<T>(Vector3 screen, out T value) where T : Object {
            return GetScreenToWorldObject(screen, out value, DefaultLayerMask);
        }
        /// <summary> 从屏幕坐标获取对象 </summary>
        public static bool GetScreenToWorldObject<T>(Camera camera, Vector3 screen, out T value) where T : Object {
            return GetScreenToWorldObject(camera, screen, out value, DefaultLayerMask);
        }
        /// <summary> 从屏幕坐标获取对象 </summary>
        public static bool GetScreenToWorldObject<T>(Vector3 screen, out T value, LayerMask planeLayerMask) where T : Object {
            return GetScreenToWorldObject(Camera.main, screen, out value, planeLayerMask);
        }
        /// <summary> 从屏幕坐标获取对象 </summary>
        public static bool GetScreenToWorldObject<T>(Camera camera, Vector3 screen, out T value, LayerMask planeLayerMask) where T : Object {
            Ray ray = camera.ScreenPointToRay(screen);
            Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
            value = hitInfo.transform?.GetComponent<T>();
            return value != null;
        }

        /// <summary> 从鼠标坐标获取碰撞信息 </summary>
        public static bool GetMouseToWorldHitInfo(out RaycastHit hitInfo) {
            return GetScreenToWorldHitInfo(Input.mousePosition, out hitInfo);
        }
        /// <summary> 从鼠标坐标获取碰撞信息 </summary>
        public static bool GetMouseToWorldHitInfo(Camera camera, out RaycastHit hitInfo) {
            return GetScreenToWorldHitInfo(camera, Input.mousePosition, out hitInfo);
        }
        /// <summary> 从鼠标坐标获取碰撞信息 </summary>
        public static bool GetMouseToWorldHitInfo(out RaycastHit hitInfo, LayerMask planeLayerMask) {
            return GetScreenToWorldHitInfo(Input.mousePosition, out hitInfo, planeLayerMask);
        }
        /// <summary> 从鼠标坐标获取碰撞信息 </summary>
        public static bool GetMouseToWorldHitInfo(Camera camera, out RaycastHit hitInfo, LayerMask planeLayerMask) {
            return GetScreenToWorldHitInfo(camera, Input.mousePosition, out hitInfo, planeLayerMask);
        }

        /// <summary> 从屏幕坐标获取碰撞信息 </summary>
        public static bool GetScreenToWorldHitInfo(Vector3 screen, out RaycastHit hitInfo) {
            return GetScreenToWorldHitInfo(screen, out hitInfo, DefaultLayerMask);
        }
        /// <summary> 从屏幕坐标获取碰撞信息 </summary>
        public static bool GetScreenToWorldHitInfo(Camera camera, Vector3 screen, out RaycastHit hitInfo) {
            return GetScreenToWorldHitInfo(camera, screen, out hitInfo, DefaultLayerMask);
        }
        /// <summary> 从屏幕坐标获取碰撞信息 </summary>
        public static bool GetScreenToWorldHitInfo(Vector3 screen, out RaycastHit hitInfo, LayerMask planeLayerMask) {
            return GetScreenToWorldHitInfo(Camera.main, screen, out hitInfo, planeLayerMask);
        }
        /// <summary> 从屏幕坐标获取碰撞信息 </summary>
        public static bool GetScreenToWorldHitInfo(Camera camera, Vector3 screen, out RaycastHit hitInfo, LayerMask planeLayerMask) {
            Ray ray = camera.ScreenPointToRay(screen);
            return Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
        }
    }
}