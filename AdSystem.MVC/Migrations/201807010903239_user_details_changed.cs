namespace AdSystem.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_details_changed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Family", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Family");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
