using nowPhotoWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nowPhotoWebApp.UserControls
{
    public partial class ImageGrid : System.Web.UI.UserControl
    {
        public List<PhotoModel> Images;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Images != null)
            {
                List<string> imagePaths = new List<string>();
                foreach(PhotoModel photo in Images)
                {
                    imagePaths.Add(photo.ImagePath);
                }
                ImageGridListView.DataSource = imagePaths;
                ImageGridListView.DataBind();
            }
        }
    }
}