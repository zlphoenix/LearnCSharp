using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.Iterators
{
    public class DaysOfTheWeek : System.Collections.IEnumerable
    {
        string[] m_Days = { "Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat" };

        public System.Collections.IEnumerator GetEnumerator()
        {
            for (int i = 0; i < m_Days.Length; i++)
            {
                yield return m_Days[i];
            }
        }
    }

    public class TestDaysOfTheWeek
    {
        public static void Do()
        {
            // Create an instance of the collection class
            DaysOfTheWeek week = new DaysOfTheWeek();

            // Iterate with foreach
            foreach (string day in week)
            {
                System.Console.Write(day + " ");
            }
        }
    }
}
