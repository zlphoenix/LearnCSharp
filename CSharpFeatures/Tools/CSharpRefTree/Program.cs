using SolutionMaker.Core;
using SolutionMaker.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.String;

namespace Inspur.GSP.Bom.Builder
{
    /// <summary>
    /// 希望通过解析项目文件中的引用关系,建立build顺序
    /// </summary>
    static class Program
    {
        public static Dictionary<string, PrjInfo> PrjInfoDic;
        public static Dictionary<string, PrjInfo> PrjInfoFileNameDic;
        public static List<PrjInfo> ErrorPrjInfo;
        public static List<PrjInfo> Root;
        public static List<string> CycleRef = new List<string>();
        public static List<string> RefStageError = new List<string>();

        public static List<string> LostAssembly = new List<string>();
        public static BomBuildOption BomBuildOption;
        private static readonly List<string> IgnoreRefPrefix = new List<string>
        {
            "Microsoft", "System",
            "stdole","Accessibility",
            "sysglobl","UIAutomationClient",
            "PresentationCore","PresentationFramework","WindowsBase","adodb"
        };


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //ProjectPath = EverythingFileSearcher.Searcher.Search(string.Format(@"{0}\Ref\bin !{0}\Ref\bin\3rd (.dll|.exe)", InitPath)).ToList();
            Init(args);
            //GenPrjFromRefBin();



            var ciPrjs = GetPrjGroupFromConfig(BomBuildOption.CiProjectListConfigPath);

            foreach (var group in ciPrjs)
            {
                RebuildRef(group);
            }

            foreach (var group in ciPrjs)
            {
                Util.Log($"++++++++Stage:{group.Key},Prj Count:{group.Count()}++++++++");
                PrintAssembly(group);
            }

            if (RefStageError.Count != 0)
            {
                Util.Log(RefStageError.OutputList("Find Ref Stage Error:"));
                //return 2;
            }
            if (LostAssembly.Count != 0)
            {
                Util.Log(LostAssembly.OrderBy(item => item).ToList().OutputList("Find LostRef Error:\n"));
                //return 2;
            }
            if (CycleRef.Count != 0)
            {
                Util.Log(CycleRef.OutputList("Find CycleRef:"));
                return 1;
            }
            return 0;

        }

        private static void Init(string[] args)
        {
            BomBuildOption = new BomBuildOption(args);

            PrjInfoDic = new Dictionary<string, PrjInfo>();
            PrjInfoFileNameDic = new Dictionary<string, PrjInfo>();
            ErrorPrjInfo = new List<PrjInfo>();
            Root = new List<PrjInfo>();
            IgnoreRefPrefix.AddRange(SampleFileSearcher.SearchFiles($@"{BomBuildOption.InitPath}\Ref\Bin\3rd", "*.dll")
           .Select(item => item.Split('\\').Last().Split('.').First()));
        }

        private static void RebuildRef(IGrouping<string, PrjInfo> @group)
        {
            foreach (var prjInfo in @group)
            {
                if (prjInfo.OriginalRef.Count == 0)
                    Root.Add(prjInfo);

                foreach (var originalRef in prjInfo.OriginalRef)
                {
                    if (originalRef.EndsWith(".csproj"))
                    {
                        var refPrjPath = Path.GetFullPath(Path.Combine(prjInfo.PrjFilePath, originalRef));
                        var refPrj = GetPrjByPath(refPrjPath);
                        if (refPrj == null)
                        {
                            var errorMsg = $" {refPrjPath} prj file Not found,\t ref by {prjInfo.ShortPrjPath}";
                            prjInfo.RefError.Add(errorMsg);
                            Util.Log(errorMsg);
                            if (!LostAssembly.Contains(errorMsg))
                            {
                                LostAssembly.Add(errorMsg);
                            }
                        }
                        else
                        {
                            RefPrj(refPrj, prjInfo);
                        }
                    }
                    else
                    {
                        var refAssembly = originalRef.Split(',').FirstOrDefault();

                        //ignore well known ref
                        if (IgnoreRefPrefix.Any(item => refAssembly.StartsWith(item, StringComparison.OrdinalIgnoreCase)) || IsNullOrEmpty(refAssembly))
                        {
                            continue;
                        }
                        var refPrj = GetPrjByAssembly(refAssembly);
                        if (refPrj == null)
                        {

                            var errorMsg = $"{refAssembly} dll file Not found,\t ref by {prjInfo.ShortPrjPath}";
                            prjInfo.RefError.Add(errorMsg);
                            Util.Log($"Prj {prjInfo.ShortPrjPath}\t{errorMsg}", LogLevel.Error);

                            if (!LostAssembly.Contains(errorMsg))
                            {
                                LostAssembly.Add(errorMsg);
                            }
                        }
                        else
                        {
                            RefPrj(refPrj, prjInfo);
                        }
                    }
                }
            }
        }

