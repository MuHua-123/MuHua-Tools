using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MuHua {
    public class SceneJump : MonoBehaviour {
        [SceneName] public string scene;
        public void Jump() { SceneLoading.Jump(scene); }
    }
}