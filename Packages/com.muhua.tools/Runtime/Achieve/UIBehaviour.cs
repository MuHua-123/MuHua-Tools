using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
    public class UIBehaviour : MonoBehaviour {
        public UIDocument document;
        public VisualElement Root => document.rootVisualElement;
    }
}