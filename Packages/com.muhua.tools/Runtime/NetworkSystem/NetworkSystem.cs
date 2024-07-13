using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
    public class NetworkSystem : MonoBehaviour {
        public NetworkLoading networkLoading;
        private static NetworkSystem instance;
        public static NetworkSystem Instance { get { return instance; } }
        public static bool isInstance { get { return instance != null; } }
        protected virtual void Awake() {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }

        public static void OpenURL(string WebUrl) { Application.OpenURL(WebUrl); }
        public static void WebRequestGet(string url, bool acLoad, Action<bool, string> action) {
            if (!isInstance) { Debug.LogError("Network Error!——Not Initialized"); return; }
            IEnumerator routine = instance.IEWebRequestGet(url, acLoad, action);
            instance.StartCoroutine(routine);
        }
        public static void WebRequestPost(string url, bool acLoad, WWWForm form, Action<bool, string> action) {
            if (!isInstance) { Debug.LogError("Network Error!——Not Initialized"); return; }
            IEnumerator routine = instance.IEWebRequestPost(url, acLoad, form, action);
            instance.StartCoroutine(routine);
        }
        public static void WebRequestPost(string url, bool acLoad, string json, Action<bool, string> action, Action<UnityWebRequest> webAction = null) {
            if (!isInstance) { Debug.LogError("Network Error!——Not Initialized"); return; }
            IEnumerator routine = instance.IEWebRequestPost(url, acLoad, json, webAction, action);
            instance.StartCoroutine(routine);
        }
        public static void DownloadingTexture(string url, Action<Texture> action) {
            if (!isInstance) { Debug.LogError("Network Error!——Not Initialized"); return; }
            IEnumerator routine = instance.IDownloadingTexture(url, action);
            instance.StartCoroutine(routine);
        }
        public static void DownloadingTexture2D(string url, Action<Texture2D> action) {
            if (!isInstance) { Debug.LogError("Network Error!——Not Initialized"); return; }
            IEnumerator routine = instance.IDownloadingTexture2D(url, action);
            instance.StartCoroutine(routine);
        }

        public void WebHandle(UnityWebRequest web, Action<bool, string> action) {
            bool isDone = !web.isDone || web.result != UnityWebRequest.Result.Success;
            if (isDone) { Debug.Log("Network Error!——" + web.url); }
            Debug.Log(web.downloadHandler.text);
            action?.Invoke(!isDone, web.downloadHandler.text);
        }
        public IEnumerator IEWebRequestGet(string url, bool acLoad, Action<bool, string> action) {
            if (acLoad) { networkLoading.SetActive(true); }
            using (UnityWebRequest web = UnityWebRequest.Get(url)) {
                yield return web.SendWebRequest();
                networkLoading.SetActive(false);
                WebHandle(web, action);
            }
        }
        public IEnumerator IEWebRequestPost(string url, bool acLoad, WWWForm form, Action<bool, string> action) {
            if (acLoad) { networkLoading.SetActive(true); }
            using (UnityWebRequest web = UnityWebRequest.Post(url, form)) {
                yield return web.SendWebRequest();
                networkLoading.SetActive(false);
                WebHandle(web, action);
            }
        }
        public IEnumerator IEWebRequestPost(string url, bool acLoad, string json, Action<UnityWebRequest> webAction, Action<bool, string> action) {
            if (acLoad) { networkLoading.SetActive(true); }
            byte[] postBytes = System.Text.Encoding.Default.GetBytes(json);
#if UNITY_2022
            using (UnityWebRequest web = UnityWebRequest.PostWwwForm(url, "POST")) {
#else
            using (UnityWebRequest web = UnityWebRequest.Post(url, "POST")) {
#endif
                web.uploadHandler.Dispose();
                web.uploadHandler = new UploadHandlerRaw(postBytes);
                webAction.Invoke(web);
                web.SetRequestHeader("Content-Type", "application/json");
                yield return web.SendWebRequest();
                networkLoading.SetActive(false);
                WebHandle(web, action);
            }
        }
        public IEnumerator IDownloadingTexture(string url, Action<Texture> action) {
            using (UnityWebRequest web = UnityWebRequestTexture.GetTexture(url)) {
                yield return web.SendWebRequest();
                bool isDone = !web.isDone || web.result != UnityWebRequest.Result.Success;
                if (isDone) { Debug.Log("纹理加载失败：" + url); yield break; }
                action?.Invoke((web.downloadHandler as DownloadHandlerTexture).texture);
            }
        }
        public IEnumerator IDownloadingTexture2D(string url, Action<Texture2D> action) {
            using (UnityWebRequest web = UnityWebRequestTexture.GetTexture(url)) {
                yield return web.SendWebRequest();
                bool isDone = !web.isDone || web.result != UnityWebRequest.Result.Success;
                if (isDone) { Debug.Log("纹理加载失败：" + url); yield break; }
                action?.Invoke((web.downloadHandler as DownloadHandlerTexture).texture);
            }
        }

        public static void ITimeAndImages(Action<DateTime, Texture2D> action) {
            if (!isInstance) { Debug.LogError("Network Error!——Not Initialized"); return; }
            IEnumerator routine = instance.IUpdateTime((time) => { ITimeAndImages(time, action); });
            instance.StartCoroutine(routine);
        }
        public static void ITimeAndImages(DateTime time, Action<DateTime, Texture2D> action) {
            if (!isInstance) { Debug.LogError("Network Error!——Not Initialized"); return; }
            IEnumerator routine = instance.IScreenshot((texture2D) => { action?.Invoke(time, texture2D); });
            instance.StartCoroutine(routine);
        }
        public static void Screenshot(Action<Texture2D> action) {
            if (!isInstance) { Debug.LogError("Network Error!——Not Initialized"); return; }
            IEnumerator routine = instance.IScreenshot(action);
            instance.StartCoroutine(routine);
        }
        public IEnumerator IUpdateTime(Action<DateTime> action) {
            string timeUrl = "https://www.tsa.cn/api/time/getCurrentTime";
            yield return IEWebRequestGet(timeUrl, true, (isDone, result) => {
                Time(isDone, result, out DateTime submitTime);
                action?.Invoke(submitTime);
            });
        }
        public static void Time(bool isDone, string result, out DateTime submitTime) {
            try {
                DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
                long time = long.Parse(result);
                submitTime = startTime.AddMilliseconds(time);
            }
            catch (Exception e) {
                Debug.Log("获取网络时间失败:" + e.Message);
                submitTime = DateTime.Now;
            }
        }
        public IEnumerator IScreenshot(Action<Texture2D> action) {
            yield return new WaitForEndOfFrame();
            int width = Screen.width; int height = Screen.height;
            Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
            texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            texture2D.Apply(); action?.Invoke(texture2D);
        }
    }
}