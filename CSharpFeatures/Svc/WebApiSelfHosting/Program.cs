using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApiSelfHosting
{
    class Program
    {


        static void Main()
        {
            string baseAddress = "http://localhost:10282/";

            // Start OWIN host
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders
       .Accept
       .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                HttpResponseMessage response = client.GetAsync(baseAddress + "api/values").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine();
        }


    }
}
