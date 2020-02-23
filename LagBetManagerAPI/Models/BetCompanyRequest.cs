using System;
using System.ComponentModel.DataAnnotations;

namespace LagBetManagerAPI.Models
{
    public class BetCompanyRequest
    {
        [Required]
        [StringLength(255)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(255)]
        public string RegNo { get; set; }

    }


    public class RegisteredCompany
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string RegNo { get; set; }

        public string Status { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}