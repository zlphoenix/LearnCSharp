using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

using SolutionMaker.Core.Model;

namespace SolutionMaker.Core
{
    [Serializable]
    public class SolutionOptions
    {
        public static readonly string CurrentVersion = "2.1";
        public static readonly string FileExtension = "slnmaker";

        private string _solutionFolderPath;
        private string _projectRootFolderPath;

        public SolutionOptions()
        {
            this.Recursive = true;
            this.SolutionFileVersion = Utils.GetPreferredSolutionFileVersion();
        }

        public string SchemaVersion { get { return CurrentVersion; } set { } }
        public string SolutionFolderPath
        {
            get { return _solutionFolderPath; }
            set { _solutionFolderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), value)); }
        }
        public string ProjectRootFolderPath
        {
            get { return _projectRootFolderPath; } 
            set
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), value);
                if (!Directory.Exists(path))
                    throw new ArgumentException(string.Format("Root project directory {0} does not exist.", path));
                _projectRootFolderPath = Path.GetFullPath(path);
            }
        }
        public string IncludeFilter { get; set; }
        public string ExcludeFilter { get; set; }
        public bool Recursive { get; set; }
        public bool IncludeReferences { get; set; }
        public string ProjectTypes { get; set; }
        public SolutionFileVersion SolutionFileVersion { get; set; }
        public SolutionUpdateMode UpdateMode { get; set; }
        public bool OverwriteReadOnlyFile { get; set; }
        public int SolutionFolderLevels { get; set; }
        public SolutionFolderNamingMode FolderNamingMode { get; set; }
        public int CommonPrefixLevels { get; set; }
        public string CommonPrefixes { get; set; }
    }
}
