using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
