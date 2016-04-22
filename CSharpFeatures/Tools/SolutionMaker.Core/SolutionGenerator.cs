using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

using SolutionMaker.Core.Model;

namespace SolutionMaker.Core
{
    public class SolutionGenerator
    {
        private SolutionOptions _options;
        private ILogger _logger;

        public int NumberOfProjectsFound { get; set; }
        public int NumberOfProjectsSkipped { get; set; }
        public int NumberOfProjectsAdded { get; set; }
        public int NumberOfProjectsRemoved { get; set; }

        public SolutionGenerator(ILogger logger)
        {
            this._logger = logger;
        }

        public Solution GenerateSolution(string solutionFile, SolutionOptions options, bool testOnly = false)
        {
            this.NumberOfProjectsFound = 0;
            this.NumberOfProjectsSkipped = 0;
            this.NumberOfProjectsAdded = 0;
            this.NumberOfProjectsRemoved = 0;

            this._options = options;
            var rootDir = new DirectoryInfo(this._options.ProjectRootFolderPath);
            var includeFilter = new string[] { };
            if (!string.IsNullOrEmpty(this._options.IncludeFilter))
            {
                includeFilter = this._options.IncludeFilter.Split(';');
            }
            var excludeFilter = new string[] { };
            if (!string.IsNullOrEmpty(this._options.ExcludeFilter))
            {
                excludeFilter = this._options.ExcludeFilter.Split(';');
            }

            Solution oldSolution = null;
            if (File.Exists(solutionFile) && options.UpdateMode != SolutionUpdateMode.Replace)
            {
                var solutionReader = new SolutionReader();
                oldSolution = solutionReader.Read(solutionFile);
            }

            var projects = new List<SolutionProject>();
            ScanProjectDirectory(rootDir, projects, options.SolutionFileVersion, includeFilter, excludeFilter, this._options.Recursive);
            if (this._options.IncludeReferences)
            {
                ScanProjectReferences(projects, options.SolutionFileVersion, includeFilter, excludeFilter);
            }

            if (projects.Count > 0)
            {
                // Sort project files
                projects = (from p in projects orderby p.Name select p).ToList();

                var merger = new SolutionBuilder(options, _logger);
                var newSolution = merger.Build(oldSolution, projects);
                this.NumberOfProjectsAdded = merger.NumberOfProjectsAdded;
                this.NumberOfProjectsRemoved = merger.NumberOfProjectsRemoved;

                if (!testOnly)
                {
                    var solutionWriter = new SolutionWriter();
                    solutionWriter.Write(solutionFile, newSolution);
                }

                return newSolution;
            }
            else
            {
                return oldSolution;
            }
        }

        private void ScanProjectDirectory(DirectoryInfo dir, IList<SolutionProject> projects, SolutionFileVersion solutionFileVersion,
            IEnumerable<string> includeFilter, IEnumerable<string> excludeFilter, bool recursive)
        {
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            foreach (FileSystemInfo file in files)
            {
                var projectAnalyzer = new ProjectAnalyzer(file.FullName);
                if (projectAnalyzer.IsProjectFile(solutionFileVersion))
                {
                    ProcessProjectFile(file.FullName, projects, includeFilter, excludeFilter, projectAnalyzer, false);
                }
            }

            if (recursive)
            {
                foreach (var subdir in dir.GetDirectories("*"))
                {
                    ScanProjectDirectory(subdir, projects, solutionFileVersion, includeFilter, excludeFilter, recursive);
                }
            }
        }

        private void ScanProjectReferences(IList<SolutionProject> projects, SolutionFileVersion solutionFileVersion,
            IEnumerable<string> includeFilter, IEnumerable<string> excludeFilter)
        {
            var referencedProjects = new List<SolutionProject>();
            foreach (var project in projects)
            {
                ScanProjectReferences(project, referencedProjects, solutionFileVersion, includeFilter, excludeFilter);
            }

            foreach (var referencedProject in referencedProjects)
            {
                if (!projects.Any(x => x.Path.Equals(referencedProject.Path, StringComparison.InvariantCultureIgnoreCase)))
                {
                    projects.Add(referencedProject);
                }
            }
        }

        private void ScanProjectReferences(SolutionProject project, IList<SolutionProject> referencedProjects, SolutionFileVersion solutionFileVersion,
            IEnumerable<string> includeFilter, IEnumerable<string> excludeFilter)
        {
            var projectAnalyzer = new ProjectAnalyzer(Path.Combine(this._options.SolutionFolderPath, project.Path));
            var projectReferences = projectAnalyzer.GetProjectReferences();
            foreach (var projectReference in projectReferences)
            {
                if (File.Exists(projectReference.Path))
                {
                    ProcessProjectFile(projectReference.Path, referencedProjects, includeFilter, excludeFilter, projectAnalyzer, true);
                    ScanProjectReferences(projectReference, referencedProjects, solutionFileVersion, includeFilter, excludeFilter);
                }
            }
        }

        private void ProcessProjectFile(string projectPath, IList<SolutionProject> projects,
            IEnumerable<string> includeFilter, IEnumerable<string> excludeFilter, ProjectAnalyzer projectAnalyzer, bool allowDuplicates)
        {
            ++this.NumberOfProjectsFound;
            string message = string.Format("Found project {0}", Path.GetFileNameWithoutExtension(projectPath));

            bool skip = false;
            if (includeFilter.Any() && !ProjectHasMatch(projectPath, includeFilter))
            {
                skip = true;
            }
            if (excludeFilter.Any() && ProjectHasMatch(projectPath, excludeFilter))
            {
                skip = true;
            }

            if (skip)
            {
                ++this.NumberOfProjectsSkipped;
                message += " (skipped)";
            }
            else
            {
                var project = new SolutionProject();
                project.ProjectTypeId = ProjectTypes.Find(Path.GetExtension(projectPath).Substring(1)).ProjectGuid;
                project.ProjectId = projectAnalyzer.GetProjectId();
                project.Name = Path.GetFileNameWithoutExtension(projectPath);
                project.Path = Utils.GetRelativePath(this._options.SolutionFolderPath, projectPath);

                if (!allowDuplicates && projects.Any(x => x.Name == project.Name))
                {
                    throw new InvalidOperationException(string.Format("Duplicate project: {0}. All projects should have unique names across the solution.", project.Name));
                }

                projects.Add(project);
            }

            this._logger.Write(message);
        }

        internal bool ProjectHasMatch(string projectPath, IEnumerable<string> patternsToMatch)
        {
            foreach (var item in patternsToMatch)
            {
                if (Utils.PathHasMatch(projectPath, item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
