using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DynamicFormEngine
{
    public partial class Starter : Form
    {
        public Starter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var f = new FormPrint();
            //var createControl = f.GetType().GetMethod("CreateControl",
            //                BindingFlags.Instance | BindingFlags.NonPublic);
            //createControl.Invoke(f, new object[] { true });

       
            f.Show();


            var printer = new FormPrinter();
            printer.Print(f);

        }
    }
}
