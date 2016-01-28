using CO5027.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027.user
{
    public partial class add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;

            if (uploadImage(txtName.Text, txtDescription.Text))
            {
                txtName.Text = "";
                txtDescription.Text = "";
                //Response.Redirect("~/");
            }
        }

        private bool uploadImage(string name, string description)
        {
            bool success = false;
            string fileExtention = System.IO.Path.GetExtension(fUplPictureUpload.FileName).ToLower();
            if (fileExtention == ".jpeg" || fileExtention == ".jpg" || fileExtention == ".gif" || fileExtention == ".png" || fileExtention == ".tif" || fileExtention == ".tiff")
            {
                try
                {
                    // check image is readable & determine dimentions
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fUplPictureUpload.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;

                    // TODO: save record to db
                    DatabaseCO5027Entities db = new DatabaseCO5027Entities();
                    Product product = new Product();

                    product.Archived = false;
                    product.Name = name;
                    product.Description = description;
                    product.InitialHeight = height;
                    product.InitialWidth = width;

                    db.Products.Add(product);
                    db.SaveChanges();

                    string filename = product.Id.ToString();

                    // save original image to disk

                    img.Save(Server.MapPath("~/files/images/original/" + filename + fileExtention));

                    // save watermarked images to disk

                    if (ImageProcessing.SaveWatermarkedImages(img, product.Id))
                    {
                        success = true;
                        return success;
                    }
                    else
                    {
                        success = false;
                        litFeedback.Text = "Unable to process image, please go to <a href='manage.aspx'>Admin Panel</a> and try again.";
                        return success;

                    }
                }
                catch
                {
                    success = false;
                    litFeedback.Text = "Image not readable";
                    return success;
                }
            }
            else
            {
                success = false;
                litFeedback.Text = "Images of " + fileExtention + " are not accepted. Please upload a JPEG, PNG, GIF or TIFF.";
                return success;
            }
            return success;
        }
    }
}