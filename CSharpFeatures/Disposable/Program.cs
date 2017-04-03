using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposable
{
    /// <summary>
    /// 验证在析构函数里面调用Dispose方法，导致退出异常
    /// </summary>
    class Program : IDisposable
    {
        private List<string> msgs = new List<string>();
        static void Main(string[] args)
        {
            var p = new Program();
        }

        public void Dispose()
        {
            msgs.Clear();
            msgs = null;
        }

        ~Program()
        {
            Dispose();
        }
    }
}
