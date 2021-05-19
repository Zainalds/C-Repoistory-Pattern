namespace SmokersTavern.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaleLogic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductPurchaseName = c.String(),
                        ProductPurchaseQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sales");
        }
    }
}
