using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WoodULike.Models;

namespace WoodULike.ViewModels
{
    public class WoodProjectsWithUsername
    {
        public IEnumerable<WoodProject> WoodProjects { get; set; }
        public IEnumerable<ApplicationUser> User { get; set; }
    }
}