using System;
using WCFService.OuterLab;
using System.ServiceModel.Description;
using CSharpFeatures.Serialization;
using Allen.Util.Logging;

namespace WCFClient
{
    class MyClient
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetLogger("Client");
            //MyService.MyServiceClient client = new MyServiceClient();
            //client.DoWork();
            try
            {
                

                var client = new CommonCRUDService.CommonCRUDServiceClient();
                var cd = client.Endpoint.Contract;
                //查找的是需要更改数据契约
                var myOperationDescription = cd.Operations.Find("Add");

                var serializerBehavior = myOperationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (serializerBehavior == null)
                {
                    serializerBehavior = new DataContractSerializerOperationBehavior(myOperationDescription);
                    myOperationDescription.Behaviors.Add(serializerBehavior);
                }
                serializerBehavior.DataContractResolver = new MyDataContractResolver(typeof(ICommonCRUDService).Assembly);

                client.Add(new BusinessEntity() { Code = "MyBizDTO", Name = "MYDTO" });
                client.Close();

                //CodeTimer.Initialize();
                //CodeTimer.Time("预热", 1, SVInvoke);
                //CodeTimer.Time("1000次服务执行", 1000, SVInvoke);
                //SVInvoke();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            Console.ReadLine();

        }

        private static void SVInvoke()
        {
            //var sv = new ServiceReference1.Service1Client();
            //sv.DoWork();
            MyServiceClient client = new MyServiceClient();
            client.DoWork();
            client.Close();



            //MyService2Client client = new MyService2Client();
            //client.DoWork();

            //var channelFactory = new ChannelFactory<IMyService>("WSHttpBinding_IMyService");
            //var client1 = channelFactory.CreateChannel();
            //var os = proxy.MyTransparentProxy.Create<IMyService>(client1);
            //os.DoWork();
        }
    }
}
