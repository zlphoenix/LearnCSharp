using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.Lambda
{
    /// <summary>
    /// 比较使用与不使用闭包解决相同问题性能的差别
    /// </summary>
    /// <remarks>
    /// 闭包变量在使用时会产生匿名类型实例，也增加的GC的负担
    /// </remarks>
    public class Closure
    {
        public List<int> Sample { get; set; }
        /// <summary>
        /// 用于存储需要查找的内容
        /// </summary>
        public ConcurrentBag<int> Content { get; } = new ConcurrentBag<int>();

        public void UseClosure()
        {
            foreach (var sample in Sample)
                UseClosure(sample);
        }
        /// <summary>
        /// 使用闭包匹配
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool UseClosure(int match)
        {
            return Content.Any(item => item.Equals(match));
        }
        public void NoClosure()
        {
            foreach (var sample in Sample)
                foreach (var item in Content)
                {
                    if (item == sample)
                    {
                        continue;
                    }
                }
        }
        /// <summary>
        /// 使用迭代匹配
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool NoClosure(int match)
        {
            foreach (var item in Content)
            {
                if (item == match)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
