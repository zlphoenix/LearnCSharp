using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel.Channels;
using System.ServiceModel;
using WCFClient.MyService;


namespace WCFClient
{
    internal class MyServiceAgent : ProxyBase, IMyService
    {
        public MyServiceAgent()
        {
            var result = GetChannel<IMyService>();
            Channel = result;
        }

        private IMyService Channel { get; set; }

        public void DoWork()
        {
            try
            {
                this.Channel.DoWork();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Exception {0} occur,Trace:{1} ", e.GetType().ToString(), e.StackTrace));
                throw;
            }
            finally
            {
                var realChannel = Channel as IDisposable;
                if (realChannel != null)
                {
                    realChannel.Dispose();
                }
            }
        }
    }

    public class ProxyBase
    {
        private static T GetChannel<T>(Binding binding, EndpointAddress endpointAddress)
        {
            var channelFactory =
                new ChannelFactory<T>(binding, endpointAddress);
            T bpChannel = channelFactory.CreateChannel();
            return bpChannel;
        }
        protected static T GetChannel<T>()
        {
            var binding = new WSHttpBinding { TransactionFlow = true };
            var result = GetChannel<T>(
                binding,
                new EndpointAddress("http://localhost:1234/" + typeof(T).FullName));
            return result;
        }
    }
}