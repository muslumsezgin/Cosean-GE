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
        public static Bitmap build(Bitmap bmp, int newWidth, int newHeight)
        {

            Bitmap newImage = new Bitmap(newWidth, newHeight);
            int w1 = bmp.Width;
            int h1 = bmp.Height;

            int x_ratio = (int)((w1 << 16) / newWidth) + 1;
            int y_ratio = (int)((h1 << 16) / newHeight) + 1;

            int x2, y2;
            for (int i = 0; i < newHeight; i++)
            {
                for (int j = 0; j < newWidth; j++)
                {
                    x2 = ((j * x_ratio) >> 16);
                    y2 = ((i * y_ratio) >> 16);
                    newImage.SetPixel(j, i, bmp.GetPixel(x2, y2));
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
