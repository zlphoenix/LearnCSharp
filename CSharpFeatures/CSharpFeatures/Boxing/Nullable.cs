using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.NewFolder1
{
    class Nullable
    {
        //private Nullable<int> a;

        private  void foo()
        {
            MyStruct s = new MyStruct();
            IDisposable d = s;
            Nullable<MyStruct> ms = s;

        }
    }

    struct MyStruct : IDisposable
    {

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
