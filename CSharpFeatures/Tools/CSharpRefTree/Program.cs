using System;
using System.Collections.Generic;
using System.Linq;

namespace Allen.Util.CSharpRefTree
{
    /// <summary>
    /// 希望通过解析项目文件中的引用关系,建立build顺序
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine(":)");


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            var prjFileNames = EverythingFileSearcher.Searcher.Search(@"D:\Work\GSP\GSP6.1\Draft\Src,*.csproj");
            var prjDic = new Dictionary<string, PrjInfo>();
            var prjFileNameDic = new Dictionary<string, PrjInfo>();

            var refErrorPrj = new List<PrjInfo>();
            var root = new List<PrjInfo>();
            foreach (var prjInfo in prjFileNames.Select(CreatePrjInfo))
            {
                prjDic.Add(prjInfo.AssemblyName, prjInfo);
                prjFileNameDic.Add(prjInfo.PrjFileName, prjInfo);
                if (prjInfo.OriginalRef.Count == 0) root.Add(prjInfo);
            }

            foreach (var prjInfo in prjDic.Values)
            {
                var succeed = BuildRef(prjInfo);
                if (!succeed)
                    refErrorPrj.Add(prjInfo);

            }

        }

        private static bool BuildRef(PrjInfo prjInfo)
        {
            throw new NotImplementedException();
        }

        private static PrjInfo CreatePrjInfo(string prj)
        {
            throw new NotImplementedException();
        }
    }

    internal class PrjInfo
    {
        public string AssemblyName { get; set; }
        public string PrjFileName { get; set; }
        public string PrjFilePath { get; set; }
        /// <summary>
        /// 项目引用
        /// </summary>
        public List<PrjInfo> PrjRef { get; set; }
        /// <summary>
        /// 原始引用字符串
        /// </summary>
        public List<string> OriginalRef { get; set; }
        /// <summary>
        /// 错误的引用
        /// </summary>
        public List<string> RefError { get; set; }
    }
}
