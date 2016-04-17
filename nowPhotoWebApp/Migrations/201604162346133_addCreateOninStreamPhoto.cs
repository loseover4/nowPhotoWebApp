namespace nowPhotoWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreateOninStreamPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StreamPhotoes", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StreamPhotoes", "CreatedOn");
        }
    }
}
