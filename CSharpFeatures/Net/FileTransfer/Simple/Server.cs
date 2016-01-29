using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace J9Updater.FileTransferSvc
{
    public partial class Server : IDisposable
    {
        Thread t1;
        int flag = 0;
        string receivedPath = "yok";
        public delegate void MyDelegate();

        public Server()
        {
            t1 = new Thread(new ThreadStart(StartListening));
            t1.Start();
        }

        public class StateObject
        {
            // Client socket.
            public Socket workSocket = null;

            public const int BufferSize = 1024;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
        }

        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public void StartListening()
        {
            byte[] bytes = new Byte[1024];
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 9050);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(ipEnd);
                listener.Listen(100);//挂起连接队列的最大长度
                while (true)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();

                }
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                listener.Close();
            }

        }
        public void AcceptCallback(IAsyncResult ar)
        {

            allDone.Set();


            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);


            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, ReadCallbackFileRecive, state);
            flag = 0;
        }

        public void ReadCallback(IAsyncResult ar)
        {

            int fileNameLen = 1;
            String content = String.Empty;
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {

                if (flag == 0)
                {
                    fileNameLen = BitConverter.ToInt32(state.buffer, 0);
                    string fileName = Encoding.UTF8.GetString(state.buffer, 4, fileNameLen);
                    receivedPath = @"R:\ReceivedFiles\" + fileName;
                    flag++;
                }
                if (flag >= 1)
                {
                    BinaryWriter writer = new BinaryWriter(File.Open(receivedPath, FileMode.Append));
                    if (flag == 1)
                    {
                        writer.Write(state.buffer, 4 + fileNameLen, bytesRead - (4 + fileNameLen));
                        flag++;
                    }
                    else
                        writer.Write(state.buffer, 0, bytesRead);
                    writer.Close();
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }

            }
            else
            {
                LabelWriter();
            }

        }
        public void LabelWriter()
        {
            Logging.Debug("Data has been received");
        }
        private void ReadCallbackFileRecive(IAsyncResult ar)
        {
            StateObject tempState = (StateObject)ar.AsyncState;
            Socket handler = tempState.workSocket;
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead <= 0)
            {
                return;
            }

            using (var memoryStream = new MemoryStream(tempState.buffer))
            {
                using (BinaryReader reader = new BinaryReader(memoryStream))
                {
                    //var length = reader.ReadInt32();
                    var filename = reader.ReadString();

                    //var md5Hash = reader.ReadString();
                    var fileData = new byte[memoryStream.Length - memoryStream.Position];
                    reader.Read(fileData, BitConverter.GetBytes(memoryStream.Position).Length, fileData.Length);
                    try
                    {
                        using (var writer = new BinaryWriter(
                            File.Open(Path.Combine(@"R:\ReceivedFiles\", filename), FileMode.Append)))
                        {
                            writer.Write(tempState.buffer, 0, bytesRead);
                        }
                    }
                    catch (Exception error)
                    {
                        Logging.Debug(error.Message);
                        Thread.Sleep(30);
                    }
                    finally
                    {
                        // this method starts a new  AsyncCallback(ReadCallback)
                        // and this method is ReadCallback so it works as a recursive method
                        handler.BeginReceive(tempState.buffer,
                            0,
                            StateObject.BufferSize,
                            0,
                            new AsyncCallback(ReadCallbackFileRecive),
                            tempState);
                    }
                }
            }
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            t1.Abort();
        }
    }

}