        private static PrjInfo GetPrjByPath(string refPrjPath)
        {

            PrjInfo prjInfo;
            PrjInfoFileNameDic.TryGetValue(refPrjPath.ToLower(), out prjInfo);
            return prjInfo;
        }

        private static PrjInfo GetPrjByAssembly(string assemblyName)
        {

            PrjInfo prjInfo;
            PrjInfoDic.TryGetValue(assemblyName.ToLower(), out prjInfo);
            return prjInfo;
        }
        private static void RefPrj(PrjInfo refPrj, PrjInfo prjInfo)
        {
            if (CompareOrdinal(refPrj.BuildStage, prjInfo.BuildStage) > 0)
            {
                var errorMsg =
                    $" Prj on stage: {prjInfo.BuildStage.PadRight(13, ' ')} ref {refPrj.BuildStage.PadRight(13, ' ')}.\tSrc:\t{prjInfo.PrjFullName},RefPrj:\t{refPrj.PrjFullName}";
                Util.Log($"Ref Stage Error:{errorMsg}", LogLevel.Error);
                RefStageError.Add(errorMsg);
            }
            else
            {
                prjInfo.Ref(refPrj);
            }
        }


        /// <summary>
        /// 以BuildStage分组的PrjInfo
        /// </summary>
        /// <param name="ciProjectListConfigPath"></param>
        /// <returns></returns>
        private static List<IGrouping<string, PrjInfo>> GetPrjGroupFromConfig(string ciProjectListConfigPath)
        {

            if (!File.Exists(ciProjectListConfigPath))
                throw new Exception($"CI Project List File not found from Path:{ciProjectListConfigPath}！");

            var prjGroup = File.ReadAllLines(ciProjectListConfigPath)
                       .Skip(1)
                       .Select(x => x.Split(','))
                       .Select(CreatPrjInfoFromCiConfig)
                       .Where(item => item != null)
                       .GroupBy(prj => prj.BuildStage)
                       .OrderBy(item => item.Key)
                       .ToList();

            return prjGroup;
        }

        private static PrjInfo CreatPrjInfoFromCiConfig(string[] x)
        {
            //不需要参与Build
            if (x[5] != "True")
                return null;
            var prjFullPath = Path.Combine(BomBuildOption.InitPath, x[2]);
            if (!File.Exists(prjFullPath))
            {
                Util.Log($"Project File {prjFullPath} Not Exsits", LogLevel.Error);
                return null;
            }

            var prj = new PrjInfo()
            {
                DevGroup = x[0],
                Module = x[1],
                PrjFullName = prjFullPath,
                AssemblyName = x[3],
                BuildStage = x[4],
            };
            GetRef(prj);
            AddToDic(prj);
            return prj;
        }

        private static void AddToDic(PrjInfo prj)
        {
            PrjInfoDic.Add(prj.AssemblyName.ToLower(), prj);
            PrjInfoFileNameDic.Add(prj.PrjFullName.ToLower(), prj);

            if (prj.OriginalRef.Count == 0)
                Root.Add(prj);
        }

