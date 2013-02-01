using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService2" in both code and config file together.
    public class MyService2 : IMyService2
    {
        public MyService2()
        {
            Console.WriteLine("Create a new SV Impl instance");
        }
        private int _count;
        private static int TotalCount;
   
        public void DoWork()
        {
            Console.WriteLine(string.Format("Service instance is Called {0} times,total {1} times !", _count++, TotalCount++));
        }
    }
}
