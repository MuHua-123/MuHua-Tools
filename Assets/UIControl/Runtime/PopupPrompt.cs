using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupPrompt : VisualElement {
    public static readonly string USS = "USS/PopupPrompt";
    public static readonly string UXML = "UXML/PopupPrompt";

    public static readonly string UssName = "Popup-Prompt";
    public static readonly string UssNameLabel = UssName + "-Label";
    public static readonly string UssNameButton = UssName + "-Button";
    public static readonly string UssNameContent = UssName + "-Content";
    public static readonly string UssNameBottom = UssName + "-Bottom";

    public static readonly string UssClassName = "popup-prompt";
    public static readonly string UssClassNameLabel = UssClassName + "-label";
    public static readonly string UssClassNameButton = UssClassName + "-button";
    public static readonly string UssClassNameContent = UssClassName + "-content";
    public static readonly string UssClassNameBottom = UssClassName + "-bottom";
    public new class UxmlFactory : UxmlFactory<PopupPrompt, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits {
        public UxmlStringAttributeDescription Text = new UxmlStringAttributeDescription {
            name = "Text", defaultValue = "弹窗内容"
        };
        public UxmlStringAttributeDescription ButtonText = new UxmlStringAttributeDescription {
            name = "Button-Text", defaultValue = "确认"
        };
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
            base.Init(ve, bag, cc);
            PopupPrompt popupPrompt = ve as PopupPrompt;
            popupPrompt.text = Text.GetValueFromBag(bag, cc);
            popupPrompt.buttonText = ButtonText.GetValueFromBag(bag, cc);
        }
    }

    public Label label = new Label();
    public Button button = new Button();
    public VisualElement content = new VisualElement();
    public VisualElement bottom = new VisualElement();

    public string text {
        get => label.text;
        set => label.text = value;
    }
    public string buttonText {
        get => button.text;
        set => button.text = value;
    }

    public PopupPrompt() {
        //加载默认样式
        StyleSheet style = Resources.Load<StyleSheet>(USS);
        if (style != null) { styleSheets.Add(style); }
        //设置名称
        label.name = UssNameLabel;
        button.name = UssNameButton;
        content.name = UssNameContent;
        bottom.name = UssNameBottom;
        //设置USS类名
        AddToClassList(UssClassName);
        label.AddToClassList(UssClassNameLabel);
        button.AddToClassList(UssClassNameButton);
        content.AddToClassList(UssClassNameContent);
        bottom.AddToClassList(UssClassNameBottom);
        //设置层级结构
        content.Add(label);
        bottom.Add(button);
        hierarchy.Add(content);
        hierarchy.Add(bottom);
    }
}
