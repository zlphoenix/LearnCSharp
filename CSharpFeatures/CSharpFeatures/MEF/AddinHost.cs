using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Reflection;
//using TelChina.TRF.Util.Logging;
//using TelChina.TRF.Util.Common;

namespace CSharpFeatures.MEF
{
    /// <summary>
    /// 职责:装配服务
    /// </summary>
    public class AddinHost
    {
        static CompositionContainer container;
        /// <summary>
        /// 是否已经初始化
        /// </summary>
        static bool isInit;
        /// <summary>
        /// 装配服务
        /// </summary>
        public void Compose(object Importer)
        {
            if (!isInit)
            {
                container = Init();
                isInit = true;
            }
            //将部件（part）和宿主程序添加到组合容器
            container.ComposeParts(Importer);
            //container.ComposeParts(this, new ComputerBookService());
        }
        //static ILogger log = LogManager.GetLogger("AddinHost");
        private static CompositionContainer Init()
        {
            //log.Debug("AddinHost.Init started");

            DirectoryCatalog catalog = null;
            var aggregateCatalog = new AggregateCatalog();


            catalog = new DirectoryCatalog(Assembly.GetCallingAssembly().Location);

            aggregateCatalog.Catalogs.Add(catalog);


            string currentpath = @".\";
            var currentcatalog = new DirectoryCatalog(currentpath);
            aggregateCatalog.Catalogs.Add(currentcatalog);

            var container = new CompositionContainer(aggregateCatalog);
            return container;
        }
    }
}
