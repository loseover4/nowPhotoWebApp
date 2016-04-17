using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using nowPhotoWebApp.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace nowPhotoWebApp.Controllers
{
    public class PhotoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Photo
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.Name;
                Console.WriteLine("username: "+ username);
                List<PhotoModel> photoList = db.Photos.Where<PhotoModel>(model => model.UserName == username)
                                                        .OrderBy(model => model.CreatedOn).ToList();                
                return View(photoList);
            }
            return View();
        }

        // GET: Photo/Details/{id}
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PhotoDetailModel detailModel = new PhotoDetailModel();

                // Get Photo by ID
                PhotoModel photoModel = db.Photos.Find(id);

                if (photoModel == null)
                {
                    return HttpNotFound();
                }

                // Get Comment List
                List<PhotoCommentModel> photoCommentList = db.PhotoComments
                    .Where<PhotoCommentModel>(commentModel =>commentModel.AuthorId == username && commentModel.PhotoId == id).ToList();

                // Get Next Photo Id
                PhotoModel nextPhoto = db.Photos
                   .Where<PhotoModel>(model => model.UserName == username && model.CreatedOn > photoModel.CreatedOn)
                   .OrderBy(model => model.CreatedOn).ToList().FirstOrDefault<PhotoModel>();
                photoModel.NextPhotoId = nextPhoto != null ? nextPhoto.PhotoId : PhotoModel.IDNotExist;

                // Get Previous Id
                PhotoModel previousPhoto = db.Photos
                   .Where<PhotoModel>(model => model.UserName == username && model.CreatedOn < photoModel.CreatedOn)
                   .OrderByDescending(model => model.CreatedOn).ToList().FirstOrDefault<PhotoModel>();
                photoModel.PreviousPhotoId = previousPhoto != null ? previousPhoto.PhotoId : PhotoModel.IDNotExist;

                // Put photo and comments into Detail Model
                detailModel.Photo = photoModel;

                if(photoCommentList == null)
                {
                    photoCommentList = new List<PhotoCommentModel>();
                }

                detailModel.Comments = photoCommentList;

                return View(detailModel);
            }
            return RedirectToAction("Index", "Home");
        }

        //// POST: Photo/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Details([Bind(Include = "Content")] PhotoCommentModel photoCommentModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Request.IsAuthenticated && this.PhotoId != PhotoController.InvalidPhotoId)
        //        {
        //            photoCommentModel.AuthorId = User.Identity.Name;
        //            photoCommentModel.PhotoId = this.PhotoId;                    
        //            photoCommentModel.CreatedOn = DateTime.Now;

        //            db.PhotoComments.Add(photoCommentModel);
        //            db.SaveChanges();
        //        }
        //        return Details(this.PhotoId);
        //    }

        //    return RedirectToAction("Index");
        //}

        // GET: Photo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PhotoId,Decription,ImagePath,ThumbPath,CreatedOn,UploadFile")] PhotoModel photoModel)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    photoModel.UserName = User.Identity.Name;

                    // Upload to Azure Storage
                    BlobOperation blobOperation = new BlobOperation();
                    CloudBlockBlob blob = await blobOperation.UploadBlob(photoModel.UserName, photoModel.UploadFile, isPublic: true /* set to false later*/);

                    photoModel.ImagePath = blob.Uri.AbsoluteUri;
                    photoModel.CreatedOn = DateTime.Now;
                    db.Photos.Add(photoModel);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(photoModel);
        }

        // GET: Photo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoModel photoModel = db.Photos.Find(id);
            if (photoModel == null)
            {
                return HttpNotFound();
            }
            return View(photoModel);
        }

        // POST: Photo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoId,Decription,ImagePath,ThumbPath,CreatedOn")] PhotoModel photoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photoModel);
        }

        // GET: Photo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoModel photoModel = db.Photos.Find(id);
            if (photoModel == null)
            {
                return HttpNotFound();
            }
            return View(photoModel);
        }

        // POST: Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhotoModel photoModel = db.Photos.Find(id);
            db.Photos.Remove(photoModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Photo/Show/5
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoModel photoModel = db.Photos.Find(id);
            if (photoModel == null)
            {
                return HttpNotFound();
            }
            return View(photoModel);
        }
    }
}
