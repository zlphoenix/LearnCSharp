using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DemoApp
{
    class Program
    {
        private const string Name = "asdfa";
        static void Main(string[] args)
        {


            //Regex.Match("my text", @"\pXXX");

            foreach (var i in new List<int> { 1, 2, 3, 4, 5, 6, 7 })
            {
                string a = "";
            }

            for (int i = 0; i < 100; i++)
            {
                ILcp ilcp = new Lcp();
                //ilcp.Modify();



                var lcp = new Lcp();
                //lcp.Modify();
            }
            while (true)
            {

            }

        }
    }


    public interface ILcp
    {
        void Modify();
    }
    public class Lcp : ILcp
    {
        public void Modify()
        {
            throw new NotImplementedException();
        }
    }
}
