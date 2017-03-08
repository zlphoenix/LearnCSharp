using System;
using System.Collections.Generic;
using System.Text;

namespace J9Updater.FileTransferSvc.Ver1.Msgs
{
    public class HandshakeMessage : Message
    {
        public TransmitOption TransmitOption { get; set; }
        public Direction Direction { get; set; }
        public string MessageBody { get; set; }
        public HandshakeMessage()
        {
            Type = MessageType.Handshake;
        }
        public HandshakeMessage(byte[] rawMsg) : base(rawMsg)
        {
            Type = MessageType.Handshake;
            TransmitOption = Util.ReadByteToEnum<TransmitOption>(rawMsg, 1, 2);
            Direction = Util.ReadByteToEnum<Direction>(rawMsg, 3);
            MessageBody = BitConverter.ToString(rawMsg, 4, rawMsg.Length - 4);
        }

        public override byte[] ToBytes()
        {

            var msgByteList = new List<byte> { (byte)(Ver | (byte)Type), 0 };
            //msgByteList[0] = Ver;//Ver
            //msgByteList.Add(0);//RCV
            msgByteList.AddRange(BitConverter.GetBytes((ushort)TransmitOption));//Opt
            msgByteList.Add(1);//FileTransfer
            msgByteList.Add((byte)Direction);
            msgByteList.AddRange(Encoding.UTF8.GetBytes(MessageBody));
            return msgByteList.ToArray();
        }
    }
}