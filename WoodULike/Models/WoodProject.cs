﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace WoodULike.Models
{
    public class WoodProject
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "WoodULike Project")]
        public string ProjectTitle { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; }

        [StringLength(300, MinimumLength = 5)]
        public string Description { get; set; }

        [ConfigurationPropertyAttribute("enableClientBasedCulture", DefaultValue = true)]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Project Type")]
        public string ProjectType { get; set; }

        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }


    
  
}