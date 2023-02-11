namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cafes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cafes",
                c => new
                    {
                        CafeId = c.Int(nullable: false, identity: true),
                        CafeName = c.String(),
                        CafeLocation = c.String(),
                    })
                .PrimaryKey(t => t.CafeId);
            
            CreateTable(
                "dbo.coffees",
                c => new
                    {
                        CoffeeId = c.Int(nullable: false, identity: true),
                        CoffeeName = c.String(),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.CoffeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.coffees");
            DropTable("dbo.cafes");
        }
    }
}
