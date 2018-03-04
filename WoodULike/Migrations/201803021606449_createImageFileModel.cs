namespace WoodULike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createImageFileModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Directory = c.String(),
                        WoodProject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WoodProjects", t => t.WoodProject_Id)
                .Index(t => t.WoodProject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageFiles", "WoodProject_Id", "dbo.WoodProjects");
            DropIndex("dbo.ImageFiles", new[] { "WoodProject_Id" });
            DropTable("dbo.ImageFiles");
        }
    }
}
