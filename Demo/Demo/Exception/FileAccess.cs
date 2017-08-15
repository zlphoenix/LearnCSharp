using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Exception
{
    class FileAccess
    {

        public void ReadFile()
        {

            //
            var fs = new FileStream("cat.txt", FileMode.Open);
            try
            {
                //fs.Read();
                //do sth.
            }
            catch (IOException ioe)
            {
                // 吞异常
                Console.WriteLine(ioe);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                throw;
                //not throw e;//这样会导致异常堆栈被截断
            }
            finally
            {
                //保证非托管资源被正确释放
                fs.Dispose();
            }
        }

        public void UsingReadFile()
        {
            using (var fs = new FileStream("cat.txt", FileMode.Open))
            {
                //do sth....
            }
        }
    }
}
