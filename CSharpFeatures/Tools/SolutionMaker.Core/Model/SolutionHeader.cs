using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Core.Model
{
    public class SolutionHeader : ICloneable
    {
        #region ICloneable Members

        public object Clone()
        {
            return new SolutionHeader();
        }

        #endregion
    }
}
