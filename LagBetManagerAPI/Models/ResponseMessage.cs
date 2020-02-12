using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LagBetManagerAPI.Models
{
    public class ResponseMessage
    {
        public string ResponseCode { get; set; }
        public string ResponseDetails { get; set; }
        public string ResponseID { get; set; }

    }
}