using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WoodULike.Models;

namespace WoodULike.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("ApplicationUsersContext")
        {
        }

        public DbSet<WoodProject> WoodProjects { get; set; }
    }
}