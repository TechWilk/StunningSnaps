using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;


namespace CO5027.Models
{
    public class ImageProcessing
    {
        public bool SaveWatermarkedImages(System.Drawing.Image img, int photoId)
        {
            bool success = false;

            string filename = photoId.ToString();

            int height = img.Height;
            int width = img.Width;

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/files/images/watermarked/");

            DatabaseCO5027Entities db = new DatabaseCO5027Entities();

            img = AddWatermark(img);

            var sizes = db.Sizes.Where(s => s.Archived == false).ToList();

            foreach (var size in sizes)
            {
                int maxWidth = size.MaxWidth;
                int maxHeight = size.MaxHeight;

                var resizedImage = img;

                // prevents enlarging
                if (width > maxWidth || height > maxHeight)
                {
                    resizedImage = ImageManipulation.ResizeImage(img, maxWidth, maxHeight);
                }

                int newHeight = resizedImage.Height;
                int newWidth = resizedImage.Width;

                string filePath = path + filename + "-" + size.Id + ".jpg";
                resizedImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                var fileSize = new System.IO.FileInfo(filePath).Length;

                var imageInDb = size.Images.FirstOrDefault(i => i.ProductId == photoId);
                if (imageInDb != null)
                {
                    imageInDb.Height = newHeight;
                    imageInDb.Width = newWidth;
                    imageInDb.SizeOfFile = (int)fileSize;
                    db.SaveChanges();
                }
                else
                {
                    imageInDb = new CO5027.Image();
                    imageInDb.Height = newHeight;
                    imageInDb.Width = newWidth;
                    imageInDb.ProductId = photoId;
                    imageInDb.SizeId = size.Id;
                    imageInDb.SizeOfFile = (int)fileSize;
                    db.Images.Add(imageInDb);
                    db.SaveChanges();
                }

            }
            success = true;

            return success;
        }

        public static System.Drawing.Image AddWatermark(System.Drawing.Image img)
        {
            int width = img.Width;
            int height = img.Height;

            int fontSize = width / 5;

            string text = "StunningSnaps";
            var g = Graphics.FromImage(img);
            var brush = new SolidBrush(Color.FromArgb(70, 255, 255, 255));
            var stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;

            // code based upon 
            // todo: reference http://stackoverflow.com/questions/9747559/c-sharp-adding-string-to-image-using-the-max-font-size
            while (true)
            {
                var testFont = new Font(FontFamily.GenericSansSerif, fontSize);
                var size = g.MeasureString(text, testFont);

                if (size.Width < width)
                {
                    break;
                }
                else
                {
                    fontSize -= 5;
                }

                testFont.Dispose();
            }
            // end of code based upon

            var font = new Font(FontFamily.GenericSansSerif, fontSize);

            g.DrawString(text, font, brush, (width / 2), (height / 2), stringFormat);

            g.Dispose();
            return img;
        }
    }
}