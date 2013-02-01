using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CSharpFeatures.Diagnostics
{
    class StackTraceTest
    {
        public void Func()
        {
            var st = new StackTrace(true);
            Console.WriteLine(st.GetFrame(1).GetMethod().Name.ToString());
        }
    }
}
