using J9Updater.FileTransferSvc.Ver1.Msgs;
using System;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc.Ver1
{
    internal class ApplicationUpdateService : IService
    {
        private Channel Channel { get; set; }
        public Action<string> OnClose;


        public ApplicationUpdateService(Socket socket)
        {
            this.Channel = new Channel(

                 new TransmitConfig
                 {
                     Connection = socket,
                     BufferSize = 81920
                 }
            );//TODo TransConfig
        }

        public void Upload(HandshakeMessage msg)
        {

        }

        public void Download(HandshakeMessage msg)
        {
            throw new System.NotImplementedException();
        }
    }

    internal interface IService
    {

    }
}