using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Other
{
    /// <summary>
    /// 索引器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AboutIndexer<T>
    {
        /// <summary>
        /// 声明存储空间
        /// </summary>
        private T[] arr = new T[100];

        /// <summary>
        /// 声明索引器
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T this[int i]
        {
            get { return arr[i]; }
            set { arr[i] = value; }
        }
        /// <summary>
        /// 重载索引器
        /// </summary>
        /// <param name="i">使用索引值对应的字符串</param>
        /// <returns></returns>
        public T this[string i]
        {
            get
            {
                return arr[Convert.ToInt32(i)];
            }
            set { arr[Convert.ToInt32(i)] = value; }
        }
    }
}
