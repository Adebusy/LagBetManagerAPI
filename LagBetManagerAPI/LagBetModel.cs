namespace LagBetManagerAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LagBetModel : DbContext
    {
        public LagBetModel()
            : base("name=LagBetModel")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LagBetModel, Migrations.Configuration>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LagBetModel>());
        }

        public virtual DbSet<tbl_Transactions> tbl_Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Transactions>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);
        }
    }
}
