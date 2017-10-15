using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoseanGE.ImageProsesing
{
    public static class Mirror
    {

        public static Bitmap build(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;


            Bitmap mimg = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int lx = 0, rx = width - 1; lx < width; lx++, rx--)
                {

                    Color p = bmp.GetPixel(rx, y);
                    mimg.SetPixel(lx, y, p);

                }
            }

            return mimg;
        }

    }
}
