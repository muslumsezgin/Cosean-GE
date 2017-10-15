using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoseanGE.ImageProsesing
{
    public static class Rotate
    {

        public static float RIGHT = 90;
        public static float LEFT = 270;

        public static Bitmap build(Bitmap bmp, float angle)
        {
            Bitmap temp = new Bitmap(bmp.Height,bmp.Width);
            Color c;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    if (angle == 90) temp.SetPixel(temp.Width - j -1, i, c);
                    else if(angle == 270) temp.SetPixel(j, temp.Height - i -1, c);
                    else return bmp;
                }
            }
            return temp;
        }

        public static Bitmap build2(Bitmap bmp, float angle)
        {
            float alpha = angle;
            float gamma = 90;
            float beta = 180 - angle - gamma;

            float c1 = bmp.Height;
            float a1 = (float)(c1 * Math.Sin(alpha * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));
            float b1 = (float)(c1 * Math.Sin(beta * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));

            float c2 = bmp.Width;
            float a2 = (float)(c2 * Math.Sin(alpha * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));
            float b2 = (float)(c2 * Math.Sin(beta * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));
            Console.WriteLine("burda uzunluk: " + c1 + " genislik: " + c2);
            Console.WriteLine("burda a1: " + a1 + " a2: " + a2);
            Console.WriteLine("burda b1: " + b1 + " b2: " + b2);

            int width = Convert.ToInt32(b2 + a1);
            int height = Convert.ToInt32(b1 + a2);
            Console.WriteLine("yeni uzunluk: " + height + " genislik: " + width);
            if (width < 0)
                width *= -1;
            if (height < 0)
                height *= -1;
            Bitmap rotatedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(rotatedImage.Width / 2, rotatedImage.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle); //rotate
                g.TranslateTransform(-rotatedImage.Width / 2, -rotatedImage.Height / 2); //restore rotation point into the matrix
                g.DrawImage(bmp, new Point((width - bmp.Width) / 2, (height - bmp.Height) / 2)); //draw the image on the new bitmap
            }
            return rotatedImage;
        }

    }
}
