using J9Updater.FileTransferSvc.Ver1.Msgs;
using System;

namespace J9Updater.FileTransferSvc.Ver1
{
    public class MsgTransmitState
    {
        public Message Message { get; set; }
        public Action<Message> Callback { get; set; }
        public byte[] Buffer { get; set; }
    }
}