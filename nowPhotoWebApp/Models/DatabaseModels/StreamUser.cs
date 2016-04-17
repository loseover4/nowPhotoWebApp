using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nowPhotoWebApp.Models
{
    public class StreamUser
    {
        public StreamUser()
        {

        }

        public StreamUser(int streamID, string username)
        {
            this.StreamId = streamID;
            this.UserName = username;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "SteamId")]
        [Required]
        public int StreamId { get; set; }

        [Display(Name = "UserName")]
        [Required]
        public string UserName { get; set; }
    }
}