namespace WoodULike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserIdForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WoodProjects", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.WoodProjects", "UserId");
            AddForeignKey("dbo.WoodProjects", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WoodProjects", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WoodProjects", new[] { "UserId" });
            DropColumn("dbo.WoodProjects", "UserId");
        }
    }
}
