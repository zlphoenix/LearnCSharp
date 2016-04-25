using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Allen.Util.CSharpRefTree
{
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

        public PrjInfo(string csprjPath)
        {

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
        public string ProjectId { get; set; }
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
        public string PrjFullName => Path.Combine(PrjFilePath ?? "", PrjFileName ?? "");
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
}