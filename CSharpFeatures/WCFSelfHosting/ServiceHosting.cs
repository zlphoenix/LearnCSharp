using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using CSharpFeatures.Serialization;
using TelChina.AF.Util.Logging;
using WCFService;
using WCFService.OuterLab;

namespace WCFSelfHosting
{
    /// <summary>
    /// 自托管方式承载服务
    /// </summary>
    public class ServiceHosting
    {
        static readonly ILogger Logger = LogManager.GetLogger("Server");
        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += SVClose;

            ServiceHost sh = null;


            try
            {
                sh = Start();
                Logger.Debug("Sv Started!");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                var disposer = sh as IDisposable;
                if (disposer != null)
                {
                    sh.Close(new TimeSpan(0, 0, 0, 1));
                    disposer.Dispose();
                }
                sh = null;
            }

        }

        private static ServiceHost Start()
        {
            var sh = new ServiceHost(typeof(MyService2));
            sh.Closing += SVClose;
            sh.Open();

            sh = new ServiceHost(typeof(MyService));
            sh.Closing += SVClose;
            sh.Open();

            sh = new ServiceHost(typeof(CommonCRUDService));
            var cd = sh.Description.Endpoints[0].Contract;
            //查找的是需要更改数据契约
            var myOperationDescription = cd.Operations.Find("Add");

            var serializerBehavior = myOperationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (serializerBehavior == null)
            {
                serializerBehavior = new DataContractSerializerOperationBehavior(myOperationDescription);
                myOperationDescription.Behaviors.Add(serializerBehavior);
            }
            serializerBehavior.DataContractResolver = new MyDataContractResolver(typeof(CommonCRUDService).Assembly);

            sh.Open();
            //var result = PolicyInjection.Create<MyService>();
            ////var result = PolicyInjection.Wrap<MyService>(serviceInstance);
            //result.DoWork();

            return sh;
        }

        private static void SVClose(object sender, EventArgs e)
        {
            Logger.Debug("SV Closing...");
        }
    }
}
