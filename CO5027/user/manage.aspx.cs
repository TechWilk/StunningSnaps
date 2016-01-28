using CO5027.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027.user
{
    public partial class manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReprocessImages_Click(object sender, EventArgs e)
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            foreach (Product p in db.Products)
            {
                int id = p.Id;
                string path = Server.MapPath("~/files/images/original/" + id.ToString() + ".jpg"); // TODO: fetch extention from database
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                ImageProcessing.SaveWatermarkedImages(img, id);
            }
        }
    }
}