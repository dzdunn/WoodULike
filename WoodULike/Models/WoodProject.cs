using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodULike.Models
{
    public class WoodProject
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "WoodULike Project")]
        public string ProjectTitle { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; }

        [StringLength(300, MinimumLength = 5)]
        public string Description { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Project Type")]
        public string ProjectType { get; set; }
        

    }

    public class WoodProjectDBContext: DbContext
    {
        public DbSet<WoodProject> WoodProjects { get; set; }

    }
}