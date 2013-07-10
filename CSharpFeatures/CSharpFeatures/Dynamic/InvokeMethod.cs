using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.Dynamic
{
    public interface IInvokeMethod
    {
        /// <summary>
        /// O(I^2)算法的相加操作
        /// </summary>
        /// <param name="i"></param>
        void Do(int i);
    }

    public class InvokeMethod : IInvokeMethod
    {

        /// <summary>
        /// O(I^2)算法的相加操作
        /// </summary>
        /// <param name="i"></param>
        public void Do(int i)
        {
            int count = 0;
            for (int j = 0; j < i; j++)
            {
                for (int k = 0; k < i; k++)
                {
                    count++;
                }
            }
        }
    }
}
