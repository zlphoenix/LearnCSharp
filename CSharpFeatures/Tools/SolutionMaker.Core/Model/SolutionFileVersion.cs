using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Core.Model
{
    [Flags]
    public enum SolutionFileVersion
    {
        None,
        VisualStudio2002 = 1,
        VisualStudio2003 = 2,
        VisualStudio2005 = 4,
        VisualStudio2008 = 8,
        VisualStudio2010 = 0x10,
        VisualStudio2012 = 0x20,
        All = 0xFF,
    }
}
