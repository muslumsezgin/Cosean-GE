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
        private static SolidBrush trnsRedBrush = new SolidBrush(Color.FromArgb(78, 255, 0, 0));
        private static SolidBrush trnsGreenBrush = new SolidBrush(Color.FromArgb(78, 0, 255, 0));
        private static SolidBrush trnsBlueBrush = new SolidBrush(Color.FromArgb(78, 0, 0, 255));

        public static Image build(Bitmap bmp)
        {
            int ratio = 1;
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

                    ratio = c.R != 0 && c.R != 255 && ratio < histR[c.R] ? histR[c.R] : ratio;
                    ratio = c.G != 0 && c.G != 255 && ratio < histG[c.G] ? histG[c.G] : ratio;
                    ratio = c.B != 0 && c.B != 255 && ratio < histB[c.B] ? histB[c.B] : ratio;
                   
                }
            }

            int divider = (ratio < 100000) ? 1 : 1000;
            ratio = (ratio < 100000) ? ratio : ratio / 1000;
           
            Bitmap image = new Bitmap(512, ratio);
            
            for (int j = 0; j < 256; j++)
            {
                using (Graphics g = Graphics.FromImage(image))
                {

                    g.FillRectangle(trnsRedBrush, j * 2, ratio - (histR[j] / divider), 2, histR[j] / divider);
                    g.FillRectangle(trnsGreenBrush, j * 2, ratio - (histG[j] / divider), 2, histG[j] / divider);
                    g.FillRectangle(trnsBlueBrush, j * 2, ratio - (histB[j] / divider), 2, histB[j] / divider);

                    //g.FillRectangle(trnsRedBrush, j * 2, ratio - (histR[j]), 2, histR[j]);
                    //g.FillRectangle(trnsGreenBrush, j * 2, ratio - (histG[j] ), 2, histG[j]);
                    //g.FillRectangle(trnsBlueBrush, j * 2, ratio - (histB[j] ), 2, histB[j] );
                }

                //using (Graphics g = Graphics.FromImage(image))
                //{
                //    g.FillRectangle(trnsGreenBrush, j * 2, ratio - (histG[j] / ratio), 2, histG[j] / ratio);
                //    g.FillRectangle(trnsGreenBrush, j * 2, ratio - (histG[j]), 2, histG[j]);
                //}

                //using (Graphics g = Graphics.FromImage(image))
                //{
                //    g.FillRectangle(trnsBlueBrush, j * 2, ratio - (histB[j] / ratio), 2, histB[j] / ratio);
                //    g.FillRectangle(trnsBlueBrush, j * 2, ratio - (histB[j]), 2, histB[j]);
                //}

            }


            return image;
        }

    }
}
