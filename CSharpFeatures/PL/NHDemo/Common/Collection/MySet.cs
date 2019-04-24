using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace NHDemo.Common.Collection
{
    public class MySet<T> : HashedSet<T>, IMySet<T>
    {
        public virtual int Calculate() { return 0; }
    }
}
