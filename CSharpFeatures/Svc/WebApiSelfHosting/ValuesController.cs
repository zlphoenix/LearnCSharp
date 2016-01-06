using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiSelfHosting
{
    public class ValuesController : ApiController
    {

        public ValuesController()
        {

        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            Console.WriteLine("Posted:" + value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            Console.WriteLine("Posted:" + value);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}