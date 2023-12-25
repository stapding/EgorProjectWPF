using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Rus
{
    class CaptchaGenerator
    {

        public static (Bitmap,string) Generate()
        {
            Bitmap bimap = new Bitmap(200, 100);
            string code = Generate(4);
            using (Graphics graphics = Graphics.FromImage(bimap))
            {
                graphics.Clear(GetRandomLightColor());
                DrawCaptchaCode(graphics, 200, 100, code);
                DrawDisorderLine(graphics, 200, 100);
            }

            return (bimap, code);
        }

        private static Color GetRandomLightColor()
        {
            int low = 180, high = 255;
            Random rand = new Random();
            int nRend = rand.Next(high) % (high - low) + low;
            int nGreen = rand.Next(high) % (high - low) + low;
            int nBlue = rand.Next(high) % (high - low) + low;

            return Color.FromArgb(nRend, nGreen, nBlue);
        }

        private static int GetFontSize(int imageWidth, int captchCodeCount)
        {
            var averageSize = imageWidth / captchCodeCount;
            return Convert.ToInt32(averageSize);
        }

        private static Color GetRandomDeepColor()
        {
            Random rand = new Random();
            int redlow = 160, greenLow = 100, blueLow = 160;
            return Color.FromArgb(rand.Next(redlow), rand.Next(greenLow), rand.Next(blueLow));
        }

        public static void DrawDisorderLine(Graphics graph, int width, int height)
        {
            Random rand = new Random();
            Pen linePen = new Pen(new SolidBrush(Color.Black), 3);
            for (int i = 0; i < rand.Next(3, 5); i++)
            {
                linePen.Color = GetRandomDeepColor();
                
                Point startPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                Point endPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                graph.DrawLine(linePen, startPoint, endPoint);
            }
        }

        private static void DrawCaptchaCode(Graphics graph, int width, int height, string captchaCode)
        {
            Random rand = new Random();
            SolidBrush fontBrush = new SolidBrush(Color.Black);
            int fontSize = GetFontSize(width, captchaCode.Length);
            Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            for (int i = 0; i < captchaCode.Length; i++)
            {
                fontBrush.Color = GetRandomDeepColor();

                int shiftPx = fontSize / 6;

                float x = i * fontSize + rand.Next(-shiftPx, shiftPx) + rand.Next(-shiftPx, shiftPx);
                int maxY = height - fontSize;
                if (maxY < 0) maxY = 0;
                float y = rand.Next(0, maxY);

                graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
            }
        }

        public static string Generate(int length)
        {
            string Letters = "012346789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rand = new Random();
            int maxRand = Letters.Length - 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }

    }
}
