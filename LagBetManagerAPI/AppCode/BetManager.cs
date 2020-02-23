
using LagBetManagerAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace LagBetManagerAPI.AppCode
{
    public class BetManager : IBetManager
    {

        private readonly mybetmodel db = new mybetmodel();
        private readonly ILog log = LogManager.GetLogger("mylog");
        private ResponseMessage responseMsg = new ResponseMessage();
        public ResponseMessage LogBetRequest(Transactions transactions)
        {
            var recID = 0;
            var ResponseMsg = new ResponseMessage();
            log.Info("Request with ID" + transactions.TicketNo + "got here");
            try
            {
                transactions.ReferenceNo = GenerateReferenceNo(transactions);
                //check request already logged
                var doccheck = CheckIfRequestAlreadySubmit(transactions);
                if (!string.IsNullOrEmpty(doccheck))
                {
                    ResponseMsg.ResponseCode = "03";
                    ResponseMsg.ResponseDetails = "Request already logged with reference number" + doccheck;
                    ResponseMsg.ResponseID = recID.ToString();
                }
                else
                {
                    // do insertion
                    var transaction = new tbl_Transactions
                    {
                        ReferenceNo = transactions.ReferenceNo,
                        CompanyName = transactions.CompanyName,
                        Amount = transactions.Amount,
                        CompanyID = transactions.CompanyID,
                        GameName = transactions.GameName,
                        GameType = transactions.GameType,
                        TotalAmt = transactions.TotalAmt,
                        AmountRemmitted = transactions.AmountRemmitted,
                        TicketNo = transactions.TicketNo,
                        TransactionDate = transactions.TransactionDate,
                        Audit = "Request logged",
                        DateLogged = DateTime.Now
                    };
                    db.tbl_Transactions.Add(transaction);
                    recID = db.SaveChanges();
                    ResponseMsg.ResponseCode = "00";
                    ResponseMsg.ResponseDetails = "Request logged successfully.";
                    ResponseMsg.ResponseID = transaction.ReferenceNo;
                }
            }
            catch (Exception ex)
            {
                ResponseMsg.ResponseCode = "X01";
                ResponseMsg.ResponseDetails = "Error Occurred. Please try again later.";
                ResponseMsg.ResponseID = recID.ToString();
                log.Error(ex);
            }
            return ResponseMsg;
        }
        public string GenerateReferenceNo(Transactions transactions)
        {
            return transactions.CompanyName.Substring(0, 2).ToUpper() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond;
        }
        private string CheckIfRequestAlreadySubmit(Transactions transactions)
        {
            var resp = "";
            var req = db.tbl_Transactions.Where(c => c.CompanyID == transactions.CompanyID).
                Where(d => d.CompanyName == transactions.CompanyName.Trim()).
                Where(k => k.TicketNo == transactions.TicketNo.Trim()).FirstOrDefault();
            if (req != null)
            {
                resp = req.ReferenceNo;
            }
            return resp;
        }

        List<tbl_Transactions> GetLogReport(DateTime startdata, DateTime enddate)
        {
            var query = db.tbl_Transactions.Where(x => x.DateLogged >= startdata).Where(s => s.DateLogged <= enddate).ToList();
            return query;
        }

        [Obsolete]
        List<tbl_Transactions> IBetManager.GetLogReport(string companyName, DateTime startdata, DateTime enddate)
        {
            var query = db.tbl_Transactions.Where(c => c.CompanyName == companyName.Trim().ToUpper()).Where(x => EntityFunctions.TruncateTime(x.DateLogged) >= startdata).Where(s => EntityFunctions.TruncateTime(s.DateLogged) <= enddate).ToList();
            return query;
        }

        public ResponseMessage DoRequestValidation(Transactions transactions)
        {
            var ResponseMsg = new ResponseMessage();
            ResponseMsg.ResponseCode = "00";
            try
            {
                var ddd = Convert.ToDecimal(transactions.Amount);
            }
            catch (Exception e)
            {
                ResponseMsg.ResponseCode = "01";
                ResponseMsg.ResponseDetails = "Transaction amount must be atleast two decila";
            }

            try
            {
                var ddd = Convert.ToDecimal(transactions.AmountRemmitted);
            }
            catch (Exception e)
            {
                ResponseMsg.ResponseCode = "01";
                ResponseMsg.ResponseDetails = "Transaction Amount Remmitted must be atleast two decila";
            }

            try
            {
                var ddd = Convert.ToDecimal(transactions.TotalAmt);
            }
            catch (Exception e)
            {
                ResponseMsg.ResponseCode = "01";
                ResponseMsg.ResponseDetails = "Transaction Total Amount must be atleast two decila";
            }

            try
            {
                if (string.IsNullOrEmpty(transactions.CompanyID) || transactions.CompanyID.ToString().ToLower() == "string")
                {
                    ResponseMsg.ResponseCode = "01";
                    ResponseMsg.ResponseDetails = "Company ID is required";
                }
            }
            catch (Exception e)
            {
                ResponseMsg.ResponseCode = "01";
                ResponseMsg.ResponseDetails = "Company ID is required";
            }

            try
            {
                if (string.IsNullOrEmpty(transactions.CompanyName) || transactions.CompanyName.ToString().ToLower() == "string")
                {
                    ResponseMsg.ResponseCode = "01";
                    ResponseMsg.ResponseDetails = "Company name is required";
                }
            }
            catch (Exception e)
            {
                ResponseMsg.ResponseCode = "01";
                ResponseMsg.ResponseDetails = "Company name is required";
            }

            try
            {
                if (string.IsNullOrEmpty(transactions.GameName) || transactions.GameName.ToString().ToLower() == "string")
                {
                    ResponseMsg.ResponseCode = "01";
                    ResponseMsg.ResponseDetails = "Game name is required";
                }
            }
            catch (Exception e)
            {
                ResponseMsg.ResponseCode = "01";
                ResponseMsg.ResponseDetails = "Game name is required";
            }

            try
            {
                if (string.IsNullOrEmpty(transactions.ReferenceNo) || transactions.ReferenceNo.ToString().ToLower() == "string")
                {
                    ResponseMsg.ResponseCode = "01";
                    ResponseMsg.ResponseDetails = "Reference No is required";
                }
            }
            catch (Exception e)
            {
                ResponseMsg.ResponseCode = "01";
                ResponseMsg.ResponseDetails = "Company ID is required";
            }

            return ResponseMsg;
        }

        public ResponseMessage CheckCompanyAlreadyCreated(BetCompanyRequest betCompanyRequest)
        {
            var query = db.tbl_Companies.Where(x => x.CompanyName == betCompanyRequest.CompanyName.ToUpper().Trim()).FirstOrDefault();
            if (query != null)
            {
                responseMsg.ResponseCode = "01";
                responseMsg.ResponseDetails = "Company name already exist with registration number " + query.RegNo;
            }
            else
            {
                responseMsg.ResponseCode = "00";
                responseMsg.ResponseDetails = "Company name does not exist";
            }
            return responseMsg;
        }

        public ResponseMessage CreatNewBetCompany(BetCompanyRequest betCompanyRequest)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            var betcomp = new tbl_Companies
            {
                CompanyName = betCompanyRequest.CompanyName.ToUpper().Trim(),
                RegNo = betCompanyRequest.RegNo.ToUpper().Trim(),
                Status = "00",
                DateAdded = DateTime.Now,
                Audit = "New company created"
            };
            db.tbl_Companies.Add(betcomp);
            var doInsert = db.SaveChanges();
            if (doInsert > 0)
            {
                responseMessage.ResponseCode = "00";
                responseMessage.ResponseDetails = "Company created successfully.";
            }
            else
            {
                responseMessage.ResponseCode = "01";
                responseMessage.ResponseDetails = "unable to create company at the moment";
                log.Error("unable to save company creation request " + doInsert.ToString());
            }
            return responseMessage;
        }

        public List<RegisteredCompany> GetRegisteredCompanies()
        {
            var Regcompanies = new List<RegisteredCompany>();
            var query = db.tbl_Companies.ToList();
            if (query != null)
            {
                foreach (var rc in query)
                {
                    var comp = new RegisteredCompany
                    {
                        Id = rc.Id,
                        RegNo = rc.RegNo,
                        CompanyName = rc.CompanyName,
                        Status = rc.Status,
                        DateAdded = rc.DateAdded
                    };
                    Regcompanies.Add(comp);
                }
            }
            return Regcompanies;
        }
    }
}