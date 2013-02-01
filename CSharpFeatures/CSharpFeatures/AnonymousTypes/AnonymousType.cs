using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.AnonymousTypes
{
    public class AnonymousType
    {
        public void TestAnonymousType()
        {
            var a = new { X = 1, Y = 2 };
            var b = new { X = "", Y = 1.0 };
            var c = new { X = 3, Y = 4 };
            // Unify
            a = c;
        }

    }
}
