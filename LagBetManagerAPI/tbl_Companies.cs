namespace LagBetManagerAPI
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_Companies
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string CompanyName { get; set; }

        [StringLength(255)]
        public string RegNo { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public DateTime? DateAdded { get; set; }

        [StringLength(255)]
        public string Audit { get; set; }
    }
}