using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using log4net.Config;

namespace Allen.Util.Logging
{
    public static class LogManager
    {
        static LogManager()
        {
            var ass = System.Reflection.Assembly.GetExecutingAssembly();
            var path = GetConfigPath(ass);
            if (!File.Exists(path))
            {
                var res = ass.FullName.Split(',')[0];
                var resName = res + "." + res + ".dll.config";
                //var resName = "Allen.Util.Logging.g.resources";
                var stream = ass.GetManifestResourceStream(resName);
                //ass.GetManifestResourceStream("Allen.Util.LogManager.Allen.Util.LogManager.config");
                if (stream == null) throw new FileNotFoundException("config");
                XmlConfigurator.Configure(stream);
                //DOMConfigurator.Configure(stream);
            }
            else
            {
                XmlConfigurator.Configure(new FileInfo(path));
                //DOMConfigurator.Configure(new FileInfo(path));
            }

            //log4net.Config.DOMConfigurator.Configure();
        }

        private static string GetConfigPath(Assembly ass)
        {
            //不能使用Location属性因为在IIS下Location返回临时目录中的Dll路径
            //file:///
            var path = new Uri(ass.CodeBase + ".config");
            return path.LocalPath;
        }

        public static ILogger GetLogger(string name)
        {
            return new Logger(log4net.LogManager.GetLogger(name));
        }
    }
}
