using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoseanGE.ImageProsesing
{
    class Scala
    {

        public static Bitmap build(Bitmap bmp, double maxWidth, double maxHeight)
        {
            var ratioX = (double)maxWidth / bmp.Width;
            var ratioY = (double)maxHeight / bmp.Height;
            //var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(bmp.Width * ratioX);
            var newHeight = (int)(bmp.Height * ratioY);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);

            new Bitmap(bmp,1500,1000);

            return newImage;
        }

        public void cvb(Bitmap bmp,double maxWidth, double maxHeight)
        {
            var ratioX = (double)maxWidth / bmp.Width;
            var ratioY = (double)maxHeight / bmp.Height;
            //var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(bmp.Width * ratioX);
            var newHeight = (int)(bmp.Height * ratioY);

            Color c;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    SolidBrush color = new SolidBrush(c);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.FillRectangle(color, i, j, newWidth, newHeight);
                    }
                }
            }
        }

        public static Bitmap Zoom(Image img, Double size)
        {
            return new Bitmap(img, Convert.ToInt32(img.Width * size), Convert.ToInt32(img.Height * size));
        }

    }
}
