using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
    public static class TemplateFactoryUI {
        public static void AddElement(this VisualElement parent, UIQuery ui) {
            parent.Add(ui.element);
        }
        public static List<Template> Instantiate<Template, V>(this VisualElement parent, VisualTreeAsset template, V[] list) where Template : ITemplateUI<V>, new() {
            List<Template> templates = new List<Template>();
            for (int i = 0; i < list.Length; i++) {
                templates.Add(parent.Instantiate<Template, V>(template, list[i]));
            }
            return templates;
        }
        public static List<Template> Instantiate<Template, V>(this VisualElement parent, VisualTreeAsset template, List<V> list) where Template : ITemplateUI<V>, new() {
            List<Template> templates = new List<Template>();
            for (int i = 0; i < list.Count; i++) {
                templates.Add(parent.Instantiate<Template, V>(template, list[i]));
            }
            return templates;
        }
        public static Template Instantiate<Template, V>(this VisualElement parent, VisualTreeAsset template, V value) where Template : ITemplateUI<V>, new() {
            Template temp = new Template();
            UIQuery query = new UIQuery(template);
            parent.AddElement(query);
            temp.SetValue(value, query);
            return temp;
        }
    }
}
