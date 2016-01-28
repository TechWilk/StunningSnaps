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

            if (uploadImage())
            {
                txtName.Text = "";
                txtDescription.Text = "";
                //Response.Redirect("~/");
            }
        }

        private bool uploadImage()
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
                    //db_1417800 db = new db_1417800();


                    // save image to disk

                    string path = Server.MapPath("~/files/images/original/");

                    // resize img's - prevent enlargening
                    int smallImgMaxWidth = 600;
                    int smallImgMaxHeight = 600;
                    int mediumImgMaxWidth = 1100;
                    int mediumImgMaxHeight = 1100;
                    int largeImgMaxWidth = 2000;
                    int largeImgMaxHeight = 2000;

                    System.Drawing.Image smallImg = img;
                    System.Drawing.Image mediumImg = img;
                    System.Drawing.Image largeImg = img;


                    if (width > smallImgMaxHeight || height > smallImgMaxHeight)
                    {
                        smallImg = ImageManipulation.ResizeImage(img, smallImgMaxWidth, smallImgMaxHeight);
                    }

                    if (width > mediumImgMaxWidth || height > mediumImgMaxHeight)
                    {
                        mediumImg = ImageManipulation.ResizeImage(img, mediumImgMaxWidth, mediumImgMaxHeight);
                    }

                    if (width > largeImgMaxWidth || height > largeImgMaxHeight)
                    {
                        largeImg = ImageManipulation.ResizeImage(img, largeImgMaxWidth, largeImgMaxHeight);
                    }

                    string filename = "1";  // TODO: grab record id from db

                    // ~~~~ TODO: watermark images ~~~~

                    // original
                    img.Save(Server.MapPath("~/files/images/original/" + filename + fileExtention));

                    // resized images
                    // large
                    largeImg.Save(Server.MapPath("~/files/images/watermarked/" + filename + "-lg.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

                    //// large
                    mediumImg.Save(Server.MapPath("~/files/images/watermarked/" + filename + "-md.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

                    //// small
                    smallImg.Save(Server.MapPath("~/files/images/watermarked/" + filename + "-sm.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);


                    success = true;
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