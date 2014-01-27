using System.ComponentModel.Design;
using Microsoft.VisualBasic;

namespace MetroProxy.My
{
    using Microsoft.VisualBasic.ApplicationServices;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"), CompilerGenerated, EditorBrowsable(EditorBrowsableState.Advanced)]
    internal sealed class MySettings : ApplicationSettingsBase
    {
        private static bool addedHandler;
        private static object addedHandlerLockObject = RuntimeHelpers.GetObjectValue(new object());
        private static MySettings defaultInstance = ((MySettings) SettingsBase.Synchronized(new MySettings()));

        [EditorBrowsable(EditorBrowsableState.Advanced), DebuggerNonUserCode]
        private static void AutoSaveSettings(object sender, EventArgs e)
        {
            if (MyProject.Application.SaveMySettingsOnExit)
            {
                MySettingsProperty.Settings.Save();
            }
        }

        public static MySettings Default
        {
            get
            {
                if (!addedHandler)
                {
                    object addedHandlerLockObject = MySettings.addedHandlerLockObject;
                    ObjectFlowControl.CheckForSyncLockOnValueType(addedHandlerLockObject);
                    lock (addedHandlerLockObject)
                    {
                        if (!addedHandler)
                        {
                            MyProject.Application.Shutdown += new ShutdownEventHandler(MySettings.AutoSaveSettings);
                            addedHandler = true;
                        }
                    }
                }
                return defaultInstance;
            }
        }
    }

  
     

        [DebuggerNonUserCode, HideModuleName, CompilerGenerated, StandardModule]
        internal sealed class MySettingsProperty
        {
            [HelpKeyword("My.Settings")]
            internal static MySettings Settings
            {
                get
                {
                    return MySettings.Default;
                }
            }
        }
    }



