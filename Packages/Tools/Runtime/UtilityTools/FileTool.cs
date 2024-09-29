using System;
using System.Runtime.InteropServices;

namespace MuHua {
    public static class FileTool {
        #region 选择文件路径
        public static bool OpenFile(string title, out string path, params string[] type) {
            FileDialog fd = new FileDialog();
            fd.structSize = Marshal.SizeOf(fd);
            fd.filter = FileType(type);
            fd.file = new string(new char[256]);
            fd.maxFile = fd.file.Length;
            fd.fileTitle = new string(new char[64]);
            fd.maxFileTitle = fd.fileTitle.Length;
            fd.initialDir = "C:/";
            fd.title = title;
            fd.defExt = type[0];
            fd.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
            bool result = GetOpenFileName(fd);
            path = fd.file;
            return result;
        }
        public static string FileType(params string[] array) {
            string type = "";
            foreach (var item in array) {
                type += "文件(*." + item + ")\0*." + item + "\0";
            }
            return type;
        }
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] FileDialog fileDialog);
        #endregion
        #region 打开文件夹
        [DllImport("Shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        private static extern IntPtr SHBrowseForFolder([In, Out] BrowseFolder browseFolder);
        [DllImport("Shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool SHGetPathFromIDList([In] IntPtr dlist, [In] char[] path);
        public static string BrowseForFolder(string title = "Path") {
            BrowseFolder temp = new BrowseFolder();
            temp.pszDisplayName = new string(new char[2048]);
            temp.lpszTitle = title;
            temp.ulFlags = 0x00000040;
            IntPtr a = SHBrowseForFolder(temp);
            char[] path = new char[2048];
            for (int i = 0; i < 2048; i++) { path[i] = '\0'; }
            SHGetPathFromIDList(a, path);
            string res = new string(path);
            return res.Substring(0, res.IndexOf('\0'));
        }
        #endregion
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class FileDialog {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public string filter = null;//筛选文件类型
        public string customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public string file = null;
        public int maxFile = 0;
        public string fileTitle = null;
        public int maxFileTitle = 0;
        public string initialDir = null;//默认路径
        public string title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public string defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public string templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class BrowseFolder {
        public IntPtr hwndOwner = IntPtr.Zero;
        public IntPtr pidlRoot = IntPtr.Zero;
        public String pszDisplayName = null;
        public String lpszTitle = null;
        public UInt32 ulFlags = 0;
        public IntPtr lpfn = IntPtr.Zero;
        public IntPtr lParam = IntPtr.Zero;
        public int iImage = 0;
    }
}