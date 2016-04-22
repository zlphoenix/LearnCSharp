using Allen.Util.CSharpRefTree.Properties;
using SolutionMaker.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Allen.Util.CSharpRefTree
{
    /// <summary>
    /// 希望通过解析项目文件中的引用关系,建立build顺序
    /// </summary>
    static class Program
    {
        public static Dictionary<string, PrjInfo> prjInfoDic;
        public static Dictionary<string, PrjInfo> prjInfoFileNameDic;
        public static List<PrjInfo> errorPrjInfo;
        public static List<PrjInfo> root;
        public static List<string> AssemblyPath;
        public static List<string> LostAssembly = new List<string>();
        private static string InitPath = @"D:\Work\GSP\GSP6.1\Draft";
        private static string SlnPath = @"R:\Sln";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //AssemblyPath = EverythingFileSearcher.Searcher.Search(string.Format(@"{0}\Ref\bin !{0}\Ref\bin\3rd (.dll|.exe)", InitPath)).ToList();
            AssemblyPath = SampleFileSearcher.SearchFiles($@"{InitPath}\Ref\Bin\Lib|{InitPath}\Ref\Bin\Impl", "*.dll|*.exe");
            Console.WriteLine(Resources.Program_Main_Total_file_count__0__, AssemblyPath.Count);
            prjInfoDic = new Dictionary<string, PrjInfo>();
            prjInfoFileNameDic = new Dictionary<string, PrjInfo>();
            errorPrjInfo = new List<PrjInfo>();
            root = new List<PrjInfo>();
            AssemblyPath.ForEach(assPath =>
            {
                if (!assPath.EndsWith("dll") && !assPath.EndsWith("exe")) return;
                CreatePrjInfo(assPath);
            }
            );
            Console.WriteLine(Resources.Program_Main_Total_aasembly_count__0_, prjInfoDic.Count);
            RebuildRef();


            var csprojFilePathList = SampleFileSearcher.SearchFiles($"{InitPath}\\Src", "*.csproj");


            foreach (var csprjFile in csprojFilePathList)
            {
                var analyzer = new ProjectAnalyzer(csprjFile);
                var assName = analyzer.GetAssemblyName();

                if (string.IsNullOrEmpty(assName))
                {
                    Console.WriteLine($"Prj File {csprjFile} could not output a assembly");
                    continue;
                }
                if (!prjInfoFileNameDic.ContainsKey(assName))
                {
                    Console.WriteLine($"Prj File {csprjFile} output assembly：{assName} did not registed");
                    continue;
                }
                var pf = new FileInfo(csprjFile);
                var prjInfo = prjInfoFileNameDic[assName];
                prjInfo.PrjFilePath = pf.DirectoryName;
                prjInfo.PrjFileName = pf.Name;


            }
            PrintAss();


        }

        private static void PrintAss()
        {
            foreach (var prjInfo in prjInfoDic.Values.Where(item => item.RefError.Count > 0))
            {
                Console.WriteLine(prjInfo.ToString());
            }

            Console.WriteLine(LostAssembly.OutputList("Lost Assemblies"));

            int level = 0;
            Console.WriteLine(root.OutputList($"-------------Level {level}-------------\n", item => $"{item.AssemblyName},{item.PrjFullName}\n"));
            root.OrderBy(item => item.Module).ToList().ForEach(item => prjInfoDic.Remove(item.AssemblyName));
            level++;
            while (prjInfoDic.Count > 0)
            {
                var count = prjInfoDic.Count;


                var thisLevel = prjInfoDic.Values
                    .Where(item => (!item.PrjRef.Exists(refItem => !root.Contains(refItem))))
                    .OrderBy(item => item.Module)
                    .ToList();
                if (thisLevel.Count == 0)
                {
                    var recursivePrj = DealRecursiveRef();

                    if (recursivePrj == null)
                    {
                        Console.WriteLine("Failed to remove cycle!");
                        break;
                    }
                    else
                    {
                        thisLevel.Add(recursivePrj);
                    }
                }
                Console.WriteLine(thisLevel.OutputList($"-------------Level {level}-------------\n", item => $"{item.AssemblyName},{item.PrjFullName}\n"));
                root.AddRange(thisLevel);
                thisLevel.ForEach(item => prjInfoDic.Remove(item.AssemblyName));


                GenSln(thisLevel, level);


                level++;
            }

            Console.WriteLine(prjInfoDic.Values.ToList().OrderBy(item => item.Module).ToList().OutputList("-------------Not Reachable-------------\n"
            , item => item.AssemblyName + "\n"));
            foreach (var notReachableAss in prjInfoDic)
            {
                Console.WriteLine(
              notReachableAss.Value.PrjRef
                    .Where(item => !root.Contains(item))
                    .Select(item => item.AssemblyName)
                    .ToList()
                    .OutputList(notReachableAss.Key + " Ref not in builded list\n", item => "\t" + item + "\n")
                    );
            }

        }

        private static void GenSln(List<PrjInfo> thisLevel, int level)
        {
            var prjs = thisLevel.Where(item => !string.IsNullOrEmpty(item.PrjFilePath)).ToList();
            if (prjs.Count == 0) return;
            var generator = new SolutionGenerator(new ConsoleLogger());
            var slnOpt = new SolutionOptions();
            slnOpt.SolutionFolderPath = SlnPath + level.ToString().PadLeft(3, '0');
            slnOpt.ProjectRootFolderPath = prjs.FirstOrDefault().PrjFilePath;
            generator.GenerateSolution(slnOpt.SolutionFolderPath, slnOpt);

            prjs.RemoveAt(0);

            foreach (var prj in prjs)
            {
                slnOpt.UpdateMode = SolutionUpdateMode.Add;
                //slnOpt.
                //TODO Add prj
            }




        }

        private static PrjInfo DealRecursiveRef()
        {
            var path = new Stack<PrjInfo>(prjInfoDic.Count);
            foreach (var prjInfo in prjInfoDic.Values.ToList())
            {
                //内部会打断循环引用，会影响到集合的成员
                if (root.Contains(prjInfo))
                    continue;
                var prj = Deal(prjInfo, path);
                if (prj != null)
                {
                    path.Clear();
                    return prj;
                }
            }
            return null;
        }
        /// <summary>
        /// 深度优先探测环
        /// </summary>
        /// <param name="prjInfo"></param>
        /// <param name="path"></param>
        public static PrjInfo Deal(PrjInfo prjInfo, Stack<PrjInfo> path)
        {
            foreach (var refAssembly in prjInfo.PrjRef.Where(item => (!root.Contains(item))))
            {

                if (path.Contains(refAssembly))
                {
                    //发现环
                    Console.Write(refAssembly.AssemblyName + "<-");
                    while (path.Peek() != refAssembly)
                    {
                        Console.Write(path.Pop().AssemblyName + "<-");
                    }
                    Console.Write(refAssembly.AssemblyName + "\t Resolve it?[Y/n]\n");
                    //var decide = Console.ReadLine();
                    //if (decide == "Y")
                    //{
                    //root.Add(refAssembly);
                    //prjInfoDic.Remove(refAssembly.AssemblyName);
                    return refAssembly;
                    //}
                }
                path.Push(refAssembly);
                var dealedPrj = Deal(refAssembly, path);
                if (dealedPrj != null) return dealedPrj;
                path.Pop();

            }
            return null;
        }
        private static void RebuildRef()
        {
            foreach (var prjInfo in prjInfoDic.Values)
            {
                if (prjInfo.OriginalRef.Count == 0)
                    continue;

                foreach (var refAssName in prjInfo.OriginalRef)
                {
                    if (prjInfoDic.ContainsKey(refAssName))
                    {
                        var refAss = prjInfoDic[refAssName];
                        prjInfo.PrjRef.Add(refAss);
                        refAss.BeRefBy.Add(prjInfo);
                    }
                    else
                    {
                        if (!LostAssembly.Contains(refAssName))
                            LostAssembly.Add(refAssName);
                        prjInfo.RefError.Add(refAssName);
                    }
                }
            }
        }

        private static void AddToDics(PrjInfo prjInfo)
        {
            if (!prjInfoDic.ContainsKey(prjInfo.AssemblyName))
            {
                prjInfoDic.Add(prjInfo.AssemblyName, prjInfo);
            }
            prjInfoFileNameDic.Add(prjInfo.AssemblyName, prjInfo);

            if (prjInfo.OriginalRef.Count == 0)
                root.Add(prjInfo);
        }


        public static PrjInfo CreatePrjInfo(string prj)
        {
            string dllName = GetFileName(prj);
            if (prjInfoDic.ContainsKey(dllName))
            {

                Console.WriteLine($"{dllName}\t{prj} load\t {prjInfoDic[dllName].AssemblyPath} before ");
                return null; //已经添加过
            }
            var ass = Assembly.ReflectionOnlyLoadFrom(prj);
            var prjInfo = new PrjInfo(ass);
            prjInfo.AssemblyPath = prj;
            AddToDics(prjInfo);

            return prjInfo;
        }

        private static string GetFileName(string prj)
        {
            var fileName = prj.Split('\\').LastOrDefault();
            return fileName.Substring(0, fileName.Length - 4);
        }
    }
}
