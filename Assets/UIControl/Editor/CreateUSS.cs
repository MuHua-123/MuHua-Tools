using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class CreateUSS : Editor {
    /// <summary>
    /// 创建自定义控件的USS
    /// </summary>
    /// <param name="ussResourcesPath"></param>
    /// <param name="name"></param>
    private static void USSCreate(string ussResourcesPath, string name) {
        string path = Application.dataPath.Replace("Assets", "");
        //原始文件路径
        StyleSheet style = Resources.Load<StyleSheet>(ussResourcesPath);
        string original = path + AssetDatabase.GetAssetPath(style);
        //目标文件路径
        string selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string fileName = DuplicateNameJudgment(path + selectPath, name, ".uss");
        string filePath = path + selectPath + fileName;
        //拷贝文件
        File.Copy(original, filePath);
        AssetDatabase.Refresh();
        //选中新创建的文件
        string relative = selectPath + fileName;
        Object asset = AssetDatabase.LoadAssetAtPath(relative, typeof(Object));
        Selection.activeObject = asset;
    }
    private static string DuplicateNameJudgment(string path, string name, string extend, int index = 0) {
        string offset = index == 0 ? "" : index.ToString();
        string fileName = "/" + name + offset + extend;
        string filePath = path + fileName;
        if (File.Exists(filePath)) { index++; return DuplicateNameJudgment(path, name, extend, index); }
        return fileName;
    }
    [MenuItem("Assets/Create/UI Toolkit/VerticalScrollView USS")]
    private static void USSVerticalScrollView() {
        USSCreate(VerticalScrollView.USS, typeof(VerticalScrollView).Name);
    }
    [MenuItem("Assets/Create/UI Toolkit/PopupPrompt USS")]
    private static void USSPopupPrompt() {
        USSCreate(PopupPrompt.USS, typeof(PopupPrompt).Name);
    }
}
