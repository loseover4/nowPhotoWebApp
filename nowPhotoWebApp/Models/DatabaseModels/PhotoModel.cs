using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace nowPhotoWebApp.Models
{
    public class PhotoModel
    {
        [NotMapped]
        public const int IDNotExist = -1;

        [Key]
        public int PhotoId { get; set; }

        [Display(Name = "Decription")]
        [Required]
        public string Decription { get; set; }

        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [Display(Name = "Thumb Path")]
        public string ThumbPath { get; set; }
        
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; }

        [NotMapped]
        [Display(Name = "File Upload")]
        public HttpPostedFileBase UploadFile { get; set; }

        [NotMapped]
        public int NextPhotoId { get; set; }

        [NotMapped]
        public int PreviousPhotoId { get; set; }
    }
}