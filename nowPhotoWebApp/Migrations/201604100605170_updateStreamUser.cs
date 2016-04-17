namespace nowPhotoWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateStreamUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StreamUsers", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.StreamUsers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StreamUsers", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.StreamUsers", "UserName");
        }
    }
}
