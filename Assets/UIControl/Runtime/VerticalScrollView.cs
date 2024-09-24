using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class VerticalScrollView : VisualElement {
    public static readonly string USS = "USS/VerticalScrollView";
    public static readonly string UXML = "UXML/VerticalScrollView";

    public static readonly string UssClassName = "vertical-scroll-view";
    public static readonly string UssClassNameViewport = UssClassName + "-viewport";
    public static readonly string UssClassNameContent = UssClassName + "-content";
    public static readonly string UssClassNameSlider = UssClassName + "-slider";
    public static readonly string UssClassNameDragger = UssClassName + "-dragger";
    public static readonly string UssClassNameHide = UssClassName + "-hide";

    public new class UxmlFactory : UxmlFactory<VerticalScrollView, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits {
        public UxmlIntAttributeDescription Count = new UxmlIntAttributeDescription {
            name = "Count", defaultValue = 10
        };
        public UxmlTypeAttributeDescription<VisualElement> Template = new UxmlTypeAttributeDescription<VisualElement> {
            name = "Template", defaultValue = typeof(Button)
        };
        public UxmlEnumAttributeDescription<ScrollerVisibility> VerticalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility> {
            name = "Vertical-Scroller-Visibility", defaultValue = ScrollerVisibility.AlwaysVisible
        };
        //public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
        //    get { yield break; }
        //}
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
            base.Init(ve, bag, cc);
            VerticalScrollView vsv = ve as VerticalScrollView;

            int count = 10;
            Count.TryGetValueFromBag(bag, cc, ref count);
            vsv.count = math.max(0, count);

            Type template = typeof(Button);
            Template.TryGetValueFromBag(bag, cc, ref template);
            vsv.template = template;

            ScrollerVisibility sv = ScrollerVisibility.AlwaysVisible;
            VerticalScrollerVisibility.TryGetValueFromBag(bag, cc, ref sv);
            vsv.verticalScrollerVisibility = sv;

            vsv.UpdateTemplate();
        }
    }

    public ScrollView scrollView = new ScrollView();

    public int count { get; set; }
    public Type template { get; set; }
    public ScrollerVisibility verticalScrollerVisibility {
        get => scrollView.verticalScrollerVisibility;
        set => scrollView.verticalScrollerVisibility = value;
    }

    public VerticalScrollView() {
        //加载默认样式
        StyleSheet style = Resources.Load<StyleSheet>(USS);
        if (style != null) { styleSheets.Add(style); }
        //设置USS类名
        scrollView.Q<VisualElement>("unity-content-and-vertical-scroll-container").AddToClassList(UssClassName);
        scrollView.Q<VisualElement>("unity-content-viewport").AddToClassList(UssClassNameViewport);
        scrollView.Q<VisualElement>("unity-content-container").AddToClassList(UssClassNameContent);
        scrollView.Q<VisualElement>("unity-slider").AddToClassList(UssClassNameSlider);
        scrollView.Q<VisualElement>("unity-dragger").AddToClassList(UssClassNameDragger);

        scrollView.Q<VisualElement>("unity-low-button").AddToClassList(UssClassNameHide);
        scrollView.Q<VisualElement>("unity-high-button").AddToClassList(UssClassNameHide);
        scrollView.Q<VisualElement>("unity-tracker").AddToClassList(UssClassNameHide);
        //设置结构
        hierarchy.Add(scrollView);
    }
    public void UpdateTemplate() {
        scrollView.Clear();
        for (int i = 0; i < count; i++) {
            scrollView.Add(Activator.CreateInstance(template) as VisualElement);
        }
    }
}
