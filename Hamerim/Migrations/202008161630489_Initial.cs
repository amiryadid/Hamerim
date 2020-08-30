namespace Hamerim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClubAddresses",
                c => new
                    {
                        ClubId = c.Int(nullable: false),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClubId)
                .ForeignKey("dbo.Clubs", t => t.ClubId)
                .Index(t => t.ClubId);
            
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        ClientName = c.String(nullable: false),
                        ClientPhone = c.String(nullable: false),
                        Club_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clubs", t => t.Club_Id, cascadeDelete: true)
                .Index(t => t.Club_Id);

            Sql("DBCC CHECKIDENT ('dbo.Orders', RESEED, 100000);");

            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Cost = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceCategories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ServiceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceOrders",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        Order_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Id, t.Order_Id })
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.Service_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClubAddresses", "ClubId", "dbo.Clubs");
            DropForeignKey("dbo.ServiceOrders", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ServiceOrders", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Services", "Category_Id", "dbo.ServiceCategories");
            DropForeignKey("dbo.Orders", "Club_Id", "dbo.Clubs");
            DropIndex("dbo.ServiceOrders", new[] { "Order_Id" });
            DropIndex("dbo.ServiceOrders", new[] { "Service_Id" });
            DropIndex("dbo.Services", new[] { "Category_Id" });
            DropIndex("dbo.Orders", new[] { "Club_Id" });
            DropIndex("dbo.ClubAddresses", new[] { "ClubId" });
            DropTable("dbo.ServiceOrders");
            DropTable("dbo.ServiceCategories");
            DropTable("dbo.Services");
            DropTable("dbo.Orders");
            DropTable("dbo.Clubs");
            DropTable("dbo.ClubAddresses");
        }
    }
}
