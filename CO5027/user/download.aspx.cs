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
            // fetch photo id
            int id = 0;
            bool idIsInt = int.TryParse(Request.QueryString["id"], out id);

            if ((!idIsInt || id == 0))
            {
                Response.Redirect("~/user/");
            }

            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            OrderedProduct orderedProduct = new OrderedProduct();

            string userId = User.Identity.GetUserId();

            try
            {
                orderedProduct = db.OrderedProducts.Single(op => op.Order.CustomerId == userId && op.Product.Id == id);
            }
            catch
            {
                Response.Redirect("~/user/");
            }

            if (!(orderedProduct.DownloadCount > orderedProduct.DownloadsAllowed))
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

            // count download
            orderedProduct.DownloadCount += 1;
            db.SaveChanges();

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

            // TODO: cleanup after download

        }
    }
}