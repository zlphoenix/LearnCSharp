using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using SolutionMaker.Core;
using SolutionMaker.Core.Model;

namespace SolutionMaker.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string relayProcessPath = EvaluateRelayProcessPath(args);

                if (!string.IsNullOrEmpty(relayProcessPath))
                {
                    // Launch relay process
                    Process.Start(relayProcessPath);
                }
                else
                {
                    // Process with a command line
                    var options = CommandLineOptions.Parse(args);
                    if (string.IsNullOrEmpty(options.ProjectRootFolderPath) || string.IsNullOrEmpty(options.SolutionFile))
                    {
                        ShowUsage();
                    }
                    else
                    {
                        GenerateSolutionFile(options);
                    }
                }
            }
            catch (CommandLineOptionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SolutionFileGenerationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while generating solution file: {0}", ex.Message);
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine(@"Usage: SolutionMaker /p <path> /s <solution> [/i includeFilter] [/e excludeFilter] [/l [*]solutionFolderLevels] [/u updateMode] [/r].");
            Console.WriteLine();
            Console.WriteLine(@"/s <solution>: Solution file path");
            Console.WriteLine(@"/p <path>: project root path");
            Console.WriteLine(@"/i <includeFilter>: Include projects that match specified filter, e.g. ""*\Data"". Multiple items are separated with semicolon.");
            Console.WriteLine(@"/x <excludeFilter>: Exclude projects that match specified filter, e.g. ""*.Tests"". Multiple items are separated with semicolon.");
            Console.WriteLine(@"/t: Include only top-level projects. If this option is not set, root directory is scanned for projects recursively.");
            Console.WriteLine(@"/e: Scan project files and include referenced projects.");
            Console.WriteLine(@"/v <fileVersion>: New solution file will be generated in the specified format. Valid versions: 2008, 2010, 2012.");
            Console.WriteLine(@"/u <updateMode>: Updating existing solution file. Valid modes: Add, AddRemove, Replace.");
            Console.WriteLine(@"/r: Overwrite read-only solution file.");
            Console.WriteLine(@"/l solutionFolderLevels: Number of solution folder levels");
            Console.WriteLine(@"/n <solutionFolderNameMode>: Naming of the solution folders. Valid modes: Project, Assembly, MostQualified");
            Console.WriteLine(@"/c <commonPrefixLevels>: Number of common prefix levels");
            Console.WriteLine(@"/f <commonPrefix>: Common project prefix that will be excluded from solution folder names, usually company names e.g. ""MyCompany"". Multiple items are separated with semicolon.");
        }

        private static string EvaluateRelayProcessPath(string[] args)
        {
            // Only relay to another application if no command-line arguments are specified
            if (args.Length == 0)
            {
                string thisProcessPath = Process.GetCurrentProcess().MainModule.FileName;
                if (Path.GetExtension(thisProcessPath).ToLower() == ".com")
                {
                    // Relay process should only differ in extension
                    string relayProcessPath = Path.ChangeExtension(thisProcessPath, ".exe");
                    if (File.Exists(relayProcessPath))
                    {
                        return relayProcessPath;
                    }
                }
            }
            return null;
        }

        private static void GenerateSolutionFile(CommandLineOptions options)
        {
            if (!Directory.Exists(options.ProjectRootFolderPath))
            {
                throw new SolutionFileGenerationException("Invalid directory: " + options.ProjectRootFolderPath);
            }
            else if (File.Exists(options.SolutionFile) && ((File.GetAttributes(options.SolutionFile) & FileAttributes.ReadOnly) != 0) && !options.OverwriteReadOnlyFile)
            {
                throw new SolutionFileGenerationException("Solution file is read-only. Use /r option to overwrite read-only solution file.");
            }
            else
            {
                if (File.Exists(options.SolutionFile))
                {
                    var fileAttributes = File.GetAttributes(options.SolutionFile);
                    if ((fileAttributes & FileAttributes.ReadOnly) != 0 && options.OverwriteReadOnlyFile)
                    {
                        File.SetAttributes(options.SolutionFile, fileAttributes ^ FileAttributes.ReadOnly);
                    }
                }

                SolutionGenerator generator = new SolutionGenerator(new ConsoleLogger());
                generator.GenerateSolution(options.SolutionFile, options);

                string message;
                if (generator.NumberOfProjectsFound > 0)
                {
                    message = string.Format("Solution file is generated\r\n{0} projects found\r\n{1} projects skipped\r\n{2} projects added\r\n{3} projects removed",
                        generator.NumberOfProjectsFound, generator.NumberOfProjectsSkipped, generator.NumberOfProjectsAdded, generator.NumberOfProjectsRemoved);
                }
                else
                {
                    message = string.Format("No project files found in the specified location\r\nSolution file is not generated");
                }
                Console.WriteLine();
                Console.WriteLine(message);
            }
        }
    }
}
