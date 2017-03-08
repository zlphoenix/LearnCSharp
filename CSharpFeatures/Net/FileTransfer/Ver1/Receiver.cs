using J9Updater.FileTransferSvc.Ver1.Msgs;
using System;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc.Ver1
{
    internal class Receiver
    {
        /// <summary>
        /// 发送传输通道配置,用于记录传输方式
        /// </summary>
        private readonly TransmitConfig config;

        /// <summary>
        /// 通过一个Config初始化Sender
        /// </summary>
        /// <param name="config"></param>
        public Receiver(TransmitConfig config)
        {
            this.config = config;
        }
        void Recieve(Action<Message> callback)
        {
            try
            {
                var state = new MsgTransmitState
                {
                    Callback = callback,
                    Buffer = new byte[config.BufferSize],
                };

                config.Connection.BeginReceive(state.Buffer,
                    0, config.BufferSize, SocketFlags.None, AfterReceiveMsgCallback, state);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                throw;
            }
        }

        private void AfterReceiveMsgCallback(IAsyncResult ar)
        {
            try
            {
                SocketError errorCode;
                var receiveBytes = config.Connection.EndReceive(ar, out errorCode);
                if (errorCode != SocketError.Success)
                {
                    Logging.Debug($"Transfre ErrorCode:{errorCode}");
                }
                var state = (MsgTransmitState)ar.AsyncState;
                state.Message = Message.CreateMessage(state.Buffer);
                state.Callback?.Invoke(state.Message);
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                throw;
            }
        }

        void Recieve(StreamTransmitState state, Action<StreamTransmitState> callback)
        {
            if (state.Stream == null || !state.Stream.CanWrite)
            {
                throw new Exception("用于接收的流实例,为空或不能被写入");
            }
            try
            {
                var actualBufferSize =
                    (int)Math.Min(state.Size, config.BufferSize);
                state.Buffer = new byte[actualBufferSize];
                state.AfterTransmitCallback = callback;

                config.Connection.BeginReceive(state.Buffer,
                    0, (int)Math.Min(config.BufferSize, state.Size), SocketFlags.None,
                    AfterReceiveStreamCallback, state);
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                throw;
            }

        }

        private void AfterReceiveStreamCallback(IAsyncResult ar)
        {
            var state = (StreamTransmitState)ar.AsyncState;
            try
            {
                var bytesRead = config.Connection.EndReceive(ar);
                if (bytesRead > 0)
                {

                    state.Stream.BeginWrite(state.Buffer, 0,
                        bytesRead, AfterWriteStreamCallback, state);
                }
                else
                {
                    throw new Exception("接受的数据流字节数为0");
                }
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                throw;

            }
        }

        private void AfterWriteStreamCallback(IAsyncResult ar)
        {
            var state = (StreamTransmitState)ar.AsyncState;
            try
            {
                state.Stream.EndWrite(ar);
                state.Count++;
                Logging.Debug(
                    string.Format("Server:WriteCount:{0},Dealed:{1}",
                    state.Count, state.TransmitedByteCount));
                if (state.Size > state.Stream.Position)
                {
                    state.TransmitedByteCount += state.DealingByteCount;//写入成功后,累计已写入字节数
                }
                else
                {
                    state.AfterTransmitCallback?.Invoke(state);
                    Logging.Debug(
                 string.Format("Server:WriteFile finished"));
                    //state.Buffer = new byte[] { 1, 0x20 };
                    //state.Connection.BeginSend(state.Buffer, 0, state.Buffer.Length,
                    //    SocketFlags.None, UploadFinishingCallBack, state);
                }
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                throw;
            }
        }
    }
}