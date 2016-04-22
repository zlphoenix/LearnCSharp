using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using Microsoft.Win32;

namespace SolutionMaker.Core
{
    public static class Utils
    {
        private static SolutionMaker.Core.Model.SolutionFileVersion _preferredSolutionFileVersion = Model.SolutionFileVersion.None;

        public static bool PathHasMatch(string path, string pattern)
        {
            string wildcardPattern = @"([\w|*|.|\\|-]+)";
            string filename;

            string filter = pattern;
            if (filter.IndexOf(Path.DirectorySeparatorChar) < 0)
            {
                // Filter by filename
                filter = pattern.Replace("*", wildcardPattern);
                filename = Path.GetFileNameWithoutExtension(path);
            }
            else
            {
                // Filter by full path
                filter = "[a-zA-Z]:" + wildcardPattern + pattern.Replace("\\", "\\\\").Replace("*", wildcardPattern);
                filename = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
            }

            string filterRegex = "^" + filter + "$";
            var nameRegex = new Regex(filterRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            var nameMatch = nameRegex.Match(filename);
            if (nameMatch.Success)
            {
                return true;
            }

            return false;
        }

        public static string GetRelativePath(string pathFrom, string pathTo)
        {
            var fi = new FileInfo(pathFrom);
            int attr = (int)(fi.Attributes & FileAttributes.Directory);
            var retval = new StringBuilder(259);

            bool ok = NativeMethods.PathRelativePathTo(retval, pathFrom, attr, pathTo, attr);
            if (!ok)
            {
                throw new ArgumentException(string.Format("Unable to compute PathRelativePathTo from {0} to {1}", pathFrom, pathTo));
            }
            return retval.ToString();
        }

        public static Model.SolutionFileVersion GetPreferredSolutionFileVersion()
        {
            if (_preferredSolutionFileVersion == Model.SolutionFileVersion.None)
            {
                string dteVersion = Registry.GetValue(@"HKEY_CLASSES_ROOT\VisualStudio.DTE\CurVer", string.Empty, string.Empty).ToString();
                switch (dteVersion)
                {
                    case "VisualStudio.DTE.9.0":
                        _preferredSolutionFileVersion = Model.SolutionFileVersion.VisualStudio2008;
                        break;
                    default:

                    case "VisualStudio.DTE.10.0":
                        _preferredSolutionFileVersion = Model.SolutionFileVersion.VisualStudio2010;
                        break;

                    case "VisualStudio.DTE.11.0":
                        _preferredSolutionFileVersion = Model.SolutionFileVersion.VisualStudio2012;
                        break;
                }
            }

            return _preferredSolutionFileVersion;
        }
    }
}
