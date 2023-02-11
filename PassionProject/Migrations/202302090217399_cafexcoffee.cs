namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cafexcoffee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cafexcoffees",
                c => new
                    {
                        CafeCoffeeId = c.Int(nullable: false, identity: true),
                        CafeId = c.Int(nullable: false),
                        CoffeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CafeCoffeeId)
                .ForeignKey("dbo.cafes", t => t.CafeId, cascadeDelete: true)
                .ForeignKey("dbo.coffees", t => t.CoffeeId, cascadeDelete: true)
                .Index(t => t.CafeId)
                .Index(t => t.CoffeeId);
            
            AddColumn("dbo.reviews", "CafeId", c => c.Int(nullable: false));
            CreateIndex("dbo.reviews", "CafeId");
            AddForeignKey("dbo.reviews", "CafeId", "dbo.cafes", "CafeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.reviews", "CafeId", "dbo.cafes");
            DropForeignKey("dbo.cafexcoffees", "CoffeeId", "dbo.coffees");
            DropForeignKey("dbo.cafexcoffees", "CafeId", "dbo.cafes");
            DropIndex("dbo.reviews", new[] { "CafeId" });
            DropIndex("dbo.cafexcoffees", new[] { "CoffeeId" });
            DropIndex("dbo.cafexcoffees", new[] { "CafeId" });
            DropColumn("dbo.reviews", "CafeId");
            DropTable("dbo.cafexcoffees");
        }
    }
}
