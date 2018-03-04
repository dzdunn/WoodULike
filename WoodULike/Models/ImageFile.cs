using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WoodULike.Models
{
    public class ImageFile
    {

        public int Id { get; set; }

        public string Directory { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        public virtual WoodProject WoodProject { get; set; }

        public string getCorrectUrl(string imageUrl)
        {
            return (imageUrl.StartsWith("Content")) ? ("/" + imageUrl.Replace('\\', '/')) : imageUrl;
        }
    }
}