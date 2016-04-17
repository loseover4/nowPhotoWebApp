using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace nowPhotoWebApp.Models
{
    public class StreamModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Modified On")]
        [Required]
        public DateTime ModifiedOn { get; set; }

        [NotMapped]
        public string ImagePath { get; set; }
    }
}