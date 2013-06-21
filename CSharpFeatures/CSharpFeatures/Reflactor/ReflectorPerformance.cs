using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.Reflactor
{
    public class ReflectorPerformance
    {
        public void InvokeWithoutLoad()
        {

        }

        public void Invoke(int first, int second)
        {
            int k = 0;
            for (int i = 0; i < first; i++)
            {
                for (int j = 0; j < second; j++)
                {
                    k++;
                }
            }
        }
    }

    public class Entity
    {
        public long Field1 { get; set; }
        public DateTime Field2 { get; set; }
        public String Field3 { get; set; }
        public List<String> Field4 { get; set; }
        public int Field5 { get; set; }
        public decimal Field6 { get; set; }
        public bool Field7 { get; set; }
        public object Field8 { get; set; }
        public Dictionary<string,string> Field9 { get; set; }
        public Enum Field10 { get; set; }
    }
}
