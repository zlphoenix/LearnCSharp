namespace MetroProxy
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    public class ConsoleOutput
    {
        private static string gWorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        private ConsoleOutput()
        {
        }

        public static string ExcuteCmd(string command)
        {
            string str2 = "";
            Process process2 = new Process();
            ProcessStartInfo startInfo = process2.StartInfo;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = startInfo.EnvironmentVariables["ComSpec"];
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.Arguments = string.Format("/C {0}", command);
            startInfo.WorkingDirectory = gWorkingDirectory;
            startInfo = null;
            try
            {
                process2.Start();
                process2.WaitForExit(0x1388);
                str2 = process2.StandardOutput.ReadToEnd();
            }
            catch (Win32Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                str2 = exception1.ToString();
                ProjectData.ClearProjectError();
            }
            process2 = null;
            return str2;
        }

        public static string WorkingDirectory
        {
            get
            {
                return gWorkingDirectory;
            }
            set
            {
                gWorkingDirectory = value;
            }
        }
    }
}