        private static void GetRef(PrjInfo prj)
        {
            //var pa = new ProjectAnalyzer(prj.PrjFullName);
            //prj.ProjectId = pa.GetProjectId().ToString();
            //prj.OriginalRef = pa.GetProjectReferences()?.Select(item => item.Name).ToList();
            var refFilter = new Regex("<.*Reference Include=\"(.*)\"", RegexOptions.IgnoreCase);
            using (var reader = File.OpenText(prj.PrjFullName))
            {
                while (!reader.EndOfStream)
                {
                    var text = reader.ReadLine();
                    var match = refFilter.Match(text);

                    if (!match.Success) continue;

                    prj.OriginalRef.Add(match.Groups[1].Value);

                }
            }
        }

        private static void PrintAssembly(IGrouping<string, PrjInfo> @group)
        {
            string buildStage = @group.Key;
            //Error
            foreach (var prjInfo in @group.Where(item => item.RefError.Count > 0))
            {
                Util.Log(prjInfo.ToString());
            }

            Util.Log(LostAssembly.OutputList("Lost Assemblies:\n"), LogLevel.Error);
            var thisStagelDic = @group.ToDictionary(item => item.AssemblyName, item => item);
            int level = 0;
            //Console.WriteLine(Root.OutputList($"-------------Level {level}-------------\n", item => $"{item.AssemblyName},{item.PrjFullName}\n"));
            //Root.OrderBy(item => item.Module).ToList().ForEach(item => PrjInfoDic.Remove(item.AssemblyName));
            //level++;
            while (thisStagelDic.Count > 0)
            {
                //var count = PrjInfoDic.Count;
                var thisLevel = thisStagelDic.Values
                    .Where(item => (!item.PrjRef.Exists(refItem => !Root.Contains(refItem))))
                    .OrderBy(item => item.Module)
                    .ToList();
                if (thisLevel.Count == 0)
                {
                    var recursivePrj = DealRecursiveRef(thisStagelDic);

                    if (recursivePrj == null)
                    {
                        Util.Log("Failed to remove cycle!", LogLevel.Error);
                        break;
                    }
                    else
                    {
                        thisLevel.Add(recursivePrj);
                    }
                }
                Util.Log(thisLevel.OutputList($"-------------{buildStage}.Level {level},Count:{thisLevel.Count}-------------\n", item => $"{item.AssemblyName},{item.PrjFullName}\n"));
                Root.AddRange(thisLevel);
                thisLevel.ForEach(item => thisStagelDic.Remove(item.AssemblyName));
                GenSln(thisLevel, level, buildStage == null ? null : Path.Combine(BomBuildOption.SlnPath, buildStage));
                level++;
            }

            Util.Log(thisStagelDic.Values.ToList().OrderBy(item => item.Module).ToList().OutputList($"-------------{buildStage},Not Reachable,Count:{thisStagelDic.Count}-------------\n"
            , item => item.AssemblyName + "\n"));
            foreach (var notReachableAss in thisStagelDic)
            {
                Util.Log(
              notReachableAss.Value.PrjRef
                    .Where(item => !Root.Contains(item))
                    .Select(item => item.AssemblyName)
                    .ToList()
                    .OutputList(notReachableAss.Key + " Ref not in builded list\n", item => "\t" + item + "\n")
                    , LogLevel.Error);
            }

        }
        public static void GenSln(List<PrjInfo> thisLevel, int level, string slnDir = null)
        {
            if (IsNullOrEmpty(slnDir)) slnDir = BomBuildOption.SlnPath;
            if (!Directory.Exists(slnDir)) Directory.CreateDirectory(slnDir);

            var prjs = thisLevel.Where(item => !IsNullOrEmpty(item.PrjFilePath)).ToList();
            if (prjs.Count == 0) return;
            var generator = new SolutionGenerator(new ConsoleLogger());
            var firstPrj = prjs.First();
            var slnOpt = new SolutionOptions
            {
                SolutionFolderPath = Path.Combine(slnDir,
                    $"{firstPrj.BuildStage}BuildSln{level.ToString().PadLeft(3, '0')}.sln"),
                SolutionFileVersion = SolutionFileVersion.VisualStudio2012,
                ProjectRootFolderPath = firstPrj.PrjFilePath
            };

            generator.GenerateSolution(slnOpt.SolutionFolderPath, slnOpt);

            prjs.RemoveAt(0);

            foreach (var prj in prjs)
            {
                slnOpt.UpdateMode = SolutionUpdateMode.Add;
                slnOpt.ProjectRootFolderPath = prj.PrjFilePath;
                generator.GenerateSolution(slnOpt.SolutionFolderPath, slnOpt);

            }
        }

