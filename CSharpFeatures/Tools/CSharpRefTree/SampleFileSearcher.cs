using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Allen.Util.CSharpRefTree
{
    /// <summary>
    /// 简单文件搜索
    /// </summary>
    public class SampleFileSearcher
    {
        /// <summary>
        /// 搜索文件，返回绝对路径
        /// </summary>
        /// <param name="searchingPah"></param>
        /// <param name="filter"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        public static List<string> SearchFiles(string searchingPah, string filter, bool isRecursive = true)
        {
            var result = new List<string>();

            var dirs = searchingPah.Split('|');
            foreach (var dir in dirs)
            {
                if (!Directory.Exists(dir))
                {
                    Console.WriteLine($"File Searching Paht ：{dir} not exist");
                    continue;
                }
                var rootDir = new DirectoryInfo(dir);

                var patterns = filter.Split('|');
                foreach (var pattern in patterns)
                {
                    result.AddRange(rootDir.GetFiles(pattern,
                        isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                        .Select(file => file.FullName));
                }
            }
            return result;
        }
    }
}
