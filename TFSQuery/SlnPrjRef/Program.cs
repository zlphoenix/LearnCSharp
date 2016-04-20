using System.Collections.Generic;
using System.IO;

namespace SlnPrjRef
{
    class Program
    {
        static void Main(string[] args)
        {
            var buildSlnPath = "";


            var slnFiles = GetSlnFiles(buildSlnPath);

            foreach (var sln in slnFiles)
            {

            }

        }

        private static List<FileInfo> GetSlnFiles(string buildSlnPath)
        {
            throw new System.NotImplementedException();
        }
    }
}
