using Allen.Util.Logging;
using Contract;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {

            Base<decimal>.Instance = new Base<decimal>();
            var a = new Derived1();
            var b = new Derived2();
            System.Console.WriteLine(Base<decimal>.Instance.GetType().FullName);
            System.Console.WriteLine(Derived1.Instance.GetType().FullName);
            System.Console.WriteLine(Derived2.Instance.GetType().FullName);


            var logger = LogManager.GetLogger("Console");
            try
            {
                var actor = Loader.Load<IMyInterface1>(@"..\Load\Implement.dll");
                if (actor == null)
                    logger.Debug("Load Type Error!");
                else
                {
                    actor.Do();
                }

                Loader.OutputAssemblies("After invoke:");

                var actorV2 = Loader.Load<IMyInterface2>(@"..\LoadV2\Implement.dll");
                if (actorV2 == null)
                    logger.Debug("Load V2 Type Error!");
                else
                {
                    actorV2.Do();
                }
                actorV2.V1.Do();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }


            logger.Debug("press any key...");
            System.Console.ReadLine();
        }
    }


    class Base<T>
    {
        public static Base<T> Instance { get; set; }
    }

    class Derived1 : Base<string>
    {
        public Derived1()
        {
            Instance = this;
        }
    }

    class Derived2 : Base<int>
    {
        public Derived2()
        {
            Instance = this;
        }
    }

}
