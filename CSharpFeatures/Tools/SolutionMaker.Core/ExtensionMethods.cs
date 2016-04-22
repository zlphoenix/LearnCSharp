using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolutionMaker.Core.Model;

namespace SolutionMaker.Core.Extensions
{
    public static class ExtensionMethods
    {
        public static int SafeGetHashCode(this object value)
        {
            if (value == null)
                return 0;
            else
                return value.GetHashCode();
        }
    }
}
