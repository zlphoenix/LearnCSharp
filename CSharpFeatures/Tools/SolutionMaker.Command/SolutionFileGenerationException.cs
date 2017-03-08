using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Command
{
    class SolutionFileGenerationException : Exception
    {
        public SolutionFileGenerationException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }
    }
}
