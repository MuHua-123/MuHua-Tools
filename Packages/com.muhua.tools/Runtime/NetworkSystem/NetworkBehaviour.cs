using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
    public class NetworkBehaviour : MonoBehaviour {
        public class NetworkEvent {
            public Action<UnityWebRequest> OnWebRequest;
            public Action<bool> OnLoadingHandle;
            public Action<bool, string> OnCompleteHandle;
            public Action<bool, Texture2D> OnTextureHandle;

            public void WebRequest(UnityWebRequest UWR) => OnWebRequest?.Invoke(UWR);
            public void LoadingHandle(bool load) => OnLoadingHandle?.Invoke(load);
            public void CompleteHandle(bool isDone, string json) => OnCompleteHandle?.Invoke(isDone, json);
            public void TextureHandle(bool isDone, Texture2D texture) => OnTextureHandle?.Invoke(isDone, texture);
        }

        public void WebRequestGet(string url, Action<bool, string> handle, Action<bool> load = null) {
            NetworkEvent networkEvent = new NetworkEvent();
            networkEvent.OnLoadingHandle = load;
            networkEvent.OnCompleteHandle = handle;
            WebRequestGet(url, networkEvent);
        }
        public void WebRequestGet(string url, NetworkEvent networkEvent) {
            StartCoroutine(IEWebRequestGet(url, networkEvent));
        }
        public void WebRequestPost(string url, WWWForm form, Action<bool, string> handle, Action<bool> load = null) {
            NetworkEvent networkEvent = new NetworkEvent();
            networkEvent.OnLoadingHandle = load;
            networkEvent.OnCompleteHandle = handle;
            WebRequestPost(url, form, networkEvent);
        }
        public void WebRequestPost(string url, WWWForm form, NetworkEvent networkEvent) {
            StartCoroutine(IEWebRequestPost(url, form, networkEvent));
        }
        public void WebRequestPost(string url, string json, Action<bool, string> handle, Action<bool> load = null) {
            NetworkEvent networkEvent = new NetworkEvent();
            networkEvent.OnLoadingHandle = load;
            networkEvent.OnCompleteHandle = handle;
            WebRequestPost(url, json, networkEvent);
        }
        public void WebRequestPost(string url, string json, NetworkEvent networkEvent) {
            StartCoroutine(IEWebRequestPost(url, json, networkEvent));
        }
        public void DownloadingTexture2D(string url, Action<bool, Texture2D> handle, Action<bool> load = null) {
            NetworkEvent networkEvent = new NetworkEvent();
            networkEvent.OnLoadingHandle = load;
            networkEvent.OnTextureHandle = handle;
            StartCoroutine(IDownloadingTexture2D(url, networkEvent));
        }
        public void DownloadingTexture2D(string url, NetworkEvent networkEvent) {
            StartCoroutine(IDownloadingTexture2D(url, networkEvent));
        }

        public virtual void WebRequestHandle(UnityWebRequest web, NetworkEvent networkEvent) {
            bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
            if (!isDone) { Debug.Log("Network Error!——" + web.url); }
            Debug.Log(web.downloadHandler.text);
            networkEvent.CompleteHandle(isDone, web.downloadHandler.text);
        }

        public IEnumerator IEWebRequestGet(string url, NetworkEvent networkEvent) {
            using (UnityWebRequest UWR = UnityWebRequest.Get(url)) {
                networkEvent.LoadingHandle(true);
                networkEvent.WebRequest(UWR);
                yield return UWR.SendWebRequest();
                networkEvent.LoadingHandle(false);
                WebRequestHandle(UWR, networkEvent);
            }
        }
        public IEnumerator IEWebRequestPost(string url, WWWForm form, NetworkEvent networkEvent) {
            using (UnityWebRequest UWR = UnityWebRequest.Post(url, form)) {
                networkEvent.LoadingHandle(true);
                networkEvent.WebRequest(UWR);
                yield return UWR.SendWebRequest();
                networkEvent.LoadingHandle(false);
                WebRequestHandle(UWR, networkEvent);
            }
        }
        public IEnumerator IEWebRequestPost(string url, string json, NetworkEvent networkEvent) {
#if UNITY_2022
            using (UnityWebRequest UWR = UnityWebRequest.PostWwwForm(url, "POST")) {
#else
            using (UnityWebRequest UWR = UnityWebRequest.Post(url, "POST")) {
#endif
                networkEvent.LoadingHandle(true);
                byte[] postBytes = System.Text.Encoding.Default.GetBytes(json);
                UWR.uploadHandler.Dispose();
                UWR.uploadHandler = new UploadHandlerRaw(postBytes);
                UWR.SetRequestHeader("Content-Type", "application/json");
                networkEvent.WebRequest(UWR);
                yield return UWR.SendWebRequest();
                networkEvent.LoadingHandle(false);
                WebRequestHandle(UWR, networkEvent);
            }
        }
        public IEnumerator IDownloadingTexture2D(string url, NetworkEvent networkEvent) {
            using (UnityWebRequest UWR = UnityWebRequestTexture.GetTexture(url)) {
                networkEvent.LoadingHandle(true);
                networkEvent.WebRequest(UWR);
                yield return UWR.SendWebRequest();
                bool isDone = UWR.isDone && UWR.result == UnityWebRequest.Result.Success;
                if (!isDone) { Debug.Log("纹理加载失败：" + url); yield break; }
                networkEvent.LoadingHandle(false);
                networkEvent.TextureHandle(isDone, (UWR.downloadHandler as DownloadHandlerTexture).texture);
            }
        }
    }
}