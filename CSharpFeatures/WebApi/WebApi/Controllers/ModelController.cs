using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ModelController : ApiController
    {


        // GET: api/Model/5
        public Model Get(string id)
        {

            return new Model
            {
                Id = id,
                Name = "Allen",
                Detail = new List<string> { "Allen", "Ada" },
                Dic = new Dictionary<string, object> { { "Allen", 100d }, { "Ada", DateTime.Now }, { "April", "Daughter" } }
            };
        }

        // POST: api/Model
        public string Post([FromBody]Model model)
        {
            return model.ToString();
        }

        // PUT: api/Model/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Model/5
        public void Delete(int id)
        {

        }
    }
}
