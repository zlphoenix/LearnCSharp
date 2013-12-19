using Contract;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implement
{
    public class Implement : IMyInterface1
    {
        public void Do()
        {
            var a = new Referenced.Action();
            a.Excute();
        }
    }

    public class Implement2 : IMyInterface2
    {
        public void Do()
        {
            var a = Loader.Load<IMyInterface3>(@"..\LoadV3\ReferencedV3.dll");
            a.Do();
        }


        public IMyInterface1 V1
        {
            get { return Activator.CreateInstance(Type.GetType("Implement")) as Implement;; }
        }
    }
}
