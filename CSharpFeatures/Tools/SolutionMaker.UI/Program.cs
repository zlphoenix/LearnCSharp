using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace SolutionMaker.UI
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
