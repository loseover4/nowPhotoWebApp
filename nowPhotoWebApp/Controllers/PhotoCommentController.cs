using nowPhotoWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace nowPhotoWebApp.Controllers
{
    public class PhotoCommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: Photo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _NewPhotoComment([Bind(Include = "Content,AuthorId,PhotoId")] PhotoCommentModel photoCommentModel)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated && photoCommentModel != null)
                {
                    photoCommentModel.AuthorId = User.Identity.Name;
                    photoCommentModel.CreatedOn = DateTime.Now;
                    photoCommentModel.ModifiedOn = DateTime.Now;

                    db.PhotoComments.Add(photoCommentModel);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Details", "Photo", new { id = 10 });
        }
    }
}