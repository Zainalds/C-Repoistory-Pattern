namespace SmokersTavern.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateLogic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "ProductPurchaseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "ProductPurchaseDate");
        }
    }
}
