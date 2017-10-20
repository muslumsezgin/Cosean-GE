using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoseanGE.ImageProsesing
{
    static class RGBChannel
    {

        public static Bitmap build(Bitmap bmp, bool r, bool g, bool b)
        {
            if (!r && !g && !b)  return new Bitmap(bmp.Width,bmp.Height) ;

            Color c;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    int beta = Convert.ToInt32(r) + Convert.ToInt32(g) + Convert.ToInt32(b) > 1 ? 0 : r ? c.R : g ? c.G : c.B;
                    c = Color.FromArgb(c.A,r ? c.R : beta, g ? c.G : beta, b ? c.B : beta);
                    bmp.SetPixel(i, j, c);
                }
            }

            return bmp;
        }


    }
}
