using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nowPhotoWebApp.Models
{
    public class PhotoCommentModel
    {
        [Key]
        public int CommentId{ get; set; }

        [Display(Name = "Author Id")]
        [Required]
        public string AuthorId { get; set; }

        [Display(Name = "Author FirstName")]
        public string AuthorFirstName { get; set; }

        [Display(Name = "Author LastName")]
        public string AuthorLastName { get; set; }

        [Display(Name = "Photo Id")]
        [Required]
        public int PhotoId { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }

        [Display(Name = "Content")]
        [Required]
        public string Content { get; set; }
    }
}