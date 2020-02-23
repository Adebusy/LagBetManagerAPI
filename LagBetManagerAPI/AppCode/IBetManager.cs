using LagBetManagerAPI.Models;
using System;
using System.Collections.Generic;

namespace LagBetManagerAPI.AppCode
{
    public interface IBetManager
    {
        ResponseMessage LogBetRequest(Transactions transactions);

        List<tbl_Transactions> GetLogReport(string companyName,DateTime startdata, DateTime enddate);

        ResponseMessage DoRequestValidation(Transactions transactions);

        ResponseMessage CheckCompanyAlreadyCreated(BetCompanyRequest betCompanyRequest);

        ResponseMessage CreatNewBetCompany(BetCompanyRequest betCompanyRequest);

        List<RegisteredCompany> GetRegisteredCompanies();
    }
}
