using LagBetManagerAPI.AppCode;
using LagBetManagerAPI.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LagBetManagerAPI.Controllers
{
    public class RequestLogController : ApiController
    {
        public RequestLogController()
        {
            _IBetManager = new BetManager();
            _HttpResponseMessage = new HttpResponseMessage();
        }
        private IBetManager _IBetManager;
        private HttpResponseMessage _HttpResponseMessage;

        [HttpPost, Route("LogBetDetail")]
        public HttpResponseMessage LogBetDetails([FromBody] Transactions transactions)
        {
            _ = new HttpResponseMessage();
            var docheckRequestBody = _IBetManager.DoRequestValidation(transactions);
            HttpResponseMessage httpResponseMessage;
            if (docheckRequestBody.ResponseCode != "00")
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, docheckRequestBody);
                return httpResponseMessage;
            }
            var dp = _IBetManager.LogBetRequest(transactions);
            httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, dp);
            return httpResponseMessage;
        }

        [HttpPost, Route("GenerateLogReport")]
        public HttpResponseMessage FetchLogReport([FromBody] ReportRequest reportRequest)
        {
            try
            {
                Convert.ToDateTime(reportRequest.StartDate);
            }
            catch (Exception)
            {
                _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, "Startdate not in correct format.");
                return _HttpResponseMessage;
            }

            try
            {
                Convert.ToDateTime(reportRequest.EndDate);
            }
            catch (Exception)
            {
                _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, "Enddate not in correct format.");
                return _HttpResponseMessage;
            }


            if (string.IsNullOrEmpty(reportRequest.CompanyName))
            {
                _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, "CompanyName is required.");
                return _HttpResponseMessage;
            }

            var dp = _IBetManager.GetLogReport(reportRequest.CompanyName, reportRequest.StartDate, reportRequest.EndDate);
            _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, dp);
            return _HttpResponseMessage;
        }

        [HttpPost, Route("CreateBetCompany")]
        public HttpResponseMessage CreateBetCompany([FromBody] BetCompanyRequest betCompanyRequest)
        {
            if (string.IsNullOrEmpty(betCompanyRequest.CompanyName) || betCompanyRequest.CompanyName.Trim() == "string")
            {
                _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, "Company Name is required.");
                return _HttpResponseMessage;
            }

            if (string.IsNullOrEmpty(betCompanyRequest.RegNo) || betCompanyRequest.CompanyName.Trim() == "string")
            {
                _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, "Company registration number is required.");
                return _HttpResponseMessage;
            }
            //check before insert
            var doCheck = _IBetManager.CheckCompanyAlreadyCreated(betCompanyRequest);

            if (doCheck.ResponseCode == "00")
            {
                //do creation
                var doCreation = _IBetManager.CreatNewBetCompany(betCompanyRequest);

                if (doCreation.ResponseCode == "00")
                {
                    _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, "Company created successfully.");
                    return _HttpResponseMessage;
                }
                else
                {
                    _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, "Unable to create company at the momment.");
                    return _HttpResponseMessage;
                }
            }
            else
            {
                _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, doCheck);
                return _HttpResponseMessage;
            }
        }

        [HttpGet, Route("GetRegisteredCompany")]
        public HttpResponseMessage GetRegisteredCompany()
        {
            _HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, _IBetManager.GetRegisteredCompanies());
            return _HttpResponseMessage;
        }
    }
}
