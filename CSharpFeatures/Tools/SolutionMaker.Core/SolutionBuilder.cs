using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using SolutionMaker.Core.Model;

namespace SolutionMaker.Core
{
    public class SolutionBuilder
    {
        private SolutionOptions _options;
        private ILogger _logger;

        public int NumberOfProjectsAdded { get; set; }
        public int NumberOfProjectsRemoved { get; set; }

        public SolutionBuilder(SolutionOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        public Solution Build(Solution oldSolution, IList<SolutionProject> projects)
        {
            this.NumberOfProjectsAdded = 0;
            this.NumberOfProjectsRemoved = 0;

            var addProject = new Action<Solution, SolutionProject>((s, p) =>
                {
                    s.Projects.Add(p);
                    ++this.NumberOfProjectsAdded;
                    _logger.Write(string.Format("Added project {0}", p.Name));
                });

            var removeProject = new Action<Solution, SolutionProject>((s, p) =>
            {
                s.Projects.Remove(p);
                _logger.Write(string.Format("Removed project {0}", p.Name));
                ++this.NumberOfProjectsRemoved;
            });

            Solution newSolution;
            if (oldSolution == null || _options.UpdateMode == SolutionUpdateMode.Replace)
            {
                newSolution = new Solution();
                newSolution.FileVersion = _options.SolutionFileVersion;

                foreach (var project in projects)
                {
                    addProject(newSolution, project);
                }
            }
            else
            {
                newSolution = oldSolution.Clone() as Solution;

                foreach (var project in projects)
                {
                    if (!newSolution.Projects.Contains(project))
                    {
                        addProject(newSolution, project);
                    }
                }

                if (_options.UpdateMode == SolutionUpdateMode.AddRemove)
                {
                    var projectsToRemove = new List<SolutionProject>();
                    foreach (var project in newSolution.Projects)
                    {
                        if (ProjectTypes.Find(project.ProjectTypeId) != null && !projects.Contains(project))
                        {
                            projectsToRemove.Add(project);
                        }
                    }

                    foreach (var project in projectsToRemove)
                    {
                        removeProject(newSolution, project);
                    }
                }
            }

            if (_options.SolutionFolderLevels != 0)
            {
                MergeSolutionFolders(newSolution);
            }

            return newSolution;
        }

        private void MergeSolutionFolders(Solution solution)
        {
            var solutionFolders = GenerateSolutionFolders(solution);
            foreach (var project in solution.Projects)
            {
                if (project.IsFolder)
                {
                    string solutionFolderPath = solution.GetSolutionFolderPath(project);
                    if (solutionFolders.Contains(solutionFolderPath))
                    {
                        solutionFolders.Remove(solutionFolderPath);
                    }
                }
            }

            // Add new solution folders that are not present in current solution
            var solutionFolderMap = new Dictionary<string, Guid>();
            foreach (var solutionFolder in solutionFolders)
            {
                SolutionProject parentProject = null;
                int index = solutionFolder.LastIndexOf('.');
                if (index > 0)
                {
                    parentProject = solution.FindFolderWithPath(solutionFolder.Substring(0, index));
                }

                var project = new SolutionProject()
                {
                    ProjectTypeId = ProjectType.SolutionFolder.ProjectGuid,
                    ProjectId = Guid.NewGuid(),
                    ParentProject = parentProject,
                    Name = solution.GetSolutionFolderName(solutionFolder),
                    Path = solution.GetSolutionFolderName(solutionFolder),
                };

                solution.Projects.Add(project);
                solutionFolderMap.Add(solutionFolder, project.ProjectId);
            }

            // Now attach parents to projects
            foreach (var project in solution.Projects)
            {
                string folderPath = EvaluateProjectFolderPath(solution, project, _options.SolutionFolderLevels);
                if (folderPath != null)
                {
                    SolutionProject parentProject = solution.FindFolderWithPath(folderPath);
                    if (parentProject != null)
                    {
                        project.ParentProject = parentProject;
                    }
                }
            }
        }

        internal List<string> GenerateSolutionFolders(Solution solution)
        {
            var solutionFolders = new List<string>();
            int startLevel = 1;
            int endLevel = _options.SolutionFolderLevels;
            for (int level = startLevel; level <= endLevel; level++)
            {
                var folders = (from p in solution.Projects
                               where !p.IsFolder
                               select EvaluateProjectFolderPath(solution, p, level)).Distinct();
                solutionFolders.AddRange(folders);
            }
            var sortedFolders = from f in solutionFolders orderby f select f;
            return sortedFolders.Distinct().ToList();
        }

        internal string EvaluateProjectFolderPath(Solution solution, SolutionProject project, int folderLevels = -1)
        {
            if (folderLevels < 0)
                folderLevels = _options.SolutionFolderLevels;

            if (project.IsFolder)
            {
                string solutionFolderPath = solution.GetSolutionFolderPath(project);
                return solutionFolderPath.Contains(".") ? solutionFolderPath.Substring(0, solutionFolderPath.LastIndexOf('.')) : null;
            }
            else
            {
                string folderBaseName = SkipCommonLevels(EvaluateFolderBaseName(project));
                int subnameCount = 0;
                int index = 0;
                while (subnameCount < folderLevels && index >= 0)
                {
                    subnameCount++;
                    index = folderBaseName.IndexOf('.', index + 1);
                }
                if (index < 0)
                {
                    index = folderBaseName.Length;
                }
                return folderBaseName.Substring(0, index);
            }
        }

        private string EvaluateFolderBaseName(SolutionProject project)
        {
            string projectName = RemoveCommonPrefixes(project.Name);
            if (_options.FolderNamingMode == SolutionFolderNamingMode.Project)
                return projectName;

            string assemblyName = RemoveCommonPrefixes(new ProjectAnalyzer(Path.Combine(_options.SolutionFolderPath, project.Path)).GetAssemblyName());
            if (string.IsNullOrEmpty(assemblyName))
                return projectName;

            if (_options.FolderNamingMode == SolutionFolderNamingMode.Assembly)
                return assemblyName;

            int projectNameParts = projectName.Split('.').Count();
            int assemblyNameParts = assemblyName.Split('.').Count();
            if (projectNameParts > assemblyNameParts)
                return projectName;
            if (assemblyNameParts > projectNameParts)
                return assemblyName;
            if (projectName.Length > assemblyName.Length)
                return projectName;
            if (assemblyName.Length > projectName.Length)
                return assemblyName;
            return projectName;
        }

        private string RemoveCommonPrefixes(string text)
        {
            if (string.IsNullOrEmpty(_options.CommonPrefixes) || string.IsNullOrEmpty(text))
                return text;

            var prefixes = _options.CommonPrefixes.Split(';');
            var newText = text;
            int length;
            do
            {
                length = newText.Length;
                foreach (var prefix in prefixes)
                {
                    if (newText.StartsWith(prefix + "."))
                    {
                        newText = newText.Substring(prefix.Length + 1);
                    }
                }
            } while (newText.Length < length);

            return newText;
        }

        private string SkipCommonLevels(string text)
        {
            var textParts = text.Split('.');
            textParts = textParts.Skip(Math.Min(_options.CommonPrefixLevels, textParts.Count() - 1)).ToArray();
            return string.Join(".", textParts);
        }
    }
}
