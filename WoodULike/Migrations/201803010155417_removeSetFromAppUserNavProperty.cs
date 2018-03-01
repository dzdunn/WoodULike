namespace WoodULike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeSetFromAppUserNavProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WoodProjects", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WoodProjects", new[] { "UserId" });
            AddColumn("dbo.WoodProjects", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.WoodProjects", "UserId", c => c.String());
            CreateIndex("dbo.WoodProjects", "ApplicationUser_Id");
            AddForeignKey("dbo.WoodProjects", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WoodProjects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.WoodProjects", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.WoodProjects", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.WoodProjects", "ApplicationUser_Id");
            CreateIndex("dbo.WoodProjects", "UserId");
            AddForeignKey("dbo.WoodProjects", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
