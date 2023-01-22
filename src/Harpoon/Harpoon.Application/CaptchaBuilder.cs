using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Harpoon.Application
{
    /// <summary>
    /// Based on http://www.stefanprodan.eu/2012/01/user-friendly-captcha-for-asp-net-mvc/
    /// </summary>
    public static class CaptchaBuilder
    {

        public static Bitmap Build(string captchaValue, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var bmp = new Bitmap(130, 30);

            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question
                gfx.DrawString(captchaValue, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                return bmp;
            }
        }

    }
}
