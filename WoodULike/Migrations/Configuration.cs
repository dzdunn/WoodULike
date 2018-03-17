namespace WoodULike.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WoodULike.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WoodULike.DAL.WoodULikeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WoodULike.DAL.WoodULikeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            //var dupImageFiles = context.WoodProjects.Where(x => x.ImageFiles.Count == 2).SelectMany(x => x.ImageFiles);
            //var imageDups = context.ImageFiles.Where(x => x.Directory.Equals("Content\\Wood_Project_Images\\multiboatTHREE.jpg"));

            //foreach (ImageFile image in dupImageFiles)
            //{
            //    if (image)
            //}

            //foreach (WoodProject w in noImageFiles)
            //{


            //    var image2 = new ImageFile();
            //    image2.Directory = "Content\\Wood_Project_Images\\multiboatTHREE.jpg";
            //    image2.WoodProject = w;
            //    context.ImageFiles.Add(image2);

            //}


            var fileOnlyWoodProjects = context.WoodProjects.Where(x => x.ImageURL.StartsWith("Content"));

            foreach(WoodProject w in fileOnlyWoodProjects)
            {
                var image = new ImageFile();
                image.Directory = w.ImageURL;
                image.WoodProject = w;
                context.ImageFiles.AddOrUpdate(image);
            }
            context.SaveChanges();
        }
    }
}
