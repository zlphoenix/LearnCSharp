using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFuncEventsTrace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to collect...");
            while (true)
            {
                var isContinue = Console.ReadLine();
                if (isContinue != "Q")
                    Do();
            }
        }


        public static void Do()
        {
            AllenProvider.EventWriteFunctionEntry("Do", 0);
            Console.WriteLine("Do sth...");
            AllenProvider.EventWriteFuntionExit(false);
        }
    }
}
