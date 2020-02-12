
using LagBetManagerAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagBetManagerAPI.AppCode
{
    public class BetManager : IBetManager
    {
        private readonly LagBetModel db = new LagBetModel();
        private readonly ILog log = LogManager.GetLogger("mylog");
        public ResponseMessage LogBetRequest(Transactions transactions)
        {
            var recID = 0;
            var ResponseMsg = new ResponseMessage();
            log.Info("Request with ID"+transactions.TicketNo+"got here");
            try
            {
                //check request already logged
                var doccheck = CheckIfRequestAlreadySubmit(transactions);
                if (!string.IsNullOrEmpty(doccheck))
                {
                    ResponseMsg.ResponseCode = "03";
                    ResponseMsg.ResponseDetails = "Request already logged with reference number"+ doccheck;
                    ResponseMsg.ResponseID = recID.ToString();
                }
                else
                {
                    // do insertion
                    var transaction = new tbl_Transactions
                    {
                        ReferenceNo = GenerateReferenceNo(transactions),
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
                ResponseMsg.ResponseDetails = "Error Occurred. Please try again later";
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
    }
}