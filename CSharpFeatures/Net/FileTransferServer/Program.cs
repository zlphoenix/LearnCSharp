﻿using J9Updater.FileTransferSvc;
using System;

namespace FileTransferServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();


            var client = new Client();
            while (true)
            {
                try
                {
                    Console.WriteLine("FilePath:");
                    var path = Console.ReadLine();

                    client.Send(path);
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