        private static PrjInfo DealRecursiveRef(Dictionary<string, PrjInfo> thisStageDic)
        {
            var path = new Stack<PrjInfo>(thisStageDic.Count);
            foreach (var prjInfo in thisStageDic.Values.ToList())
            {
                //内部会打断循环引用，会影响到集合的成员
                if (Root.Contains(prjInfo))
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
            foreach (var refAssembly in prjInfo.PrjRef.Where(item => (!Root.Contains(item))))
            {

                if (path.Contains(refAssembly))
                {
                    //发现环
                    var cycleRefError = new StringBuilder();
                    cycleRefError.Append(refAssembly.AssemblyName).Append("<-");
                    while (path.Peek() != refAssembly)
                    {
                        cycleRefError.Append(path.Pop().AssemblyName).Append("<-");
                    }

                    cycleRefError.Append(refAssembly.AssemblyName);
                    CycleRef.Add(cycleRefError.ToString());
                    cycleRefError.Append("\t Resolve it?[Y/n]\n");
                    Util.Log(cycleRefError, LogLevel.Error);

                    //var decide = Console.ReadLine();
                    //if (decide == "Y")
                    //{
                    //Root.Add(refAssembly);
                    //PrjInfoDic.Remove(refAssembly.AssemblyName);
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

        //private static void AddToDics(PrjInfo prjInfo)
        //{
        //    if (!PrjInfoDic.ContainsKey(prjInfo.AssemblyName))
        //    {
        //        PrjInfoDic.Add(prjInfo.AssemblyName, prjInfo);
        //    }
        //    PrjInfoFileNameDic.Add(prjInfo.AssemblyName, prjInfo);

        //    if (prjInfo.OriginalRef.Count == 0)
        //        Root.Add(prjInfo);
        //}


        //public static PrjInfo CreatePrjInfoFromAssemblyPath(string prj)
        //{
        //    string dllName = GetFileNameWithoutExt(prj);
        //    if (PrjInfoDic.ContainsKey(dllName))
        //    {

        //        Console.WriteLine($"{dllName}\t{prj} load\t {PrjInfoDic[dllName].AssemblyPath} before ");
        //        return null; //已经添加过
        //    }
        //    var ass = Assembly.ReflectionOnlyLoadFrom(prj);
        //    var prjInfo = new PrjInfo(ass);
        //    prjInfo.AssemblyPath = prj;
        //    AddToDics(prjInfo);

        //    return prjInfo;
        //}
        //public static PrjInfo CreatePrjInfoFromCsproj(string prj)
        //{
        //    var pa = new ProjectAnalyzer(prj);

        //    string dllName = pa.GetAssemblyName();
        //    if (PrjInfoDic.ContainsKey(dllName))
        //    {

        //        Console.WriteLine($"{dllName}\t{prj} load\t {PrjInfoDic[dllName].AssemblyPath} before ");
        //        return null; //已经添加过
        //    }
        //    //var ass = Assembly.ReflectionOnlyLoadFrom(prj);
        //    var prjInfo = new PrjInfo(pa) { AssemblyPath = prj };
        //    AddToDic(prjInfo);
        //    return prjInfo;
        //}

        //private static string GetFileNameWithoutExt(string prj)
        //{
        //    var fileName = prj.Split('\\').LastOrDefault();
        //    return fileName.Substring(0, fileName.Length - 4);
        //}
    }

}
