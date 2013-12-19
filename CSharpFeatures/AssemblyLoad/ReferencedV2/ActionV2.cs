using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferencedV2
{
    public class ActionV2 : IMyInterface3
    {
        public void Do()
        {
            Console.WriteLine("Referenced assembly action V2 is Excuted!");
        }
    }
}
