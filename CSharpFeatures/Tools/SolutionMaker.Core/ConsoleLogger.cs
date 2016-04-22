using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolutionMaker.Core;

namespace SolutionMaker.Core
{
    public class ConsoleLogger : ILogger
    {
        #region ILogger Members

        public void Write(string text)
        {
            Console.WriteLine(text);
        }

        #endregion
    }
}
