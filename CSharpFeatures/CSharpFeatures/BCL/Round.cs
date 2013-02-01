using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.BCL
{
    public class Round
    {
        public int IntRound(int a, int b)
        {
            var result = (a % b) == 0 ? a / b : a / b + 1;
            return result;
        }
    }
}
