using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Core.Model
{
    public class Solution : ICloneable
    {
        public static readonly string NestedProjectsSectionName = "NestedProjects";

        public Solution()
        {
            this.Header = new SolutionHeader();
            this.Projects = new List<SolutionProject>();
            this.Global = new SolutionGlobal();
        }

        public SolutionFileVersion FileVersion { get; set; }
        public SolutionHeader Header { get; set; }
        public IList<SolutionProject> Projects { get; private set; }
        public SolutionGlobal Global { get; set; }

        public bool HasNestedProjects()
        {
            foreach (var project in this.Projects)
            {
                if (project.ParentProjectId != null)
                {
                    return true;
                }
            }
            return false;
        }

        public SolutionProject FindProject(Guid projectId)
        {
            foreach (var project in this.Projects)
            {
                if (project.ProjectId == projectId)
                {
                    return project;
                }
            }
            return null;
        }

        public SolutionProject FindFolderWithPath(string solutionFolderPath)
        {
            foreach (var project in this.Projects)
            {
                if (project.IsFolder && GetSolutionFolderPath(project) == solutionFolderPath)
                {
                    return project;
                }
            }
            return null;
        }

        public SolutionGlobal.Section FindGlobalSection(string sectionName)
        {
            foreach (var section in this.Global.Sections)
            {
                if (section.SectionName == sectionName)
                {
                    return section;
                }
            }
            return null;
        }

        public string GetSolutionFolderName(string solutionFolderPath)
        {
            return solutionFolderPath.Contains(".") ? solutionFolderPath.Substring(solutionFolderPath.LastIndexOf('.') + 1) : solutionFolderPath;
        }

        public string GetSolutionFolderPath(SolutionProject project)
        {
            if (!project.IsFolder)
            {
                throw new InvalidOperationException("GetSolutionFolderPath requires folder project");
            }

            string path = project.Name;
            Guid? parentProjectId = project.ParentProjectId;
            while (parentProjectId != null)
            {
                var parentProject = this.FindProject(parentProjectId.Value);
                if (parentProject != null)
                {
                    path = string.Format("{0}.{1}", parentProject.Path, path);
                    parentProjectId = parentProject.ParentProjectId;
                }
            }
            return path;
        }

        #region ICloneable Members

        public object Clone()
        {
            var solution = new Solution();
            solution.FileVersion = this.FileVersion;
            solution.Header = this.Header.Clone() as SolutionHeader;
            foreach (var project in this.Projects)
            {
                solution.Projects.Add(project.Clone() as SolutionProject);
            }
            solution.Global = this.Global.Clone() as SolutionGlobal;
            return solution;
        }

        #endregion
    }
}
