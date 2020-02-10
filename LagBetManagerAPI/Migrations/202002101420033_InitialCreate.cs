namespace LagBetManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 124, unicode: false),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        CompanyID = c.String(maxLength: 16, unicode: false),
                        GameName = c.String(maxLength: 50, unicode: false),
                        GameType = c.String(maxLength: 50, unicode: false),
                        TotalAmt = c.Decimal(precision: 18, scale: 2),
                        AmountRemmitted = c.Decimal(precision: 18, scale: 2),
                        TicketNo = c.String(maxLength: 50, unicode: false),
                        TransactionDate = c.DateTime(),
                        Audit = c.String(maxLength: 124, unicode: false),
                        DateLogged = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_Transactions");
        }
    }
}
