namespace SmokersTavern.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "ProductPurchaseTotal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "ProductPurchaseTotal");
        }
    }
}
