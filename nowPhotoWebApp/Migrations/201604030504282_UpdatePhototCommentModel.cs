namespace nowPhotoWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhototCommentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoCommentModels", "AuthorFirstName", c => c.String());
            AddColumn("dbo.PhotoCommentModels", "AuthorLastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhotoCommentModels", "AuthorLastName");
            DropColumn("dbo.PhotoCommentModels", "AuthorFirstName");
        }
    }
}
