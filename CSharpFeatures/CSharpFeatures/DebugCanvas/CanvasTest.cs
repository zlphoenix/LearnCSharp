using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.DebugCanvas
{
    public class CanvasTest
    {
        public void FuncRoot()
        {
            var i = 0;
            Function1(i);

        }

        private void Function1(int i)
        {
            if (i > 5)
                return;
            i++;
            Function2(i);
        }

        private void Function2(int i)
        {
            Console.WriteLine(i);
            Function1(i);
        }

        public void ParallFunc()
        {
            Parallel.For(1, 4, Function1);
        }

    }
}
