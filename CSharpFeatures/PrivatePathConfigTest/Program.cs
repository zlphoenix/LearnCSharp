using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpFeatures.TPL;

namespace PrivatePathConfigTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Assembly.LoadFrom(@"D:\MyDoc\Study\GitHub\MyCode\CSharpFeatures\PrivatePathConfigTest\bin\Debug\PrivatePath\AutoMappper.dll");
            //var obj = new TaskFeature();
            //obj.Bar();
            //Console.Read();
        }
    }
}
