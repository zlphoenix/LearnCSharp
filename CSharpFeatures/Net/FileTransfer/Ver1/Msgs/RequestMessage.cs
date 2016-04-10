using System;

namespace J9Updater.FileTransferSvc.Ver1.Msgs
{
    public class RequestMessage : Message
    {
        public RequestMessage()
        {

        }

        public RequestMessage(byte[] rawMsg) : base(rawMsg)
        {

        }
        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}