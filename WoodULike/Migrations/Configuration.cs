namespace WoodULike.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WoodULike.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WoodULike.Models.WoodProjectDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WoodULike.Models.WoodProjectDBContext";
        }

        protected override void Seed(WoodULike.Models.WoodProjectDBContext context)
        {
            context.WoodProjects.AddOrUpdate(c => c.ProjectTitle,
                new WoodProject
                {
                    ProjectTitle = "Planter",
                    ImageURL = "https://i.pinimg.com/originals/41/52/75/415275890ca2c956f54a5032dd6e651f.jpg",
                    ProjectType = "Garden",
                    PublishDate = DateTime.Parse("12-11-2017"),
                    Description = "This is a garden planter like a boat"

            });

        }
    }
}
