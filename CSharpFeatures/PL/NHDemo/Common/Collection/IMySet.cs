using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace NHDemo.Common.Collection
{
    public interface IMySet<T> : ISet<T>
    {
        int Calculate();
    }
}
