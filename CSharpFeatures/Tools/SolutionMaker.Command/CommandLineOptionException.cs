using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Command
{
    public class CommandLineOptionException : Exception
    {
        public CommandLineOptionException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }
    }
}
