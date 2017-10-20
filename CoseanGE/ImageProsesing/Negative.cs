using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoseanGE.ImageProsesing
{
    public static class Negative
    {

        public static Bitmap build(Bitmap bmp)
        {
            Color c;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    c = Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B);
                    bmp.SetPixel(i, j, c);
                }
            }

            return bmp;
        }

    }
}
