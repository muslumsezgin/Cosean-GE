using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoseanGE.ImageProsesing
{
    public static class BlackWhite
    {

        public static Bitmap build(Bitmap bmp)
        {
            Color c;
            for (int i = 0; i < bmp.Height - 1; i++)
            {
                for (int j = 0; j < bmp.Width - 1; j++)
                {
                    c = bmp.GetPixel(j, i);
                    int color = Convert.ToInt32(c.R * 0.2126) + Convert.ToInt32(c.G * 0.7152) + Convert.ToInt32(c.B * 0.0722);
                    bmp.SetPixel(j, i, Color.FromArgb(c.A,color,color,color));
                }
            }

            return bmp;

        }

    }
}
