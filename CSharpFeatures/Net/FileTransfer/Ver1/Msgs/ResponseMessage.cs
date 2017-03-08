using System;

namespace J9Updater.FileTransferSvc.Ver1.Msgs
{
    public class ResponseMessage : Message
    {
        public ResponseStatus Status { get; set; }

        public ResponseMessage()
        {
        }

        public ResponseMessage(ResponseStatus status)
        {
            this.Status = status;
        }

        public ResponseMessage(byte[] rawMsg) : base(rawMsg)
        {
            if (rawMsg == null || rawMsg.Length == 0) throw new Exception("ÏûÏ¢Îª¿Õ");
            this.Status = (ResponseStatus)Enum.ToObject(typeof(ResponseStatus), rawMsg[1]);
        }
        public override byte[] ToBytes()
        {
            return new[] { Ver, (byte)Status };
        }
    }
}