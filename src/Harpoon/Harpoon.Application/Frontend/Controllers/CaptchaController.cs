using System;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace Harpoon.Application.Frontend.Controllers
{
    public class CaptchaController : Controller
    {

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question
            int a = rand.Next(10, 19);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);
            //store answer
            Session["captcha" + prefix] = a + b;

            //image stream
            FileContentResult img = null;
            using (var mem = new MemoryStream())
            {
                using (var bmp = CaptchaBuilder.Build(captcha))
                {
                    //render as Jpeg
                    bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                    img = this.File(mem.GetBuffer(), "image/Jpeg");
                }
            }

            return img;
        }

    }
}
