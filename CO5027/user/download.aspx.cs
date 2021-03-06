﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CO5027.Models;

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
            int remainingDownloads = 0;

            try
            {
                var orderedProducts = db.OrderedProducts.Where(op => op.Order.CustomerId == userId && op.ProductId == id);
                bool downloadAllowed = false;
                foreach (OrderedProduct item in orderedProducts)
                {
                    if ((item.DownloadCount < item.DownloadsAllowed))
                    {
                        downloadAllowed = true;
                        orderedProduct = item;
                        remainingDownloads += (item.DownloadsAllowed - item.DownloadCount);
                    }
                }
                if (!downloadAllowed)
                {
                    litMessage.Text = "<p>You are not permitted to download this photo. If you think this message is in error, please <a href=\"" + ResolveUrl("~/contact.aspx") + "\">contact us.</a></p>";
                    return;
                }
            }
            catch
            {
                Response.Redirect("~/user/");
            }

            // fetch from db
            Product photo = orderedProduct.Product;

            string extention = photo.Extension;
            int imageSize = (int)photo.SizeOfFile;
            string downloadName = photo.Name + extention;
            string fileLocation = MapPath("~/files/images/original/" + id.ToString() + extention);

            // count download
            orderedProduct.DownloadCount += 1;
            remainingDownloads -= 1;
            db.SaveChanges();

            sendEmailToCustomer(orderedProduct, remainingDownloads);

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
        }

        protected void sendEmailToCustomer(OrderedProduct orderedProduct, int remainingDownloads)
        {
            string userId = User.Identity.GetUserId();

            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            UserDetail customer = db.UserDetails.Single(u => u.UserId == userId);
            db.Dispose();

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

            string emailBody = customer.FirstName + "," + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Just a quick reminder of the remaining download allowance for your photo: " + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "----------" + Environment.NewLine;
            emailBody += orderedProduct.Product.Name + Environment.NewLine;
            emailBody += "Remaining available downloads: " + remainingDownloads + Environment.NewLine;
            emailBody += "----------" + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Need to download this photo again? " + Environment.NewLine;
            emailBody += baseUrl + ResolveUrl("~/user/download.aspx?id=" + orderedProduct.ProductId) + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "If you have trouble downloading, please contact us." + Environment.NewLine;
            emailBody += baseUrl + ResolveUrl("~/contact.aspx") + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Message sent though StunningSnaps website";

            string subject = "Downloaded: " + orderedProduct.Product.Name + " from StunningSnaps";

            Email.sendEmail(customer.Email, "stunningsnaps@wilk.tech", subject, emailBody);

        }
    }
}