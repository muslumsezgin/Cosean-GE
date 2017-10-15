using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoseanGE.ImageProsesing
{
    class Histogram
    {


        public static Image build(Bitmap bmp)
        {

            int[] histR = new int[256];
            int[] histG = new int[256];
            int[] histB = new int[256];
            Color c;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);

                    histR[c.R] += 1;
                    histG[c.G] += 1;
                    histB[c.B] += 1;
                }
            }

            Bitmap image = new Bitmap(512, 256);
            SolidBrush trnsRedBrush = new SolidBrush(Color.FromArgb(78, 255, 0, 0));
            SolidBrush trnsGreenBrush = new SolidBrush(Color.FromArgb(78, 0, 255, 0));
            SolidBrush trnsBlueBrush = new SolidBrush(Color.FromArgb(78, 0, 0, 255));

            for (int j = 0; j < 256; j++)
            {


                using (Graphics g = Graphics.FromImage(image))
                {
                    g.FillRectangle(trnsRedBrush, j * 2, 256 - (histR[j] / 256), 2, histR[j] / 256);
                }

                using (Graphics g = Graphics.FromImage(image))
                {
                    g.FillRectangle(trnsGreenBrush, j * 2, 256 - (histG[j] / 256), 2, histG[j] / 256);
                }

                using (Graphics g = Graphics.FromImage(image))
                {
                    g.FillRectangle(trnsBlueBrush, j * 2, 256 - (histB[j] / 256), 2, histB[j] / 256);
                }

            }


            return image;
        }

    }
}
