using LagBetManagerAPI.AppCode;
using LagBetManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LagBetManagerAPI.Controllers
{
    public class RequestLogController : ApiController
    {

        public RequestLogController()
        {
            _IBetManager = new BetManager();
        }
        private IBetManager _IBetManager;

        [HttpPost, Route("LogBetDetail")]
        public HttpResponseMessage LogBetDetails([FromBody] Transactions transactions)
        {
            var dp = _IBetManager.LogBetRequest(transactions);
            HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, dp);
            return httpResponseMessage;
        }
    }
}
