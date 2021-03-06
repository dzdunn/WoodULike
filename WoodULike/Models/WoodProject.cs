﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.IO;

namespace WoodULike.Models
{
    public class WoodProject
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "WoodULike Project")]
        public string ProjectTitle { get; set; }

       

        [StringLength(10000)]
        public string Description { get; set; }

        
        [Display(Name = "Description")]
        public string DescriptionTrimmed { get {
                if (this.Description.Length > 650)
                {
                    return Description.Substring(0, 650) + "...";
                } else { return Description; }
            }
        }

        [ConfigurationPropertyAttribute("enableClientBasedCulture", DefaultValue = true)]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Project Type")]
        public string ProjectType { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; } 

        [NotMapped]
        public HttpPostedFileBase[] Files { get; set; }

        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ImageFile> ImageFiles { get; set; }




        public string getUserName()
        {
            return this.ApplicationUser.UserName;
        }

        public string getCorrectUrl(string imageUrl)
        {
            return (imageUrl.StartsWith("Content")) ? ("/" + imageUrl.Replace('\\', '/')) : imageUrl;
        }

        

        public string[] ProjectTypes =
        {
            "Decor",
            "Cabinets",
            "Tables",
            "Book Cases",
            "Garden",
            "Carpentry",
            "Stationary",
            "Gadgets",
            "Furniture",
            "Other"
        };

    }


    
  
}