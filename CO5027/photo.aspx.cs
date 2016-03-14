using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027
{
    public partial class photo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idString = Request.QueryString["id"];

            int id = 0;

            try
            {
                id = int.Parse(idString);
            }
            catch
            {
                Response.Redirect("~/");
            }

            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            Product photo = new Product();

            try
            {
                photo = db.Products.Single(p => p.Id == id && p.Archived == false);
            }
            catch
            {
                Response.Redirect("~/");
            }

            string photoInfoFormatted = "<h3>" + Server.HtmlEncode(photo.Name) + "</h3>";
            photoInfoFormatted += "<p>(" + photo.InitialWidth + " x " + photo.InitialHeight + ")</p>";
            photoInfoFormatted += "<p>" + Server.HtmlEncode(photo.Description) + "</p>";

            litPhotoInfo.Text = photoInfoFormatted;

            var image = photo.Images.FirstOrDefault(p => p.SizeId == 2);

            imgPhoto.Src = "~/files/images/watermarked/" + id.ToString() + "-2.jpg";
            imgPhoto.Alt = Server.HtmlEncode(photo.Description);
            imgPhoto.Width = image.Width;
            imgPhoto.Height = image.Height;
        }

        protected void btnAddToBasket_Click(object sender, EventArgs e)
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();

            int productId = int.Parse(Request.QueryString["id"]);
            string userId = User.Identity.GetUserId();
            try
            {
                db.Baskets.Single(i => i.ProductId == productId && i.CustomerId == userId);
            }
            catch
            {
                if (User.Identity.IsAuthenticated)
                {
                    var basketEntry = new Basket();
                    basketEntry.CustomerId = userId;
                    basketEntry.ProductId = productId;
                    db.Baskets.Add(basketEntry);
                    db.SaveChangesAsync();
                    Response.Redirect("~/checkout.aspx");
                }
                else
                {
                    // redir to login
                    Session.Add("basketProductId", productId);
                    Response.Redirect("~/login.aspx");
                }
            }
        }
    }
}