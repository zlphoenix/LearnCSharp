using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Allen.Util.Logging;

namespace Framework
{
    public class Loader
    {
        private static readonly ILogger Logger = LogManager.GetLogger("logger");
        public static T Load<T>(string path)
        {
            //反射加载
            if (File.Exists(path))
            {
                var file = new FileInfo(path);
                OutputAssemblies("Before Load:");
                var ass = Assembly.LoadFile(file.FullName);
                ass.GetReferencedAssemblies();

                var type = (from t in ass.GetTypes()
                            where t.GetInterface(typeof(T).FullName, true) != null
                            select t).FirstOrDefault();

                if (type == null) throw new Exception("type not found!");

                var instance = Activator.CreateInstance(type);
                OutputAssemblies("After Load:");
                return (T)instance;
            }

            return default(T);
        }


        public static void OutputAssemblies(string occur)
        {
            Logger.Debug(occur);
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.FullName);
            foreach (var a in loadedAssemblies)
            {
                Logger.Debug(string.Format("Ass:{0},Location:{1},CodeBae:{2}", a.FullName, a.Location, a.CodeBase));
            }
        }
    }
}
