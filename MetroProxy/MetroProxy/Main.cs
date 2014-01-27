using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using MetroProxy.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System.Windows.Forms;
using System.ComponentModel;

namespace MetroProxy
{
    public partial class  Main
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
      
        private long flag;
        private RegistryKey proxy;
        private void Button1_Click(object sender, EventArgs e)
        {
            this.ListBox1.Items.Clear();
            this.ListBox2.Items.Clear();
            RegistryKey r = MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\AppContainer\Mappings");
            if (r == null)
            {
                Interaction.MsgBox("Is It a Windows 8 RP?", MsgBoxStyle.ApplicationModal, null);
            }
            else
            {
                this.readsubkey(r, this.ListBox1, this.ListBox2);
            }
            this.proxy = MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
            this.flag = Conversions.ToLong(this.proxy.GetValue("ProxyEnable"));
            if (this.flag == 0L)
            {
                this.TextBox1.Text = "None";
            }
            else
            {
                this.TextBox1.Text = Conversions.ToString(this.proxy.GetValue("ProxyServer"));
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Interaction.Shell("cmd.exe /c CheckNetIsolation.exe loopbackexempt -a -p=" + this.ListBox1.Text, AppWinStyle.MinimizedFocus, false, -1);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Interaction.Shell("cmd.exe /c CheckNetIsolation.exe loopbackexempt -d -p=" + this.ListBox1.Text, AppWinStyle.MinimizedFocus, false, -1);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Interaction.Shell("cmd.exe /c CheckNetIsolation.exe loopbackexempt -c", AppWinStyle.MinimizedFocus, false, -1);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Interaction.MsgBox(ConsoleOutput.ExcuteCmd("CheckNetIsolation.exe loopbackexempt -s"), MsgBoxStyle.ApplicationModal, null);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text == "")
            {
                Interaction.MsgBox("Please Input The Proxy Ip and Port", MsgBoxStyle.ApplicationModal, null);
            }
            else
            {
                MyProject.Computer.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyEnable", 1);
                MyProject.Computer.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyServer", this.TextBox1.Text);
                this.proxy = MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
                this.flag = Conversions.ToLong(this.proxy.GetValue("ProxyEnable"));
                if (this.flag == 0L)
                {
                    this.TextBox1.Text = "None";
                }
                else
                {
                    this.TextBox1.Text = Conversions.ToString(this.proxy.GetValue("ProxyServer"));
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.proxy = MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
            this.flag = Conversions.ToLong(this.proxy.GetValue("ProxyEnable"));
            if (this.flag == 0L)
            {
                this.TextBox1.Text = "None";
            }
            else
            {
                MyProject.Computer.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyEnable", 0);
                this.TextBox1.Text = "None";
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListBox2.SelectedIndex = this.ListBox1.SelectedIndex;
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ListBox1.SelectedIndex = this.ListBox2.SelectedIndex;
        }

        private void readsubkey(RegistryKey r, ListBox outputbox1, ListBox outputbox2)
        {
            if (r.SubKeyCount > 0)
            {
                string[] subKeyNames = r.GetSubKeyNames();
                int num2 = subKeyNames.GetLength(0) - 1;
                for (int i = 0; i <= num2; i++)
                {
                    outputbox1.Items.Add(subKeyNames[i]);
                    RegistryKey key = r.OpenSubKey(subKeyNames[i]);
                    outputbox2.Items.Add(RuntimeHelpers.GetObjectValue(key.GetValue("DisplayName")));
                }
            }
        }
    }
}
