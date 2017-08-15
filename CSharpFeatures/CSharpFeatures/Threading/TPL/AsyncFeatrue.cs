using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpFeatures.TPL
{
    public class AsyncFeatrue
    {
        public async Task<int> AccessTheWebAsync()
        {
            // You need to add a reference to System.Net.Http to declare client.
            var client = new HttpClient();

            // GetStringAsync returns a Task<string>. That means that when you await the
            // task you'll get a string (urlContents).
            Task<string> getStringTask = client.GetStringAsync("https://www.google.com");

            // You can do work here that doesn't rely on the string from GetStringAsync.
            DoIndependentWork();

            // The await operator suspends AccessTheWebAsync.
            //  - AccessTheWebAsync can't continue until getStringTask is complete.
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.
            //  - Control resumes here when getStringTask is complete. 
            //  - The await operator then retrieves the string result from getStringTask.
            string urlContents = await getStringTask;
            Console.WriteLine("Thread:{0},is done", Thread.CurrentThread.ManagedThreadId);
            // The return statement specifies an integer result.
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.
            return urlContents.Length;
        }

        private void DoIndependentWork()
        {

            Console.WriteLine("Do Independet Work...");
            //throw new Exception("");
        }
    }
}
