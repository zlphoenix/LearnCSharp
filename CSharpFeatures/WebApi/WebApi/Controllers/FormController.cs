using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public enum BufferType
    {
        Curr,
        Trans,
        Original
    }
    //[RoutePrefix("")]
    public class FormController : ApiController
    {
        //Retrive Url：http://localhost/webapi/api/form/vm1/id1?bufferType=1&isNeedLock=true&scope=header,child&TotalCount=1000&PageIndex&100&PageSize=20
        /// <summary>
        /// 查询参数之间使用&分隔，逗号不能作为参数分隔
        /// </summary>
        /// <param name="vmId"></param>
        /// <param name="id"></param>
        /// <param name="bufferType"></param>
        /// <param name="isNeedLock"></param>
        /// <param name="scope"></param>
        /// <param name="paging">复杂类型在请求中不需要发json，可以直接展开发，详见例子中的查询字符串</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/form/{vmId}/{Id}")]
        public string Get(string vmId, string id,
            [FromUri] BufferType bufferType = BufferType.Curr,
            [FromUri] bool isNeedLock = false,
            [FromUri] string[] scope = null,
            [FromUri] Paging paging = null)
        {
            return $"Func:{Request.GetFuncSessionId()},Vm:{vmId},RecordId:{id},"
                + $"Buff:{bufferType},NeedLock:{isNeedLock},Scope:{scope.Display()}"
                + $"Paging:{(paging == null ? string.Empty : paging.ToString())}";
        }
        [HttpGet]
        [Route("api/form/{vmId}/{rootId}/{childNode}/{childId}")]
        public string Get(string vmId, string rootId, string childNode, string childId)
        {
            return $"Func:{Request.GetFuncSessionId()},Vm:{vmId},rootId:{rootId},childNode:{childNode},childId:{childId}";
        }
        // POST: api/Form
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Form/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Form/5
        public void Delete(int id)
        {
        }
    }
}
