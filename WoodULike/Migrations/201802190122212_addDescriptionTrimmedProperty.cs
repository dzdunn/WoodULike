namespace WoodULike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDescriptionTrimmedProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WoodProjects", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WoodProjects", "Description", c => c.String(maxLength: 300));
        }
    }
}
