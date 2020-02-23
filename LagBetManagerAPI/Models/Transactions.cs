using System;
using System.ComponentModel.DataAnnotations;

namespace LagBetManagerAPI.Models
{
    public class Transactions
    {

        [Required]
        [StringLength(124)]
        public string CompanyName { get; set; }

        [Required]
        public decimal? Amount { get; set; }

        [Required]
        [StringLength(16)]
        public string CompanyID { get; set; }

        [Required]
        [StringLength(50)]
        public string GameName { get; set; }

        [Required]
        [StringLength(50)]
        public string GameType { get; set; }

        [Required]
        public decimal? TotalAmt { get; set; }

        [Required]
        public decimal? AmountRemmitted { get; set; }

        [Required]
        [StringLength(50)]
        public string TicketNo { get; set; }

        [Required]
        public DateTime? TransactionDate { get; set; }

        [StringLength(124)]
        public string Audit { get; set; }

        public DateTime? DateLogged { get; set; }

        [StringLength(124)]
        public string ReferenceNo { get; set; }
    }

    public class ReportRequest
    {
        [Required]
        [StringLength(124)]
        public string CompanyName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

    }
}