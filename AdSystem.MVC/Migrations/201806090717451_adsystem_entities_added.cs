namespace AdSystem.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adsystem_entities_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Area = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        UnitCount = c.Int(nullable: false),
                        ThumbnailPhotoPath = c.String(maxLength: 150),
                        CategoryId = c.Int(nullable: false),
                        Diposite = c.Int(),
                        Rent = c.Int(),
                        PricePerUnit = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        AdId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ads", t => t.AdId, cascadeDelete: true)
                .Index(t => t.AdId);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FileContent = c.Binary(),
                        Extension = c.String(),
                        OriginalFileName = c.String(),
                        MimeType = c.String(),
                        FileSize = c.Int(nullable: false),
                        AdId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ads", t => t.AdId, cascadeDelete: true)
                .Index(t => t.AdId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Media", "AdId", "dbo.Ads");
            DropForeignKey("dbo.Features", "AdId", "dbo.Ads");
            DropForeignKey("dbo.Ads", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Media", new[] { "AdId" });
            DropIndex("dbo.Features", new[] { "AdId" });
            DropIndex("dbo.Ads", new[] { "CategoryId" });
            DropTable("dbo.Media");
            DropTable("dbo.Features");
            DropTable("dbo.Categories");
            DropTable("dbo.Ads");
        }
    }
}
