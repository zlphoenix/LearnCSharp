using Contract;
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
}
