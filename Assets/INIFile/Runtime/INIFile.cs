using System.Runtime.InteropServices;
using System.Text;

public static class INIFile {
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder stringBuilder, int size, string filePath);

    /// <summary>
    /// 写入ini文件数据
    /// </summary>
    /// <param name="section">节点名称</param>
    /// <param name="key">键名</param>
    /// <param name="value">键值</param>
    /// <param name="filePath">文件路径</param>
    public static void Write(string section, string key, string value, string filePath) {
        WritePrivateProfileString(section, key, value, filePath);
    }
    /// <summary>
    /// 读取ini文件数据
    /// </summary>
    /// <param name="section">节点名称</param>
    /// <param name="key">键名</param>
    /// <param name="filePath">文件路径</param>
    /// <returns>返回键值</returns>
    public static string Read(string section, string key, string filePath) {
        StringBuilder stringBuilder = new StringBuilder(255);
        GetPrivateProfileString(section, key, "", stringBuilder, 255, filePath);
        return stringBuilder.ToString();
    }
}
