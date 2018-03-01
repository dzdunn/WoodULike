using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WoodULike.Models;

namespace WoodULike.ViewModels
{
    public class WoodProjectsWithUsername
    {
        public WoodProject WoodProject { get; set; }
        public string User { get; set; }
    }
}