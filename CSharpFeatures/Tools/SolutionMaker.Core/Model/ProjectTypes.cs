using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Core.Model
{
    public static class ProjectTypes
    {
        public static IEnumerable<ProjectType> Collection
        {
            get
            {
                yield return ProjectType.CSharp;
                yield return ProjectType.VB;
                yield return ProjectType.FSharp;
                yield return ProjectType.VCX;
                yield return ProjectType.VC;
                yield return ProjectType.Wix;
            }
        }

        public static ProjectType Find(string fileExtension)
        {
            foreach (var projectType in ProjectTypes.Collection)
            {
                if (projectType.FileExtension == fileExtension)
                {
                    return projectType;
                }
            }
            return null;
        }

        public static ProjectType Find(Guid projectGuid)
        {
            foreach (var projectType in ProjectTypes.Collection)
            {
                if (projectType.ProjectGuid == projectGuid)
                {
                    return projectType;
                }
            }
            return null;
        }
    }
}
