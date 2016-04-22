using SolutionMaker.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolutionMaker.Command
{
    public class CommandLineOptions : SolutionOptions
    {
        private enum OptionType
        {
            None,
            SolutionFile,
            ProjectRootFolderPath,
            IncludeFilter,
            ExcludeFilter,
            NonRecursive,
            IncludeReferences,
            ProjectTypes,
            SolutionFileVersion,
            SolutionUpdateMode,
            OverwriteReadOnlyFile,
            SolutionFolderLevels,
            SolutionFolderNaming,
            CommonPrefixLevels,
            CommonPrefixes
        };

        public string SolutionFile { get; set; }

        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();
            options.SolutionFile = string.Empty;
            var optionType = OptionType.None;

            foreach (string arg in args)
            {
                if (arg.StartsWith("-") || arg.StartsWith("/"))
                {
                    optionType = options.ParseOptionType(arg.Substring(1));
                }
                else
                {
                    try
                    {
                        options.ParseOptionArgument(optionType, arg);
                        optionType = OptionType.None;
                    }
                    catch (CommandLineOptionException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw new CommandLineOptionException("Invalid command line option: {0}: {1}", arg, ex.Message);
                    }
                }
            }

            if (!string.IsNullOrEmpty(options.SolutionFile))
                options.SolutionFolderPath = Path.GetDirectoryName(options.SolutionFile);

            return options;
        }

        private OptionType ParseOptionType(string arg)
        {
            // Used letters: cefilnoprstuvx

            var optionType = OptionType.None;
            var optionTypes = new Dictionary<string, OptionType>()
                                  {
                                      { "solutionfile", OptionType.SolutionFile },
                                      { "solution", OptionType.SolutionFile },
                                      { "s", OptionType.SolutionFile },
                                      { "projectroot", OptionType.ProjectRootFolderPath },
                                      { "root", OptionType.ProjectRootFolderPath },
                                      { "p", OptionType.ProjectRootFolderPath },
                                      { "includefilter", OptionType.IncludeFilter },
                                      { "include", OptionType.IncludeFilter },
                                      { "i", OptionType.IncludeFilter },
                                      { "excludefilter", OptionType.ExcludeFilter },
                                      { "exclude", OptionType.ExcludeFilter },
                                      { "x", OptionType.ExcludeFilter },
                                      { "nonrecursive", OptionType.NonRecursive },
                                      { "o", OptionType.NonRecursive },
                                      { "includereferences", OptionType.IncludeReferences },
                                      { "references", OptionType.IncludeReferences },
                                      { "e", OptionType.IncludeReferences },
                                      { "projecttypes", OptionType.ProjectTypes },
                                      { "t", OptionType.ProjectTypes },
                                      { "solutionfileversion", OptionType.SolutionFileVersion },
                                      { "version", OptionType.SolutionFileVersion },
                                      { "v", OptionType.SolutionFileVersion },
                                      { "solutionupdatemode", OptionType.SolutionUpdateMode },
                                      { "updatemode", OptionType.SolutionUpdateMode },
                                      { "u", OptionType.SolutionUpdateMode },
                                      { "overwritereadonly", OptionType.OverwriteReadOnlyFile },
                                      { "overwrite", OptionType.OverwriteReadOnlyFile },
                                      { "r", OptionType.OverwriteReadOnlyFile },
                                      { "solutionfolderlevels", OptionType.SolutionFolderLevels },
                                      { "folderlevels", OptionType.SolutionFolderLevels },
                                      { "l", OptionType.SolutionFolderLevels },
                                      { "solutionfoldernaming", OptionType.SolutionFolderNaming },
                                      { "foldernaming", OptionType.SolutionFolderNaming },
                                      { "n", OptionType.SolutionFolderNaming },
                                      { "commonprefixlevels", OptionType.CommonPrefixLevels },
                                      { "prefixlevels", OptionType.CommonPrefixLevels },
                                      { "c", OptionType.CommonPrefixLevels },
                                      { "commonprefixes", OptionType.CommonPrefixes },
                                      { "prefixes", OptionType.CommonPrefixes },
                                      { "f", OptionType.CommonPrefixes },
                                  };

            optionTypes.TryGetValue(arg.ToLower(), out optionType);

            var optionDelegates = new Dictionary<OptionType, Action>()
            {
                { OptionType.NonRecursive, AssignNonRecursive },
                { OptionType.IncludeReferences, AssignIncludeReferences },
                { OptionType.OverwriteReadOnlyFile, AssignOverwriteReadOnlyFile },
            };

            if (optionDelegates.ContainsKey(optionType))
            {
                optionDelegates[optionType].Invoke();
                optionType = OptionType.None;
            }

            return optionType;
        }

        private void ParseOptionArgument(OptionType optionType, string arg)
        {
            var optionDelegates = new Dictionary<OptionType, Action<string>>()
            {
                { OptionType.SolutionFile, AssignSolutionFile },
                { OptionType.ProjectRootFolderPath, AssignProjectRootFolderPath },
                { OptionType.IncludeFilter, AssignIncludeFilter },
                { OptionType.ExcludeFilter, AssignExcludeFilter },
                { OptionType.ProjectTypes, AssignProjectTypes },
                { OptionType.SolutionFileVersion, AssignSolutionFileVersion },
                { OptionType.SolutionUpdateMode, AssignSolutionUpdateMode },
                { OptionType.SolutionFolderLevels, AssignSolutionFolderLevels },
                { OptionType.SolutionFolderNaming, AssignSolutionFolderNaming },
                { OptionType.CommonPrefixLevels, AssignCommonPrefixLevels },
                { OptionType.CommonPrefixes, AssignCommonPrefixes },
            };

            if (optionDelegates.ContainsKey(optionType))
            {
                optionDelegates[optionType].Invoke(arg);
            }
        }

        private void AssignNonRecursive()
        {
            this.Recursive = false;
        }

        private void AssignIncludeReferences()
        {
            this.IncludeReferences = true;
        }

        private void AssignOverwriteReadOnlyFile()
        {
            this.OverwriteReadOnlyFile = true;
        }

        private void AssignSolutionFile(string arg)
        {
            this.SolutionFile = arg;
        }

        private void AssignProjectRootFolderPath(string arg)
        {
            this.ProjectRootFolderPath = arg;
        }

        private void AssignIncludeFilter(string arg)
        {
            this.IncludeFilter = arg;
        }

        private void AssignExcludeFilter(string arg)
        {
            this.ExcludeFilter = arg;
        }

        private void AssignProjectTypes(string arg)
        {
            this.ProjectTypes = arg;
        }

        private void AssignSolutionFileVersion(string arg)
        {
            switch (arg)
            {
                case "2008":
                    this.SolutionFileVersion = Core.Model.SolutionFileVersion.VisualStudio2008;
                    break;
                case "2010":
                    this.SolutionFileVersion = Core.Model.SolutionFileVersion.VisualStudio2010;
                    break;
                case "2012":
                    this.SolutionFileVersion = Core.Model.SolutionFileVersion.VisualStudio2012;
                    break;
                default:
                    throw new CommandLineOptionException("Invalid file version: {0}. Valid versions: 2008, 2010 and 2012.", arg);
            }
        }

        private void AssignSolutionUpdateMode(string arg)
        {
            try
            {
                this.UpdateMode = (SolutionUpdateMode)Enum.Parse(typeof(SolutionUpdateMode), arg, true);
            }
            catch (ArgumentException)
            {
                throw new CommandLineOptionException("Invalid update option: {0}. Valid options: Add, AddRemove or Replace.", arg);
            }
        }

        private void AssignSolutionFolderLevels(string arg)
        {
            try
            {
                if (arg.StartsWith("*"))
                {
                    // Obsolete SM 1.0 setting
                    if (this.CommonPrefixLevels == 0)
                        this.CommonPrefixLevels = 1;
                }

                this.SolutionFolderLevels = Convert.ToInt32(arg);
            }
            catch (FormatException)
            {
                throw new CommandLineOptionException("Invalid number of folder levels: {0}. Must be a non-negative number or *.", arg);
            }
        }

        private void AssignSolutionFolderNaming(string arg)
        {
            try
            {
                this.FolderNamingMode = (SolutionFolderNamingMode)Enum.Parse(typeof(SolutionFolderNamingMode), arg, true);
            }
            catch (ArgumentException)
            {
                throw new CommandLineOptionException("Invalid folder naming option: {0}. Valid options: Project, Assembly or MostQualified.", arg);
            }
        }

        private void AssignCommonPrefixLevels(string arg)
        {
            try
            {
                this.CommonPrefixLevels = Convert.ToInt32(arg);
            }
            catch (FormatException)
            {
                throw new CommandLineOptionException("Invalid number of common prefix levels: {0}. Must be a non-negative number or *.", arg);
            }
        }

        private void AssignCommonPrefixes(string arg)
        {
            this.CommonPrefixes = arg;
        }
    }
}
