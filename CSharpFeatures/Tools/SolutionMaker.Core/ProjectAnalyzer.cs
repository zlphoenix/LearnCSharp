using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SolutionMaker.Core.Model;

namespace SolutionMaker.Core
{
    public class ProjectAnalyzer
    {
        private static readonly XNamespace _msBuildNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        private string _fullPath;

        public ProjectAnalyzer(string absolutePath)
        {
            _fullPath = Path.GetFullPath(absolutePath);
        }

        public bool IsProjectFile(SolutionFileVersion solutionFileVersion)
        {
            if (string.IsNullOrEmpty(Path.GetExtension(_fullPath)))
            {
                return false;
            }

            string fileExtension = Path.GetExtension(_fullPath).ToLower().Substring(1); // Remove leading "."
            var projectType = ProjectTypes.Find(fileExtension);
            if (projectType != null && (solutionFileVersion & projectType.SupportedVersions) != 0)
            {
                return GetProjectId() != Guid.Empty;
            }
            else
            {
                return false;
            }
        }

        public Guid GetProjectId()
        {
            var projectType = GetProjectType();
            if (projectType == null)
                return Guid.Empty;

            using (var reader = File.OpenText(_fullPath))
            {
                if (projectType.CustomFileFormat && !string.IsNullOrEmpty(projectType.ProjectGuidRegex))
                {
                    string text = reader.ReadToEnd();
                    string guidRegex = @"{(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})}";
                    string projectIdRegex = string.Format(projectType.ProjectGuidRegex, guidRegex);

                    var match = Regex.Match(text, projectIdRegex, RegexOptions.Multiline);
                    if (match.Success)
                    {
                        return new Guid(match.Groups["ProjectId"].Value);
                    }
                }
                else
                {
                    var doc = XDocument.Load(reader);
                    var element = doc.Descendants(_msBuildNamespace + "ProjectGuid").SingleOrDefault();
                    if (element != null)
                        return new Guid(element.Value);
                }
            }

            return Guid.Empty;
        }

        public string GetAssemblyName()
        {
            var projectType = GetProjectType();
            if (projectType == null)
                return string.Empty;

            using (var reader = File.OpenText(_fullPath))
            {
                if (projectType.CustomFileFormat && !string.IsNullOrEmpty(projectType.AssemblyNameRegex))
                {
                    string text = reader.ReadToEnd();

                    var match = Regex.Match(text, projectType.AssemblyNameRegex, RegexOptions.Multiline);
                    if (match.Success)
                    {
                        return match.Groups["AssemblyName"].Value;
                    }
                }
                else
                {
                    var doc = XDocument.Load(reader);
                    var element = doc.Descendants(_msBuildNamespace + "AssemblyName").SingleOrDefault();
                    if (element != null)
                        return element.Value;
                }
            }

            return string.Empty;
        }

        public IEnumerable<SolutionProject> GetProjectReferences()
        {
            var projectType = GetProjectType();
            if (projectType == null || projectType.CustomFileFormat)
                yield break;

            using (var reader = File.OpenText(_fullPath))
            {
                var doc = XDocument.Load(reader);
                foreach (var element in doc.Descendants(_msBuildNamespace + "ProjectReference"))
                {
                    var relativePath = element.Attribute("Include").Value;
                    var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(_fullPath), relativePath));
                    projectType = GetProjectType(path);
                    var projectTypeId = projectType == null ? Guid.Empty : projectType.ProjectGuid;
                    var projectId = new Guid(element.Descendants(_msBuildNamespace + "Project").Single().Value);
                    var name = element.Descendants(_msBuildNamespace + "Name").Single().Value;

                    yield return new SolutionProject
                                     {
                                         ProjectTypeId = projectTypeId, 
                                         ProjectId = projectId, 
                                         Name = name, 
                                         Path = path,
                                     };
                }
            }
        }

        private ProjectType GetProjectType()
        {
            return GetProjectType(_fullPath);
        }

        private ProjectType GetProjectType(string path)
        {
            string projectFileExtension = Path.GetExtension(path).Substring(1).ToLower();
            return ProjectTypes.Find(projectFileExtension);
        }
    }
}
