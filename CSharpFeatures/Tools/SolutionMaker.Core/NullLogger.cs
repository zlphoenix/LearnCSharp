using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Core
{
    public class NullLogger : ILogger
    {
        #region ILogger Members

        public void Write(string text)
        {
        }

        #endregion
    }
}
