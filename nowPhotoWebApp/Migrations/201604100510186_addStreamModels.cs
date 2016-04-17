namespace nowPhotoWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStreamModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StreamPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreamId = c.Int(nullable: false),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StreamModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StreamUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreamId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StreamUsers");
            DropTable("dbo.StreamModels");
            DropTable("dbo.StreamPhotoes");
        }
    }
}
