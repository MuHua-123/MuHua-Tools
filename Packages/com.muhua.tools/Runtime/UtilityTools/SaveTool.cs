using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace MuHua {
    public static class SaveTool {
        /// <summary>默认扩展名</summary>
        public const string EXTENSION = "MuHua";
        /// <summary>各平台本地保存路径</summary>
        public static string PATH {
#if UNITY_IOS
            get { return  Application.persistentDataPath;}
#elif UNITY_ANDROID
            get { return  Application.persistentDataPath;}
#else
            get { return Application.streamingAssetsPath; }
#endif
        }
        #region 字符串保存
        /// <summary>保存字符串到本地文件夹</summary>
        /// <param name="directory">文件夹</param>
        /// <param name="fileName">文件名</param>
        /// <param name="saveString">保存内容</param>
        public static void SaveText(string directory, string fileName, string saveString) {
            string filePath = directory + fileName;
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            File.WriteAllText(filePath, saveString);
        }
        /// <summary>读取文件返回字符串</summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>读取内容</returns>
        public static string LoadText(string filePath) {
            if (File.Exists(filePath)) { return File.ReadAllText(filePath); }
            else { return null; }
        }
        #endregion
        #region 编码字符串保存
        /// <summary>保存编码字符串到本地文件夹</summary>
        /// <param name="directory">文件夹</param>
        /// <param name="fileName">文件名</param>
        /// <param name="saveString">保存内容</param>
        public static void SaveEncodingString(string directory, string fileName, string saveString, Encoding encodeType) {
            string filePath = directory + fileName;
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            byte[] byteArray = encodeType.GetBytes(saveString);
            string base64 = Convert.ToBase64String(byteArray);
            File.WriteAllText(filePath, base64);
        }
        /// <summary>读取编码文件返回字符串</summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>读取内容</returns>
        public static string LoadEncodingString(string filePath, Encoding encodeType) {
            if (File.Exists(filePath)) {
                string base64 = File.ReadAllText(filePath);
                byte[] byteArray = Convert.FromBase64String(base64);
                return encodeType.GetString(byteArray);
            }
            else { return null; }
        }
        #endregion
        #region 对象转Json的保存与加载
        /// <summary>保存Object为Json文件</summary>
        /// <param name="directory">文件夹</param>
        /// <param name="fileName">文件名</param>
        /// <param name="saveObject">保存数据类</param>
        public static void SaveObjectToJson<TSaveObject>(string directory, string fileName, TSaveObject saveObject) {
            SaveText(directory, fileName, JsonTool.ToJson(saveObject));
        }
        /// <summary>加载Class</summary>
        /// <typeparam name="TSaveObject">读取的类型</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <returns>读取数据类</returns>
        public static TSaveObject LoadJsonToObject<TSaveObject>(string filePath) {
            string json = LoadText(filePath);
            if (json != null) { return JsonTool.FromJson<TSaveObject>(json); }
            else { return default(TSaveObject); }
        }
        #endregion
        #region 对象Encoding的保存与加载
        /// <summary>编码保存Object</summary>
        /// <param name="directory">文件夹</param>
        /// <param name="fileName">文件名</param>
        /// <param name="saveObject">保存数据类</param>
        /// <param name="encodeType">编码类型</param>
        public static void SaveEncodingObject<TSaveObject>(string directory, string fileName, TSaveObject saveObject, Encoding encodeType) {
            SaveEncodingString(directory, fileName, JsonTool.ToJson(saveObject), encodeType);
        }
        /// <summary>加载编码Object</summary>
        /// <typeparam name="TSaveObject">读取的类型</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <param name="encodeType">编码类型</param>
        /// <returns>读取数据类</returns>
        public static TSaveObject LoadEncodingObject<TSaveObject>(string filePath, Encoding encodeType) {
            string json = LoadEncodingString(filePath, encodeType);
            if (json != null) { return JsonTool.FromJson<TSaveObject>(json); }
            else { return default(TSaveObject); }
        }
        #endregion
    }
}