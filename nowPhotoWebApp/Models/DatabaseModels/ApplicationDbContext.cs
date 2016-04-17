using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace nowPhotoWebApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<PhotoModel> Photos { get; set; }

        public DbSet<PhotoCommentModel> PhotoComments { get; set; }

        public DbSet<StreamModel> Streams { get; set; }

        public DbSet<StreamPhoto> StreamPhotoes { get; set; }

        public DbSet<StreamUser> StreamUsers { get; set; }
    }
}