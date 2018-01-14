namespace WoodULike.Migrations.WoodProjectDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WoodProjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectTitle = c.String(maxLength: 50),
                        ImageURL = c.String(),
                        Description = c.String(maxLength: 300),
                        PublishDate = c.DateTime(nullable: false),
                        ProjectType = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WoodProjects");
        }
    }
}
