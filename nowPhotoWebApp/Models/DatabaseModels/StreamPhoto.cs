using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nowPhotoWebApp.Models
{
    public class StreamPhoto
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "SteamId")]
        [Required]
        public int StreamId { get; set; }

        [Display(Name = "PhotoId")]
        [Required]
        public int PhotoId { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
    }
}