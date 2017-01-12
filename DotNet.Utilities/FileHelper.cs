using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNet.Utilities
{
    public static class FileHelper
    {
        #region 根据路径读取文件内容

        /// <summary>
        /// 根据路径读取文件内容
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public static string FileReader(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
            {
                string s = sr.ReadToEnd();
                return s;
            }
        }

        #endregion 根据路径读取文件内容

        #region 把文本保存为文件

        /// <summary>
        /// 把文本保存为文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="strContent">写入文件的内容</param>
        /// <param name="isUtf8">是否是utf8</param>
        public static void FileSave(string filePath, string strContent, bool isUtf8 = false)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs, isUtf8 ? Encoding.UTF8 : Encoding.Default);
                sw.Write(strContent);
            }
        }

        #endregion 把文本保存为文件

        #region 向文件追加文本

        /// <summary>
        /// 向文件追加文本
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="strContent">写入文件的内容</param>
        /// <param name="isUtf8">是否是utf8</param>
        public static void FileAppend(string filePath, string strContent, bool isUtf8 = false)
        {
            if (!File.Exists(filePath))
            {
                FileSave(filePath, strContent, isUtf8);
                return;
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, isUtf8 ? Encoding.UTF8 : Encoding.Default);
                sw.Write(filePath);
            }
        }

        #endregion 向文件追加文本

        #region 遍历文件

        public static List<string> _Files = new List<string>();

        /// <summary>
        /// 遍历文件
        /// </summary>
        /// <param name="rootPath">文件夹目录</param>
        /// <returns></returns>
        public static List<string> FindFiles(string rootPath)
        {
            _Files.Clear();
            GetAllDirectories(rootPath);
            return _Files;
        }

        private static void GetAllDirectories(string rootPath)
        {
            string[] subPaths = System.IO.Directory.GetDirectories(rootPath);//得到所有子目录

            foreach (string path in subPaths)
            {
                GetAllDirectories(path);//对每一个字目录做与根目录相同的操作：即找到子目录并将当前目录的文件名存入List
            }

            string[] files = System.IO.Directory.GetFiles(rootPath);

            foreach (string file in files)
            {
                _Files.Add(file);//将当前目录中的所有文件全名存入文件List
            }
        }

        #endregion 遍历文件
    }
}