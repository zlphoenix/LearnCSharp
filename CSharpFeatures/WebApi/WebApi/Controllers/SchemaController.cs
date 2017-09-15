using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class SchemaController : ApiController
    {


        // GET: api/Schema/5
        public Paging Get(string id)
        {
            return new Paging
            {
                PageIndex = 100,
                PageSize = 20,
                TotalCount = 1000
            };
        }

    }
}
