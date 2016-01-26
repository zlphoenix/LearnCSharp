using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace J9Updater.FileTransferSvc
{
    public class Client
    {

        public Client()
        {

        }

        private string GetFileName()
        {

            return "R:\\temp.txt";
        }

        public void Send(string filePath)
        {

            if (string.IsNullOrEmpty(filePath)) filePath = GetFileName();
            var fileInfo = new FileInfo(filePath);
            var fName = fileInfo.Name;

            var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            byte[] fileName = Encoding.UTF8.GetBytes(fName); //file name
            //byte[] fileData = File.ReadAllBytes(filePath); //file
            byte[] fileNameLen = BitConverter.GetBytes(fileName.Length); //lenght of file name
            //var clientData = new byte[4 + fileName.Length + fileData.Length];
            var clientData = new byte[4 + fileName.Length];

            fileNameLen.CopyTo(clientData, 0);
            fileName.CopyTo(clientData, 4);
            //fileData.CopyTo(clientData, 4 + fileName.Length);


            clientSocket.Connect("127.0.0.1", 9050); //target machine's ip address and the port number
            //clientSocket.Send(clientData);
            clientSocket.SendFile(filePath, clientData, null, TransmitFileOptions.WriteBehind);
            clientSocket.Close();
        }

        public void SendFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) filePath = GetFileName();
            var fileInfo = new FileInfo(filePath);
            var fName = fileInfo.Name;
            byte[] preBuffer;
            using (var memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {

                    writer.Write(fName);

                    //writer.Write(md5Hash);
                }
                preBuffer = memoryStream.ToArray();
            }
            var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.Connect("127.0.0.1", 9050);
            clientSocket.SendFile(filePath, preBuffer, null,
                TransmitFileOptions.UseDefaultWorkerThread);
            clientSocket.Close();
        }
    }
}
