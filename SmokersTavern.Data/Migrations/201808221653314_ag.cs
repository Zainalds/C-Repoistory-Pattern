namespace SmokersTavern.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sales", "ProductPurchaseTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "ProductPurchaseTotal", c => c.Int(nullable: false));
        }
    }
}
