using System;
using System.IO;

namespace Inspur.GSP.Bom.Builder
{
    internal class BomBuildOption
    {
        public string InitPath = @"D:\Work\GSP\GSP6.1\Draft";
        private string slnPath;
        public string SlnPath
        {
            get
            {
                if (string.IsNullOrEmpty(slnPath))
                    return Path.Combine(InitPath, "BuildSln");
                return slnPath;
            }
            set { slnPath = value; }
        }
        public string CiProjectListConfigPath = ".\\CI.csv";

        internal BomBuildOption(string[] args)
        {
            var opType = BomBuildOptionType.None;
            foreach (string arg in args)
            {
                if (arg.StartsWith("-") || arg.StartsWith("/"))
                {
                    opType = Parse(arg.Substring(1));
                }
                else
                {
                    try
                    {
                        ParseOptionArgument(opType, arg);
                        opType = BomBuildOptionType.None;
                    }

                    catch (Exception ex)
                    {
                        throw new Exception($"Invalid command line option: {arg}: {ex.Message}", ex);
                    }
                }
            }

        }

        private void ParseOptionArgument(BomBuildOptionType opType, string s)
        {
            switch (opType)
            {
                case BomBuildOptionType.BuildPath:
                    {
                        if (!Directory.Exists(s))
                        {
                            throw new IOException($"Build start path:{s} not found!");
                        }
                        InitPath = s;
                        break;
                    }
                case BomBuildOptionType.CiFilePath:
                    if (!Directory.Exists(s))
                    {
                        throw new IOException($"Build Sln target path:{s} not found!");
                    }
                    SlnPath = s;
                    break;

                default:
                    break;
            }
        }

        private static BomBuildOptionType Parse(string argPrefix)
        {
            switch (argPrefix)
            {
                case "B":
                case "b":
                    return BomBuildOptionType.BuildPath;
                case "C":
                case "c":
                    return BomBuildOptionType.CiFilePath;
                default:
                    return BomBuildOptionType.None;
            }
        }

        enum BomBuildOptionType
        {
            None = 0,
            BuildPath = 1,
            CiFilePath = 2,
        }
    }
}