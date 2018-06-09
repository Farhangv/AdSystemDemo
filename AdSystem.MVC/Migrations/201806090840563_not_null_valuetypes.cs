namespace AdSystem.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class not_null_valuetypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ads", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Ads", new[] { "CategoryId" });
            AlterColumn("dbo.Ads", "Area", c => c.Int());
            AlterColumn("dbo.Ads", "Age", c => c.Int());
            AlterColumn("dbo.Ads", "UnitCount", c => c.Int());
            AlterColumn("dbo.Ads", "CategoryId", c => c.Int());
            CreateIndex("dbo.Ads", "CategoryId");
            AddForeignKey("dbo.Ads", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ads", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Ads", new[] { "CategoryId" });
            AlterColumn("dbo.Ads", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Ads", "UnitCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Ads", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.Ads", "Area", c => c.Int(nullable: false));
            CreateIndex("dbo.Ads", "CategoryId");
            AddForeignKey("dbo.Ads", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
