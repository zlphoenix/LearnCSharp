using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using SolutionMaker.Core.Model;

namespace SolutionMaker.Core
{
    public class SolutionWriter
    {
        private string _solutionFile;
        private Solution _solution;

        public void Write(string solutionFile, Solution solution)
        {
            this._solutionFile = solutionFile;
            this._solution = solution;

            using (StreamWriter writer = File.CreateText(this._solutionFile))
            {
                WriteHeader(writer);
                WriteProjects(writer);
                WriteGlobal(writer);
            }
        }

        private void WriteHeader(StreamWriter writer)
        {
            // Add UTF BOM marker
            string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            writer.WriteLine(byteOrderMarkUtf8);
            string fileFormatVersion;
            string visualStudioVersion;
            switch (this._solution.FileVersion)
            {
                case SolutionFileVersion.VisualStudio2008:
                    fileFormatVersion = "10.00";
                    visualStudioVersion = "2008";
                    break;

                case SolutionFileVersion.VisualStudio2010:
                    fileFormatVersion = "11.00";
                    visualStudioVersion = "2010";
                    break;

                case SolutionFileVersion.VisualStudio2012:
                    fileFormatVersion = "12.00";
                    visualStudioVersion = "2012";
                    break;

                default:
                    throw new InvalidOperationException("Unsupported Visual Studio solution file");
            }
            writer.WriteLine(string.Format("Microsoft Visual Studio Solution File, Format Version {0}", fileFormatVersion));
            writer.WriteLine(string.Format("# Visual Studio {0}", visualStudioVersion));
        }

        private void WriteProjects(StreamWriter writer)
        {
            foreach (var project in this._solution.Projects)
            {
                WriteProject(writer, project);
            }
        }

        private void WriteGlobal(StreamWriter writer)
        {
            writer.WriteLine("Global");
            foreach (var section in this._solution.Global.Sections)
            {
                writer.WriteLine(string.Format("\tGlobalSection({0}) = {1}", section.SectionName, section.PrePostSolution));
                foreach (string item in section.SectionItems)
                {
                    writer.WriteLine(string.Format("\t\t{0}", item));
                }
                writer.WriteLine(string.Format("\tEndGlobalSection"));
            }

            writer.WriteLine(string.Format("\tGlobalSection({0}) = {1}", Solution.NestedProjectsSectionName, "preSolution"));
            foreach (var project in this._solution.Projects)
            {
                if (project.ParentProjectId != null)
                {
                    writer.WriteLine("\t\t{{{0}}} = {{{1}}}", project.ProjectId.ToString().ToUpper(), project.ParentProjectId.ToString().ToUpper());
                }
            }
            writer.WriteLine(string.Format("\tEndGlobalSection"));

            writer.WriteLine("EndGlobal");
        }

        private void WriteProject(StreamWriter writer, SolutionProject project)
        {
            writer.Write(string.Format(@"Project(""{{{0}}}"") = ", project.ProjectTypeId.ToString().ToUpper()));
            writer.WriteLine(string.Format(@"""{0}"", ""{1}"", ""{{{2}}}""",
                project.Name, project.Path, project.ProjectId.ToString().ToUpper()));

            foreach (var section in project.Sections)
            {
                writer.WriteLine(string.Format("\tProjectSection({0}) = {1}", section.SectionName, section.PrePostProject));
                foreach (string item in section.SectionItems)
                {
                    writer.WriteLine(string.Format("\t\t{0}", item));
                }
                writer.WriteLine(string.Format("\tEndProjectSection"));
            }
            writer.WriteLine("EndProject");
        }
    }
}
