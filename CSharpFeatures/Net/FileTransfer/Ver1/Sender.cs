using J9Updater.FileTransferSvc.Ver1.Msgs;
using System;
using System.IO;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc.Ver1
{
    /// <summary>
    /// 消息发送器
    /// </summary>
    internal class Sender
    {
        /// <summary>
        /// 发送传输通道配置,用于记录传输方式
        /// </summary>
        private readonly TransmitConfig config;

        /// <summary>
        /// 通过一个Config初始化Sender
        /// </summary>
        /// <param name="config"></param>
        public Sender(TransmitConfig config)
        {
            this.config = config;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg">消息内容，通过序列化为字节流</param>
        /// <param name="callback"></param>
        public void Send(Message msg, Action<Message> callback)
        {
            var msgContent = msg.ToBytes();
            try
            {
                var state = new MsgTransmitState
                {
                    Message = msg,
                    Callback = callback
                };
                config.Connection.BeginSend(msgContent, 0,
                    Math.Min(msgContent.Length, config.BufferSize),
                    0, AfterSendCallBack, state);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                throw;
            }
        }

        private void AfterSendCallBack(IAsyncResult ar)
        {
            try
            {
                SocketError errorCode;
                config.Connection.EndSend(ar, out errorCode);
                if (errorCode != SocketError.Success)
                {
                    Logging.Debug($"Transfre ErrorCode:{errorCode}");
                }
                var state = ar.AsyncState as MsgTransmitState;
                state?.Callback?.Invoke(state.Message);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                throw;
            }
        }

        /// <summary>
        /// 发送数据流
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="callback"></param>
        public void Send(Stream stream, Action<StreamTransmitState> callback)
        {
            if (stream == null || !stream.CanRead)
            {
                throw new Exception("需要传输的流实例,为空或不能被读取");
            }
            try
            {
                var actualBufferSize =
                       (int)Math.Min(stream.Length, config.BufferSize);
                var state = new StreamTransmitState()
                {
                    Stream = stream,
                    Buffer = new byte[actualBufferSize],
                    AfterTransmitCallback = callback
                };

                state.Stream.BeginRead(state.Buffer, 0,
                    actualBufferSize, AfterReadStream, state);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                throw;

            }
        }

        private void AfterReadStream(IAsyncResult ar)
        {
            var state = (StreamTransmitState)ar.AsyncState;
            try
            {
                var readingBytes = state.Stream.EndRead(ar);
                Logging.Debug($"Server:ReadBytesFromFile:{readingBytes}");
                //TransmitedByteCount
                config.Connection.BeginSend(state.Buffer, 0, readingBytes,
                    SocketFlags.None, SendStreamCallBack, state);


                if (state.Stream.Position < state.Stream.Length)
                {
                    var bytesToSend = state.Stream.Length - state.Stream.Position;
                    var actualBufferSize = Math.Min((int)bytesToSend, config.BufferSize);

                    state.Stream.BeginRead(state.Buffer, 0, actualBufferSize,
                        AfterReadStream, state);
                }
                //else
                //{
                //    state.AfterTransmitCallback?.Invoke(state);
                //}
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                throw;

            }
        }


        private void SendStreamCallBack(IAsyncResult ar)
        {
            var state = (StreamTransmitState)ar.AsyncState;
            try
            {
                SocketError error;
                var sentBytes = config.Connection.EndSend(ar, out error);
                state.Count++;
                Logging.Debug(
                    string.Format("Server:ReadCount:{0},Dealed:{1}，SentBytes{2},Error:{3}",
                        state.Count, state.TransmitedByteCount, sentBytes, error));
                if (state.Stream.Position >= state.Stream.Length)
                {
                    state.AfterTransmitCallback?.Invoke(state);
                    //Logging.Debug("Server Listen Complete Response。。。");
                    //state.Connection.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None,
                    //    CloseConnectionCallBack, state);
                }
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                //state.Close();
                throw;

            }
        }
    }
}