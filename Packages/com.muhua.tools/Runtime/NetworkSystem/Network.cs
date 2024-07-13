using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
    public class Network : MonoBehaviour {
        public NetworkLoading networkLoading;
        public void WebRequestGet(string url, bool acLoad, Action<bool, string> action) {
            StartCoroutine(IEWebRequestGet(url, acLoad, action));
        }
        public void WebRequestPost(string url, bool acLoad, WWWForm form, Action<bool, string> action) {
            StartCoroutine(IEWebRequestPost(url, acLoad, form, action));
        }
        public void WebRequestPost(string url, bool acLoad, string json, Action<bool, string> action, Action<UnityWebRequest> webAction = null) {
            StartCoroutine(IEWebRequestPost(url, acLoad, json, webAction, action));
        }
        public void DownloadingTexture(string url, Action<Texture> action) {
            StartCoroutine(IDownloadingTexture(url, action));
        }
        public void DownloadingTexture2D(string url, Action<Texture2D> action) {
            StartCoroutine(IDownloadingTexture2D(url, action));
        }

        public virtual void WebHandle(UnityWebRequest web, Action<bool, string> action) {
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
    }
}