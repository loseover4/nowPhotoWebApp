using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nowPhotoWebApp.Models.ViewModels
{
    public class StreamPhotoModel
    {
        public StreamModel StreamModel { get; set; }
        public List<PhotoModel> PhotoModelList { get; set; }
    }
}