using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CSharpFeatures.WCF.Binding
{
    public class Server
    {
        public void StartSv()
        {
            var address = new Uri("http://127.0.0.1:9999/messagingviabinding");
            BasicHttpBinding binding = new BasicHttpBinding();
            IChannelListener<IReplyChannel> channelListener = binding.BuildChannelListener<IReplyChannel>(address);
            channelListener.Open();
            IReplyChannel channel = channelListener.AcceptChannel();
            channel.Open();
            Console.WriteLine("Begin to listen  ");
            while (true)
            {
                RequestContext context = channel.ReceiveRequest(new TimeSpan(1, 0, 0));
                Console.WriteLine("Receive a request message:\n{0}", context.RequestMessage);
                Message replyMessage = Message.CreateMessage(MessageVersion.Soap11, "http://artech.messagingviabinding", "This is a mannualy created reply message for the purpose of testing");
                context.Reply(replyMessage);
            }
        }
    }
}
