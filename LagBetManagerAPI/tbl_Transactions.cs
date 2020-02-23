namespace LagBetManagerAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Transactions
    {
        public int Id { get; set; }

        [StringLength(124)]
        public string CompanyName { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(50)]
        public string CompanyID { get; set; }

        [StringLength(50)]
        public string GameName { get; set; }

        [StringLength(50)]
        public string GameType { get; set; }

        public decimal? TotalAmt { get; set; }

        public decimal? AmountRemmitted { get; set; }

        [StringLength(50)]
        public string TicketNo { get; set; }

        public DateTime? TransactionDate { get; set; }

        [StringLength(50)]
        public string Audit { get; set; }

        public DateTime? DateLogged { get; set; }

        [StringLength(128)]
        public string ReferenceNo { get; set; }
    }
}
