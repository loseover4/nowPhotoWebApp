using nowPhotoWebApp.Models;
using nowPhotoWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace nowPhotoWebApp.Controllers
{
    public class StreamController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stream
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                // Get all streams that current user has access to
                string username = User.Identity.Name;
                List<StreamUser> StreamUserList = db.StreamUsers.Where<StreamUser>(model => model.UserName == username).ToList();

                List<StreamModel> StreamList = new List<StreamModel>();
                foreach (StreamUser streamUser in StreamUserList)
                {
                    StreamModel streamModel = db.Streams.Where<StreamModel>(model => model.Id == streamUser.StreamId).FirstOrDefault();
                    StreamPhoto streamPhoto = db.StreamPhotoes.Where<StreamPhoto>(model => model.StreamId == streamUser.StreamId)
                                                        .OrderBy(model => model.CreatedOn).FirstOrDefault();

                    if(streamPhoto != null)
                    {
                        PhotoModel photoModel = db.Photos.Where<PhotoModel>(model => model.PhotoId == streamPhoto.PhotoId).FirstOrDefault();

                        if(photoModel!= null)
                        {
                            streamModel.ImagePath = photoModel.ImagePath;
                        }
                    }
                    StreamList.Add(streamModel);
                }
                return View(StreamList);
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ModifiedOn")] StreamModel streamModel)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    // Create new item in StreamModel table
                    streamModel.ModifiedOn = DateTime.Now;
                    db.Streams.Add(streamModel);
                    db.SaveChanges();

                    // Associate Stream with user by creating new item in StreamUser table
                    StreamUser streamUser = new StreamUser(streamID: streamModel.Id, username: User.Identity.Name);
                    db.StreamUsers.Add(streamUser);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(streamModel);
        }

        public ActionResult Photoes(int id)
        {
            if (Request.IsAuthenticated)
            {
                // Get Stream Model
                StreamModel streamModel = db.Streams.Where(model => model.Id == id).First();

                // Get Photo List in this Stream
                List<StreamPhoto> streamPhotoList = db.StreamPhotoes.Where<StreamPhoto>(model => model.StreamId == id).ToList();
                List<PhotoModel> photoLists = new List<PhotoModel>();
                foreach (StreamPhoto streamPhoto in streamPhotoList)
                {
                    PhotoModel photoModel = db.Photos.Where<PhotoModel>(model => model.PhotoId == streamPhoto.PhotoId).First();
                    if (photoModel != null)
                    {
                        photoLists.Add(photoModel);
                    }
                }

                // Add data to ViewModel
                StreamPhotoModel streamPhotoModel = new StreamPhotoModel();
                streamPhotoModel.PhotoModelList = photoLists;
                streamPhotoModel.StreamModel = streamModel;

                return View(streamPhotoModel);
            }
            return View();
        }

        public ActionResult AddPhoto(int id)
        {
            if (Request.IsAuthenticated)
            {
                // Get Stream Model
                StreamModel streamModel = db.Streams.Where(model => model.Id == id).First();

                // Get all Photo NOT in this Stream
                List<StreamPhoto> photoInStream = db.StreamPhotoes.Where<StreamPhoto>(model => model.StreamId == id).ToList();

                List<int> photoIDInStream = (from x in photoInStream select x.PhotoId).ToList();

                List<PhotoModel> photoList = db.Photos
                    .Where<PhotoModel>(model => !photoIDInStream.Contains(model.PhotoId)
                                        && model.UserName == User.Identity.Name).ToList();

                // Add data to ViewModel
                StreamPhotoModel streamPhotoModel = new StreamPhotoModel();
                streamPhotoModel.PhotoModelList = photoList;
                streamPhotoModel.StreamModel = streamModel;

                return View(streamPhotoModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(StreamModel streamModel, [Bind(Include = "selectedPhotoIds")]  List<int> selectedPhotoIds)
        {
            if(selectedPhotoIds == null)
            {
                return View();
            }

            foreach(int id in selectedPhotoIds)
            {
                StreamPhoto streamPhoto = new StreamPhoto();
                streamPhoto.StreamId = streamModel.Id;
                streamPhoto.PhotoId = id;
                db.StreamPhotoes.Add(streamPhoto);
                db.SaveChanges();
            }

            return View();
        }

        public ActionResult AddUser(int id)
        {
            if (Request.IsAuthenticated)
            {
                // Get Stream Model
                StreamModel streamModel = db.Streams.Where(model => model.Id == id).First();

                return View(streamModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adduser(StreamModel streamModel, [Bind(Include = "username")] string username)
        {
            return View();
        }
    }
}
