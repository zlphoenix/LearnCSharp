using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.BCL
{
    public class LazyInit
    {
        public void Lazy()
        {
            int cout = 0;
            var lazyString = new Lazy<string>(
                () =>
                {
                    cout++;
                    // Here you can do some complex processing
                    // and then return a value.
                    Console.Write("Inside lazy loader");
                    return "Lazy loading!";
                });
            Console.Write("Is value created: ");
            Console.WriteLine(lazyString.IsValueCreated);

            Console.Write("Value: ");
            Console.WriteLine(lazyString.Value);

            Console.Write("Value again: ");
            Console.WriteLine(lazyString.Value);

            Console.Write("Is value created: ");
            Console.WriteLine(lazyString.IsValueCreated);

            Console.WriteLine("Press any key to continue ...");
            Console.ReadLine();
        }
    }
}
