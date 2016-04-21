using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            AssemblyPath = EverythingFileSearcher.Searcher.Search(@"D:\Work\GSP\GSP6.1\Draft\Ref\bin !D:\Work\GSP\GSP6.1\Draft\Ref\bin\3rd (.dll|.exe)").ToList();
            Console.WriteLine("Total file count {0} ", AssemblyPath.Count);
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
            Console.WriteLine("Total aas count {0}", prjInfoDic.Count);
            RebuildRef();
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
            Console.WriteLine(root.OutputList($"-------------Level {level}-------------\n", item => item.AssemblyName + "\n"));
            root.OrderBy(item => item.Module).ToList().ForEach(item => prjInfoDic.Remove(item.AssemblyName));
            level++;
            while (prjInfoDic.Count > 0)
            {
                var count = prjInfoDic.Count;


                var thisLevel = prjInfoDic.Values
                    .Where(item => (!item.PrjRef.Exists(refItem => !root.Contains(refItem))))
                    .OrderBy(item => item.Module)
                    .ToList();
                Console.WriteLine(thisLevel.OutputList($"-------------Level {level}-------------\n", item => item.AssemblyName + "\n"));
                root.AddRange(thisLevel);
                thisLevel.ForEach(item => prjInfoDic.Remove(item.AssemblyName));

                if (count == prjInfoDic.Count)
                {
                    DealRecursiveRef();
                    if (count == prjInfoDic.Count)
                    {
                        Console.WriteLine("Failed to remove cycle!");
                        break;
                    }
                }
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

        private static void DealRecursiveRef()
        {
            var path = new Stack<PrjInfo>(prjInfoDic.Count);
            foreach (var prjInfo in prjInfoDic.Values.ToList())
            {
                //内部会打断循环引用，会影响到集合的成员
                if (root.Contains(prjInfo))
                    continue;
                if (Deal(prjInfo, path))
                {
                    path.Clear();
                }
            }
        }
        /// <summary>
        /// 深度优先探测环
        /// </summary>
        /// <param name="prjInfo"></param>
        /// <param name="path"></param>
        public static bool Deal(PrjInfo prjInfo, Stack<PrjInfo> path)
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
                    root.Add(refAssembly);
                    prjInfoDic.Remove(refAssembly.AssemblyName);
                    return true;
                    //}
                }
                path.Push(refAssembly);
                if (Deal(refAssembly, path)) return true;
                path.Pop();

            }
            return false;
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
            //prjInfoFileNameDic.Add(prjInfo.PrjFileName, prjInfo);
            if (prjInfo.OriginalRef.Count == 0) root.Add(prjInfo);
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

    internal class PrjInfo
    {
        public PrjInfo()
        {
            PrjRef = new List<PrjInfo>();
            OriginalRef = new List<string>();
            RefError = new List<string>();
            BeRefBy = new List<PrjInfo>();
        }

        public PrjInfo(Assembly ass) : this()
        {

            this.AssemblyName = ass.GetName().Name;

            var refAssemblies = ass.GetReferencedAssemblies().Where(IsMatch);
            if (refAssemblies.Count() == 0) return;
            foreach (var refAss in refAssemblies)
            {
                this.OriginalRef.Add(refAss.Name);
            }


        }

        private PrjInfo LoadAssByRefName(AssemblyName refAss)
        {
            var assPath = Program.AssemblyPath.FirstOrDefault(path => path.Contains(refAss.Name));
            if (string.IsNullOrEmpty(assPath))
            {
                this.RefError.Add($"Ref：{assPath} not found ");
                return null;
            }
            else
            {
                return Program.CreatePrjInfo(assPath);

            }
        }

        public static bool IsMatch(AssemblyName assemblyName)
        {
            var excludeNames = new string[] { "mscorlib", "DevExpress", "Newtonsoft" };
            if (excludeNames.Any(item => assemblyName.Name.StartsWith(item))
                || assemblyName.Name.StartsWith("System") || assemblyName.Name.StartsWith("Microsoft")) return false;
            if (assemblyName.Name.StartsWith("Inspur") || assemblyName.Name.StartsWith("Genersoft") || assemblyName.Name.Contains("GSP")) return true;


            //Console.WriteLine("AssName：{0} Could Not be recognized", assemblyName.Name);
            return false;
        }

        public string Module { get; set; }
        private string assemblyName;

        public string AssemblyName
        {
            get { return assemblyName; }
            set
            {
                assemblyName = value;
                if (assemblyName.StartsWith("Inspur") || assemblyName.StartsWith("Genersoft"))
                {
                    Module = assemblyName.Split('.')[2];
                }
                //else if (assemblyName.StartsWith("Genersoft"))
                //{
                //    Module = assemblyName.Split('.')[2]
                //}
            }
        }


        public string PrjFileName { get; set; }
        public string PrjFilePath { get; set; }
        public string AssemblyPath { get; set; }
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

        public List<PrjInfo> BeRefBy { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"Ass:{AssemblyName};" +
                   //$"\t{OriginalRef.OutputList("Ref")};" +
                   $"\t{RefError.OutputList("Error")}";
        }
        public string ToString(string str)
        {
            return $"Ass:{AssemblyName};" +
                   $"\t{OriginalRef.OutputList("Ref:")};" +
                   $"\t{RefError.OutputList("Error: ")}" +
                   "\n";
        }
    }

    public static class Util
    {
        public static StringBuilder OutputList<T>(this List<T> collection, string title, Func<T, string> print = null)
        {
            if (print == null)
                print = arg => arg.ToString() + ",";
            if (collection.Count == 0) return null;
            var refs = new StringBuilder(title);

            collection.ForEach(item => refs.Append(print(item)));
            return refs;
        }
    }

}
