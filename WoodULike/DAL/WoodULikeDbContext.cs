using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WoodULike.Models;

namespace WoodULike.DAL
{
    public class WoodULikeDbContext : IdentityDbContext<ApplicationUser>
    {

        public WoodULikeDbContext()
            : base("ApplicationUsersContext")
        {
        }

        public DbSet<WoodProject> WoodProjects { get; set; }

       
    }
}