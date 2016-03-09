using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027.user
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();

            // TODO: ensure user is authorised to download image

            string userId = User.Identity.GetUserId();  //TODO: fetch from auth providor

            OrderedProduct orderedProduct = db.OrderedProducts.Single(op => op.Order.CustomerId == userId);

            string databaseToken = "xxxxx"; // TODO: fetch token from database

            // check token
            string token = Request.QueryString["token"];

            if (token != databaseToken)
            {
                Response.Redirect("~/user/");
            }


            // fetch photo
            int id = 0;
            bool idIsInt = int.TryParse(Request.QueryString["id"], out id);

            if ((!idIsInt || id == 0))
            {
                Response.Redirect("~/user/");
            }

            // fetch from db
            Product photo = new Product();

            try
            {
                photo = db.Products.Single(p => p.Id == id);
            }
            catch
            {
                Response.Redirect("~/user/");
            }

            string extention = photo.Extension;
            int imageSize = (int)photo.SizeOfFile;
            string downloadName = photo.Name + extention;
            string fileLocation = MapPath("~/files/images/original/" + id.ToString() + extention);


            
            try
            {
                // initiate download
                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "image/jpeg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + downloadName + ";");
                Response.AddHeader("Content-Length", imageSize.ToString());
                Response.WriteFile(fileLocation);

                Response.Flush();
                Response.End();
            }
            catch
            {
            }
            
            // cleanup

        }
    }
}