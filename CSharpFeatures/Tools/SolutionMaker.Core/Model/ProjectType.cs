using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Core.Model
{
    public class ProjectType
    {
        private static readonly string _msBuildProjectGuidRegex = @"^.*<ProjectGuid>(?<ProjectId>{0})</ProjectGuid>.*$";
        private static readonly string _msBuildAssemblyNameRegex = @"^.*<AssemblyName>(?<AssemblyName>.+)</AssemblyName>.*$";

        public string FileExtension { get; set; }
        public Guid ProjectGuid { get; set; }
        public bool CustomFileFormat { get; set; }
        public string ProjectGuidRegex { get; set; }
        public string AssemblyNameRegex { get; set; }
        public SolutionFileVersion SupportedVersions { get; set; }

        public static readonly ProjectType SolutionFolder = new ProjectType
        {
            FileExtension = null,
            ProjectGuid = new Guid("2150E333-8FDC-42A3-9474-1A3956D46DE8"),
            ProjectGuidRegex = null,
            AssemblyNameRegex = null,
            SupportedVersions = SolutionFileVersion.VisualStudio2008 | SolutionFileVersion.VisualStudio2010 | SolutionFileVersion.VisualStudio2012,
        };

        public static readonly ProjectType CSharp = new ProjectType 
        { 
            FileExtension = "csproj",
            ProjectGuid = new Guid("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"),
            ProjectGuidRegex = _msBuildProjectGuidRegex,
            AssemblyNameRegex = _msBuildAssemblyNameRegex,
            SupportedVersions = SolutionFileVersion.VisualStudio2008 | SolutionFileVersion.VisualStudio2010 | SolutionFileVersion.VisualStudio2012,
        };

        public static readonly ProjectType VB = new ProjectType
        {
            FileExtension = "vbproj",
            ProjectGuid = new Guid("F184B08F-C81C-45F6-A57F-5ABD9991F28F"),
            ProjectGuidRegex = _msBuildProjectGuidRegex,
            AssemblyNameRegex = _msBuildAssemblyNameRegex,
            SupportedVersions = SolutionFileVersion.VisualStudio2008 | SolutionFileVersion.VisualStudio2010 | SolutionFileVersion.VisualStudio2012,
        };

        public static readonly ProjectType FSharp = new ProjectType
        {
            FileExtension = "fsproj",
            ProjectGuid = new Guid("F2A71F9B-5D33-465A-A702-920D77279786"),
            ProjectGuidRegex = _msBuildProjectGuidRegex,
            AssemblyNameRegex = _msBuildAssemblyNameRegex,
            SupportedVersions = SolutionFileVersion.VisualStudio2010 | SolutionFileVersion.VisualStudio2012,
        };

        public static readonly ProjectType VCX = new ProjectType
        {
            FileExtension = "vcxproj",
            ProjectGuid = new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942"),
            ProjectGuidRegex = _msBuildProjectGuidRegex,
            AssemblyNameRegex = null,
            SupportedVersions = SolutionFileVersion.VisualStudio2010 | SolutionFileVersion.VisualStudio2012,
        };

        public static readonly ProjectType VC = new ProjectType
        {
            FileExtension = "vcproj",
            ProjectGuid = new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942"),
            CustomFileFormat = true,
            ProjectGuidRegex = @"^.*ProjectGUID\s*=\s*""(?<ProjectId>{0})"".*$",
            AssemblyNameRegex = null,
            SupportedVersions = SolutionFileVersion.VisualStudio2008,
        };

        public static readonly ProjectType Wix = new ProjectType
        {
            FileExtension = "wixproj",
            ProjectGuid = new Guid("930C7802-8A8C-48F9-8165-68863BCCD9DD"),
            ProjectGuidRegex = _msBuildProjectGuidRegex,
            AssemblyNameRegex = _msBuildAssemblyNameRegex,
            SupportedVersions = SolutionFileVersion.VisualStudio2008 | SolutionFileVersion.VisualStudio2010 | SolutionFileVersion.VisualStudio2012,
        };
    }
}
