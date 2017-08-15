using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspur.Gsp.CSharpIntroduction.Demo.OO;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Generic
{
    public class Queue<T> //where T : new()
    {

        /// <summary>
        /// 传入不同的类型参数构造出不同的泛型类型，其静态属性值不共享
        /// </summary>
        public static int Count { get; set; }

        public void Enqueue(T item)
        {
            throw new NotSupportedException();
        }

        public T Dequeue()
        {
            return default(T);
            //throw new NotSupportedException();
        }
    }



}
