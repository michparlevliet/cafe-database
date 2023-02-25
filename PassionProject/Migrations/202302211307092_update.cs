namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.cafexcoffees", "CafeId", "dbo.cafes");
            DropForeignKey("dbo.cafexcoffees", "CoffeeId", "dbo.coffees");
            DropIndex("dbo.cafexcoffees", new[] { "CafeId" });
            DropIndex("dbo.cafexcoffees", new[] { "CoffeeId" });
            CreateTable(
                "dbo.coffeecafes",
                c => new
                    {
                        coffee_CoffeeId = c.Int(nullable: false),
                        cafe_CafeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.coffee_CoffeeId, t.cafe_CafeId })
                .ForeignKey("dbo.coffees", t => t.coffee_CoffeeId, cascadeDelete: true)
                .ForeignKey("dbo.cafes", t => t.cafe_CafeId, cascadeDelete: true)
                .Index(t => t.coffee_CoffeeId)
                .Index(t => t.cafe_CafeId);
            
            DropTable("dbo.cafexcoffees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.cafexcoffees",
                c => new
                    {
                        CafeCoffeeId = c.Int(nullable: false, identity: true),
                        CafeId = c.Int(nullable: false),
                        CoffeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CafeCoffeeId);
            
            DropForeignKey("dbo.coffeecafes", "cafe_CafeId", "dbo.cafes");
            DropForeignKey("dbo.coffeecafes", "coffee_CoffeeId", "dbo.coffees");
            DropIndex("dbo.coffeecafes", new[] { "cafe_CafeId" });
            DropIndex("dbo.coffeecafes", new[] { "coffee_CoffeeId" });
            DropTable("dbo.coffeecafes");
            CreateIndex("dbo.cafexcoffees", "CoffeeId");
            CreateIndex("dbo.cafexcoffees", "CafeId");
            AddForeignKey("dbo.cafexcoffees", "CoffeeId", "dbo.coffees", "CoffeeId", cascadeDelete: true);
            AddForeignKey("dbo.cafexcoffees", "CafeId", "dbo.cafes", "CafeId", cascadeDelete: true);
        }
    }
}
