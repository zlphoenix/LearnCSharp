using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpFeatures.TPL
{
    public class TaskFeature
    {
        public void Bar()
        {

            var t = new Task<string>(Foo);
            t.ContinueWith(task => CallBack(task));
        }

        private static object CallBack(Task<string> task)
        {
            //task.Result
            return null;
        }


        public string Foo()
        {

            return string.Empty;
        }
        public void CallBack(string str)
        {

        }


        public void QueueNewJob()
        {
            ThreadPool.QueueUserWorkItem(WorkJob);
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("MainTread!");
            }
        }


        private void WorkJob(object state)
        {
            Thread.Sleep(1000);
            Console.WriteLine("WorkJob!");
        }
    }
}
