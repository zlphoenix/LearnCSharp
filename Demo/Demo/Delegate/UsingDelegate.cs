using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Inspur.Gsp.CSharpIntroduction.Demo.OO;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Delegate
{
    public class UsingDelegate
    {

        public int Closure()
        {

            var i = 0;

            Action action = () =>
            {
                i++;
            };

            i++;
            Console.WriteLine("before infoke i is {0}", i);

            action();
            return i;

            //var a = new A()
            //{
            //    i = 0
            //};
            //a.i++;

            //a.Action();


        }

        class A
        {
            public int i { get; set; }

            public void Action()
            {
                i++;
            }
        }
    }
}
