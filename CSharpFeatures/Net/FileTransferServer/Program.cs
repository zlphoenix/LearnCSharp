using J9Updater.FileTransferSvc;
using J9Updater.FileTransferSvc.Ver1;
using System;

namespace FileTransferServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //var server = new Server();
            var service = new ServiceHost();
            service.Start();

            var client = new TcpFileTransmitServiceClient();
            while (true)
            {
                try
                {
                    Console.WriteLine("FilePath:");
                    var path = Console.ReadLine();
                    Logging.sw.Reset();
                    Logging.sw.Start();
                    client.Upload(path, (state) => client.DownLoad(state.FileName, null));

                    ;
                }
                catch (Exception ex)
                {

                    OutputMsg(ex);
                }
            }
        }

        private static void OutputMsg(Exception ex)
        {
            Console.WriteLine("{0},Error Occur", DateTime.Now);
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.GetType());
            Console.WriteLine(ex.StackTrace);

            if (ex.InnerException != null)
            {
                OutputMsg(ex.InnerException);
            }

        }
    }
}
