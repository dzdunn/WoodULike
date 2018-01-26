﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WoodULike.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public ICollection<WoodProject> WoodProjects { get; set; }

        public ICollection<WoodProject> WoodProjects { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public virtual ICollection<WoodProject> WoodProjects { get; set; }

        public ApplicationDbContext()
            : base("ApplicationUsersContext")
        {
        }

        public DbSet<WoodProject> WoodProjects{ get; set; }

    }

  


}