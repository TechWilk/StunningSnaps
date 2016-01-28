using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;


namespace CO5027.Models
{
    public class ImageProcessing
    {
        public static bool SaveWatermarkedImages(Image i, int photoId)
        {
            bool success = false;

            string filename = photoId.ToString();

            int height = i.Height;
            int width = i.Width;


            // resize img's
            int smallImgMaxWidth = 600;
            int smallImgMaxHeight = 600;
            int mediumImgMaxWidth = 1100;
            int mediumImgMaxHeight = 1100;
            int largeImgMaxWidth = 2000;
            int largeImgMaxHeight = 2000;

            System.Drawing.Image smallImg = i;
            System.Drawing.Image mediumImg = i;
            System.Drawing.Image largeImg = i;

            // prevent enlarging
            if (width > smallImgMaxHeight || height > smallImgMaxHeight)
            {
                smallImg = ImageManipulation.ResizeImage(i, smallImgMaxWidth, smallImgMaxHeight);
            }

            if (width > mediumImgMaxWidth || height > mediumImgMaxHeight)
            {
                mediumImg = ImageManipulation.ResizeImage(i, mediumImgMaxWidth, mediumImgMaxHeight);
            }

            if (width > largeImgMaxWidth || height > largeImgMaxHeight)
            {
                largeImg = ImageManipulation.ResizeImage(i, largeImgMaxWidth, largeImgMaxHeight);
            }

            // ~~~~ TODO: watermark images ~~~~

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/files/images/watermarked/");

            // resized images
            // large
            largeImg.Save(path + filename + "-lg.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //// large
            mediumImg.Save(path + filename + "-md.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //// small
            smallImg.Save(path + filename + "-sm.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);


            return success;
        }
    }
}