namespace LagBetManagerAPI
{
    using System.Data.Entity;


    public partial class mybetmodel : DbContext
    {
        public mybetmodel()
            : base("name=mybetmodel")
        {
        }

        public virtual DbSet<tbl_Transactions> tbl_Transactions { get; set; }
        public virtual DbSet<tbl_Companies> tbl_Companies { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Transactions>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Transactions>()
                .Property(e => e.CompanyID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Transactions>()
                .Property(e => e.GameName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Transactions>()
                .Property(e => e.GameType)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Transactions>()
                .Property(e => e.TicketNo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Transactions>()
                .Property(e => e.Audit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Transactions>()
                .Property(e => e.ReferenceNo)
                .IsUnicode(false);
        }
    }
}
