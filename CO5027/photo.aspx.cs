﻿using System;
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

            string photoInfoFormatted = "<h3>" + photo.Name + "</h3>";
            photoInfoFormatted += "<p>" + photo.Description + "</p>";

            // TODO: add price & options to buy

            var sizes = db.Sizes.Where(s => s.Archived == false).ToList();

            ddlSize.DataTextField = "Name";
            ddlSize.DataValueField = "Id";
            ddlSize.DataSource = sizes;
            ddlSize.DataBind();

            litPhotoInfo.Text = photoInfoFormatted;

            var image = photo.Images.FirstOrDefault(p => p.SizeId == 2);
            
            imgPhoto.ImageUrl = "~/files/images/watermarked/" + id.ToString() + "-2.jpg";
            imgPhoto.AlternateText = photo.Description;
            imgPhoto.Width = image.Width;
            // TODO: ask Andrew - this uses CSS, not HTML tags - imgPhoto.Height = image.Height;
        }

        protected void btnAddToBasket_Click(object sender, EventArgs e)
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();

            int productId = int.Parse(Request.QueryString["id"]);
            int qty = 1; //TODO: add option to UI

            if (true) // TODO: check if user logged in
            {
                var basketEntry = new Basket();
                basketEntry.CustomerId = 1; // TODO: fetch from identity
                basketEntry.ProductId = productId;
                basketEntry.Qty = qty;
                db.Baskets.Add(basketEntry);
                db.SaveChangesAsync();
                //Response.Redirect("~/Basket.aspx");
            }
            else
            {
                // redir to login
                Session.Add("basketProducteId", productId);
                Session.Add("basketQty", qty);
                // TODO: if not logged in, add to session & update db upon login
                Response.Redirect("~/login.aspx");
            }
        }
    }
}