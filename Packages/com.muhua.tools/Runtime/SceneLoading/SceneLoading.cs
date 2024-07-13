using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MuHua;

namespace MuHua {
    public class SceneLoading : MonoBehaviour {
        public Slider progressBar;
        private static SceneLoading instance;
        public static SceneLoading Instance { get { return instance; } }
        public static bool isInstance { get { return instance != null; } }
        protected virtual void Awake() {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }
        public static void Jump(string scene) {
            instance.LoadSceneAsync(scene);
        }
        public void LoadSceneAsync(string scene) {
            StartCoroutine(ILoadSceneAsync(scene));
        }
        public IEnumerator ILoadSceneAsync(string scene) {
            int disableProgress = 0;
            int toProgress = 0;
            AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
            ao.allowSceneActivation = false;
            SonActive(transform, true);
            while (ao.progress < 0.9f) {
                toProgress = (int)(ao.progress * 100);
                while (disableProgress < toProgress) {
                    ++disableProgress;
                    progressBar.value = disableProgress / 100.0f;//0.01开始
                    yield return new WaitForEndOfFrame();
                }
            }
            toProgress = 100;
            while (disableProgress < toProgress) {
                ++disableProgress;
                progressBar.value = disableProgress / 100.0f;
                yield return new WaitForEndOfFrame();
            }
            ao.allowSceneActivation = true;
            while (!ao.isDone) {
                yield return new WaitForEndOfFrame();
            }
            SonActive(transform,false);
        }
        private static void SonActive(Transform parent, bool active) {
            foreach (Transform item in parent) {
                item.gameObject.SetActive(active);
            }
        }
    }
}