using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SolutionMaker.Core
{
    internal static class NativeMethods
    {
        [DllImport("shlwapi.dll", CharSet=CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PathRelativePathTo(
            StringBuilder path,
            string from, 
            int attrFrom,
            string to, 
            int attrTo);
    }
}
