using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolutionMaker.Core.Extensions;

namespace SolutionMaker.Core.Model
{
    public class SolutionProject : ICloneable
    {
        public class Section : ICloneable
        {
            public Section()
            {
                this.SectionItems = new List<string>();
            }

            public string SectionName { get; set; }
            public string PrePostProject { get; set; }
            public IList<string> SectionItems { get; private set; }

            public override string ToString()
            {
                return string.Format("{0}", this.SectionName);
            }

            #region ICloneable Members

            public object Clone()
            {
                var section = new Section
                {
                    SectionName = this.SectionName,
                    PrePostProject = this.PrePostProject,
                };
                foreach (var item in this.SectionItems)
                {
                    section.SectionItems.Add(item);
                }
                return section;
            }

            #endregion
        }

        public SolutionProject()
        {
            this.Sections = new List<Section>();
        }

        public SolutionProject(Guid projectTypeId, Guid projectId, string name, string path)
            : this(projectTypeId, projectId, name, path, null)
        {
        }

        public SolutionProject(Guid projectTypeId, Guid projectId, string name, string path, SolutionProject parent)
            : this()
        {
            if (parent != null && projectId == parent.ProjectId)
                throw new ArgumentException("Project can not assign itself as its parent", "parent");

            this.ProjectTypeId = projectTypeId;
            this.ProjectId = projectId;
            this.ParentProject = parent;
            this.Name = name;
            this.Path = path;
        }

        public Guid ProjectTypeId { get; set; }
        public Guid ProjectId { get; set; }
        public SolutionProject ParentProject { get; set; }
        public Guid? ParentProjectId { get { if (this.ParentProject == null) return null; else return this.ParentProject.ProjectId; } }
        public string Name { get; set; }
        public string Path { get; set; }

        public IList<Section> Sections { get; private set; }

        public bool IsFolder
        {
            get { return this.ProjectTypeId == ProjectType.SolutionFolder.ProjectGuid; }
        }

        public override string ToString()
        {
            return string.Format("{{{0}, {1}}}", this.Name, this.Path);
        }

        public override bool Equals(object obj)
        {
            SolutionProject project = obj as SolutionProject;
            if (project != null)
            {
                return this.ProjectTypeId == project.ProjectTypeId &&
                    this.ProjectId == project.ProjectId &&
                    this.Name == project.Name &&
                    this.Path == project.Path;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this.ProjectTypeId.GetHashCode() ^ this.ProjectId.GetHashCode() ^
                this.Name.SafeGetHashCode() ^ this.Path.SafeGetHashCode() ^ this.ParentProjectId.SafeGetHashCode();
        }

        #region ICloneable Members

        public object Clone()
        {
            var project = new SolutionProject(this.ProjectTypeId, this.ProjectId, this.Name, this.Path, this.ParentProject);
            foreach (var section in this.Sections)
            {
                project.Sections.Add(section.Clone() as Section);
            }
            return project;
        }

        #endregion
    }
}
