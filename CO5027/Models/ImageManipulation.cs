// ~~~~~~~~~~
// Muncey (2016)
// ~~~~~~~~~~

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace CO5027.Models
{
    public static class ImageManipulation
    {
        public static System.Drawing.Image SimpleResize(System.Drawing.Image i, int width, int height)
        {
            //create a bitmap image with the required width and height
            Bitmap resized = new Bitmap(width, height);

            //create an instance of the graphics class which we will use to do the resizing work
            Graphics g = Graphics.FromImage(resized);

            //create a rectangle where the upper left coordinates are 0 and 0, and its width and hight match the required size
            Rectangle NewSizeRectangle = new Rectangle(0, 0, width, height);

            //draw the original image i, into the new rectangle, starting at x=0 and y=0, drawing the full height and width of the initial image, using pixels as the measurement
            g.DrawImage(i, NewSizeRectangle, 0, 0, i.Width, i.Height, GraphicsUnit.Pixel);

            //dispose of our graphics instance
            g.Dispose();

            //return the resized image
            return resized;
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image i, int MaxWidth, int MaxHeight)
        {
            //do some maths to determine the best way of resizing the image
            //e.g. if the max width and height are 100, a tall image will be 100 on the height, a wide one will be 100 on the width
            decimal imageRatio = (decimal)i.Width / (decimal)i.Height;
            decimal optimalRatio = (decimal)MaxWidth / (decimal)MaxHeight;
            int w, h;

            if (imageRatio < optimalRatio)
            {
                //resize on the height
                h = MaxHeight;
                w = i.Width * h / i.Height;
            }

            else
            {
                //resize on the width
                w = MaxWidth;
                h = i.Height * w / i.Width;
            }

            //as above
            Bitmap resized = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(resized);

            //settings for image quality (better qulity = slower process)
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //draw image as in simple example
            g.DrawImage(i, new Rectangle(0, 0, resized.Width, resized.Height), 0, 0, i.Width, i.Height, GraphicsUnit.Pixel);
            g.Dispose();
            return resized;
        }

    }
}