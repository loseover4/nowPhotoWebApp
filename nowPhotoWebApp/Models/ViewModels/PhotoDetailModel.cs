using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nowPhotoWebApp.Models
{
    public class PhotoDetailModel
    {
        public PhotoModel Photo { get; set; }
        public List<PhotoCommentModel> Comments { get; set; }
    }
}