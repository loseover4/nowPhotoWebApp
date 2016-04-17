namespace nowPhotoWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoCommentModels",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        AuthorId = c.String(nullable: false),
                        PhotoId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PhotoCommentModels");
        }
    }
}
