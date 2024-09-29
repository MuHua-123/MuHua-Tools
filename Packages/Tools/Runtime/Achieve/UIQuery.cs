using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
    public class UIQuery {
        public readonly VisualElement element;
        public UIQuery(UIQuery uIQuery) { element = uIQuery.element; }
        public UIQuery(UIDocument document) { element = document.rootVisualElement; }
        public UIQuery(VisualElement element) { this.element = element; }
        public UIQuery(VisualTreeAsset asset) { element = asset.Instantiate(); }
    }
}