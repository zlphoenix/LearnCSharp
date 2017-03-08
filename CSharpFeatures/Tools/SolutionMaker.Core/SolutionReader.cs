using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using SolutionMaker.Core.Model;

namespace SolutionMaker.Core
{
    public class SolutionReader
    {
        public Solution Read(string solutionFile)
        {
            var solution = new Solution();

            using (var reader = new StreamReader(solutionFile))
            {
                var lines = new List<string>();
                string text;
                while ((text = reader.ReadLine()) != null)
                {
                    lines.Add(text);
                }

                int lineIndex = 0;
                ParseHeader(solution, lines, ref lineIndex);
                ParseProjects(solution, lines, ref lineIndex);
                ParseGlobal(solution, lines, ref lineIndex);
                ParseNestedProjects(solution);

                var section = solution.FindGlobalSection(Solution.NestedProjectsSectionName);
                if (section != null)
                {
                    solution.Global.Sections.Remove(section);
                }
            }

            return solution;
        }

        private void ParseHeader(Solution solution, List<string> lines, ref int lineIndex)
        {
            bool more = true;
            while (more && lineIndex < lines.Count)
            {
                string line = lines[lineIndex++];
                if (line.StartsWith("Project("))
                {
                    more = false;
                }
                else if (line.Contains("Format Version"))
                {
                    string fileVersion = line.Split(' ').Last();
                    switch (fileVersion)
                    {
                        case "10.00":
                            solution.FileVersion = SolutionFileVersion.VisualStudio2008;
                            break;

                        case "11.00":
                            solution.FileVersion = SolutionFileVersion.VisualStudio2010;
                            break;

                        case "12.00":
                            solution.FileVersion = SolutionFileVersion.VisualStudio2012;
                            break;

                        default:
                            throw new InvalidOperationException("Unsupported Visual Studio solution file");
                    }
                }
                else if (line.Contains("Visual Studio"))
                {
                    ;
                }

                if (!more)
                {
                    lineIndex--;
                }
            }
        }

        private void ParseProjects(Solution solution, List<string> lines, ref int lineIndex)
        {
            bool more = true;
            while (more && lineIndex < lines.Count)
            {
                string line = lines[lineIndex++];
                if (!line.StartsWith("Project("))
                {
                    more = false;
                }
                else
                {
                    var project = new SolutionProject();
                    int index = line.IndexOf('{') + 1;
                    project.ProjectTypeId = new Guid(line.Substring(index, line.IndexOf('}', index) - index));
                    index = line.LastIndexOf('{') + 1;
                    project.ProjectId = new Guid(line.Substring(index, line.IndexOf('}', index) - index));
                    index = line.IndexOf("= \"") + 3;
                    project.Name = line.Substring(index, line.IndexOf('\"', index) - index);
                    index = line.IndexOf(", \"", index) + 3;
                    project.Path = line.Substring(index, line.IndexOf('\"', index) - index);

                    ParseProjectSections(project, lines, ref lineIndex);

                    solution.Projects.Add(project);
                }

                if (!more)
                {
                    lineIndex--;
                }
            }
        }

        private void ParseProjectSections(SolutionProject project, List<string> lines, ref int lineIndex)
        {
            bool more = true;
            while (more && lineIndex < lines.Count)
            {
                string line = lines[lineIndex++];
                if (line.StartsWith("EndProject"))
                {
                    more = false;
                }
                else if (line.StartsWith("\tProjectSection"))
                {
                    --lineIndex;
                    ParseProjectSection(project, lines, ref lineIndex);
                }
            }
        }

        private void ParseProjectSection(SolutionProject project, List<string> lines, ref int lineIndex)
        {
            var section = new SolutionProject.Section();
            project.Sections.Add(section);
            bool more = true;
            while (more && lineIndex < lines.Count)
            {
                string line = lines[lineIndex++];
                if (line.StartsWith("\tEndProjectSection"))
                {
                    more = false;
                }
                else if (line.StartsWith("\tProjectSection"))
                {
                    int index = line.IndexOf('(') + 1;
                    section.SectionName = line.Substring(index, line.IndexOf(')', index) - index);
                    index = line.IndexOf("= ", index) + 2;
                    section.PrePostProject = line.Substring(index);

                }
                else
                {
                    section.SectionItems.Add(line.Trim());
                }
            }
        }

        private void ParseGlobal(Solution solution, List<string> lines, ref int lineIndex)
        {
            bool more = true;
            while (more && lineIndex < lines.Count)
            {
                string line = lines[lineIndex++];
                if (line.StartsWith("Global"))
                {
                    ;
                }
                else if (line.StartsWith("EndGlobal"))
                {
                    more = false;
                }
                else if (line.StartsWith("\tGlobalSection"))
                {
                    --lineIndex;
                    ParseGlobalSection(solution.Global, lines, ref lineIndex);
                }
            }
        }

        private void ParseGlobalSection(SolutionGlobal global, List<string> lines, ref int lineIndex)
        {
            var section = new SolutionGlobal.Section();
            global.Sections.Add(section);
            bool more = true;
            while (more && lineIndex < lines.Count)
            {
                string line = lines[lineIndex++];
                if (line.StartsWith("\tEndGlobalSection"))
                {
                    more = false;
                }
                else if (line.StartsWith("\tGlobalSection"))
                {
                    int index = line.IndexOf('(') + 1;
                    section.SectionName = line.Substring(index, line.IndexOf(')', index) - index);
                    index = line.IndexOf("= ", index) + 2;
                    section.PrePostSolution = line.Substring(index);
                }
                else
                {
                    section.SectionItems.Add(line.Trim());
                }
            }
        }

        private void ParseNestedProjects(Solution solution)
        {
            SolutionGlobal.Section nestedProjectsSection = null;
            foreach (var section in solution.Global.Sections)
            {
                if (section.SectionName == Solution.NestedProjectsSectionName)
                {
                    foreach (string item in section.SectionItems)
                    {
                        int index = item.IndexOf('{') + 1;
                        Guid projectId = new Guid(item.Substring(index, item.IndexOf('}', index) - index));
                        index = item.LastIndexOf('{') + 1;
                        Guid parentProjectId = new Guid(item.Substring(index, item.IndexOf('}', index) - index));

                        var project = solution.FindProject(projectId);
                        var parentProject = solution.FindProject(parentProjectId);
                        if (project != null && parentProject != null)
                        {
                            project.ParentProject = parentProject;
                        }
                    }
                }
            }
            if (nestedProjectsSection != null)
            {
                solution.Global.Sections.Remove(nestedProjectsSection);
            }
        }
    }
}
