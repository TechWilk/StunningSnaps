using CO5027.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027.admin
{
    public partial class add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idString = Request.QueryString["id"];
                int id = 0;
                if (int.TryParse(idString, out id))
                {
                    try
                    {
                        DatabaseCO5027Entities db = new DatabaseCO5027Entities();
                        Product product = db.Products.Single(p => p.Id == id);

                        txtName.Text = product.Name;
                        txtDescription.Text = product.Description;
                        txtPrice.Text = ((decimal)product.Price).ToString("0.00");

                        btnUpload.Text = "Update";
                    }
                    catch
                    {
                        litFeedback.Text = "Error loading photo";
                        pnlInputFields.Visible = false;
                        btnUpload.Visible = false;

                    }
                    pnlUploadControl.Visible = false;
                }
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            decimal price;
            if (!Decimal.TryParse(txtPrice.Text, out price))
            {
                litFeedback.Text = "Please enter a price in the formal: 5.20";
                return;
            }

            string idString = Request.QueryString["id"];
            int id = 0;
            if (int.TryParse(idString, out id))
            {
                DatabaseCO5027Entities db = new DatabaseCO5027Entities();
                var product = db.Products.Single(p => p.Id == id);
                product.Name = txtName.Text;
                product.Description = txtDescription.Text;
                product.Price = price;

                db.SaveChanges();
                Response.Redirect("~/admin");
            }
            else
            {
                if (uploadImage(txtName.Text, txtDescription.Text, price))
                {
                    txtName.Text = "";
                    txtDescription.Text = "";
                    Response.Redirect("~/admin");
                }
            }
        }

        private bool uploadImage(string name, string description, decimal price)
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

                    DatabaseCO5027Entities db = new DatabaseCO5027Entities();
                    Product product = new Product();

                    product.Archived = false;
                    product.Name = name;
                    product.Description = description;
                    product.Price = price;
                    product.InitialHeight = height;
                    product.InitialWidth = width;
                    product.Extension = fileExtention;

                    db.Products.Add(product);
                    db.SaveChanges();

                    string filename = product.Id.ToString();

                    // save original image to disk
                    string filePath = Server.MapPath("~/files/images/original/" + filename + fileExtention);
                    img.Save(filePath);
                    product.SizeOfFile = (int)new System.IO.FileInfo(filePath).Length;
                    db.SaveChanges();

                    // save watermarked images to disk

                    ImageProcessing imageProcessor = new ImageProcessing();

                    if (imageProcessor.SaveWatermarkedImages(img, product.Id))
                    {
                        success = true;
                        return success;
                    }
                    else
                    {
                        success = false;
                        litFeedback.Text = "Unable to process image, please go to <a href='manage.aspx'>Admin Panel</a> and click 'Reprocess Images' to try again.";
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
        }
    }
}