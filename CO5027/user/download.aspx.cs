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

            int userId = 1;  //TODO: fetch from auth providor

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
            int size = 0;

            bool idIsInt = int.TryParse(Request.QueryString["id"], out id);
            bool sizeIdIsInt = int.TryParse(Request.QueryString["size"], out size);

            if ((!idIsInt || id == 0) && (!sizeIdIsInt || size == 0))
            {
                Response.Redirect("~/user/");
            }

            // fetch from db
            Image image = new Image();

            try
            {
                image = db.Images.Single(i => i.ProductId == id && i.SizeId == size);
            }
            catch
            {
                Response.Redirect("~/user/");
            }

            string extention = ".jpg";  // TODO: fetch extention from db
            int imageSize = (int)image.SizeOfFile;
            string downloadName = image.Product.Name + extention;
            string fileLocation = MapPath("~/files/images/original/" + id.ToString() + extention);


            
            try
            {
                // initiate download
                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "image/jpeg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + downloadName + ";");
                //Response.AddHeader("Content-Length", imageSize.ToString());
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