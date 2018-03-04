using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WoodULike.Models;

namespace WoodULike.ViewModels
{
    public class WoodProjectDetailsViewModel
    {
        public WoodProject WoodProject { get; set; }
        public ICollection<ImageFile> ImageFiles { get; set; }
    }
}