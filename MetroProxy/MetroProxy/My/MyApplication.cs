namespace MetroProxy.My
{
    using Microsoft.VisualBasic.ApplicationServices;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("MyTemplate", "10.0.0.0")]
    internal class MyApplication : WindowsFormsApplicationBase
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        [DebuggerStepThrough]
        public MyApplication() : base(AuthenticationMode.Windows)
        {
            __ENCAddToList(this);
            this.IsSingleInstance = false;
            this.EnableVisualStyles = true;
            this.SaveMySettingsOnExit = true;
            this.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining), STAThread, DebuggerHidden, EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static void Main(string[] Args)
        {
            try
            {
                Application.SetCompatibleTextRenderingDefault(WindowsFormsApplicationBase.UseCompatibleTextRendering);
            }
            finally
            {
            }
            MyProject.Application.Run(Args);
        }

        [DebuggerStepThrough]
        protected override void OnCreateMainForm()
        {
            this.MainForm = MyProject.Forms.Main;
        }
    }
}

