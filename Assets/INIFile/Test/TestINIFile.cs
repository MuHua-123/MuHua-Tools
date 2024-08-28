using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestINIFile : MonoBehaviour {
    public enum Section { Section1, Section2 }
    public enum Key { Key1, Key2, Key3 }

    public static string Root {
        get {
            string exclude = "Assets/StreamingAssets";
            string streaming = Application.streamingAssetsPath;
            return streaming.Remove(streaming.Length - exclude.Length);
        }
    }

    public string filePath;
    public Section section;
    public Key key;
    public string value;

    public void ReadTest() {
        Debug.Log(Root + filePath);
        Debug.Log(section.ToString());
        Debug.Log(key.ToString());

        value = INIFile.Read(section.ToString(), key.ToString(), Root + filePath);
        Debug.Log(value);
    }
    public void WriteTest() {
        INIFile.Write(section.ToString(), key.ToString(), value, Root + filePath);
    }
}
