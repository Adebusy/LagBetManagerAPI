using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LagBetManagerAPI.Controllers
{
    public class RequestLogController : ApiController
    {
        // GET: api/RequestLog
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RequestLog/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RequestLog
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RequestLog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RequestLog/5
        public void Delete(int id)
        {
        }
    }
}
