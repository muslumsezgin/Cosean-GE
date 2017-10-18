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

        public static Bitmap myScale(Bitmap bmp, double maxWidth, double maxHeight)
        {
            Bitmap newImage = new Bitmap((int)maxWidth, (int)maxHeight);
            var ratioX = (double)maxWidth / bmp.Width;
            var ratioY = (double)maxHeight / bmp.Height;
            Color[] bmpPixels = new Color[bmp.Width * bmp.Height];
            
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color colour = bmp.GetPixel(x, y);

                    bmpPixels[x + y * bmp.Width] = colour;
                }
            }
            
            for (int i = 0; i < newImage.Height; i++)
            {
                for (int j = 0; j < newImage.Width; j++)
                {
                    //newImage.SetPixel(j, i,bmpPixels[j+*]);
                }
            }

            return newImage;
        }

        public static Bitmap Zoom(Image img, Double size)
        {
            return new Bitmap(img, Convert.ToInt32(img.Width * size), Convert.ToInt32(img.Height * size));
        }

    }
}
