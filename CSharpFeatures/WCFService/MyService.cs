using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

//using ServiceExtention;
//using ServiceExtention.CustomAttribute;

namespace WCFService
{
    [Serializable]
    public class MyException : Exception
    {

    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in both code and config file together.
    public class MyService : IMyService
    {
        public MyService()
        {
            Console.WriteLine("Create a new SV Impl instance");
        }
        private int _count;
        private static int TotalCount;
        //[MyLog]
        public void DoWork()
        {
            Console.WriteLine(string.Format("Service instance is Called {0} times,total {1} times !", _count++, TotalCount++));
        }
    }
}
