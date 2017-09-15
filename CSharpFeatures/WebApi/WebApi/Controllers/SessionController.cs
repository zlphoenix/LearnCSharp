using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class SessionController : ApiController
    {
        // GET: api/Session
        public string Get()
        {
            return $"Create Func Session:{Guid.NewGuid().ToString()}";
        }

        // GET: api/Session/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Session
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Session/5
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// 通过URI传递GUID没问题，不需要搞到Header或者body
        /// </summary>
        /// <param name="id">SessionId</param>
        /// <returns></returns>
        // DELETE: api/Session/5 
        public string Delete(string id)
        {
            return $"Close Func Session:{id}";
        }
    }
}
