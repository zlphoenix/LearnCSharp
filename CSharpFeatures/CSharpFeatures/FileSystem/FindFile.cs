using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.FileSystem
{
    public class FindFile
    {
        public string GetFile(string path)
        {

            var fs = Directory.GetFiles(path, "*.bat", SearchOption.AllDirectories);
            return fs.FirstOrDefault();

        }
    }
}
