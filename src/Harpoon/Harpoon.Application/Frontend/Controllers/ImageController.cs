using System;
using System.Web;
using System.Web.Mvc;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Frontend.Controllers
{
    public class ImageController : ControllerBase
    {
        private const string UPLOADING_MESSAGE_OK = "Изображение было загружено успешно";
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IImageRepository imageRepository;

        public ImageController(IUnitOfWorkFactory unitOfWorkFactory, IImageRepository imageRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        public ActionResult Show(int? id)
        {
            var idValue = ConvertParameter("id", id);

            var image = imageRepository.FetchById(idValue);
            if (image == null)
            {
                throw new ContentNotFoundException("image", idValue);
            }

            return new FileContentResult(image.Data, image.ContentType);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor,
            string langCode)
        {
            Image image;
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                image = new Image(upload.InputStream, upload.ContentType);
                imageRepository.Add(image);

                unitOfWork.Commit();
            }

            if (image == null)
            {
                throw new NullReferenceException("Saving image can't be null");
            }
            if (Request.Url == null)
            {
                throw new NullReferenceException("Request.Url is null");
            }

            var imageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Image/Show?id=" + image.Id;

            // since it is an ajax request it requires this string
            var output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction("
                + CKEditorFuncNum + ", \"" + imageUrl + "\", \"" + UPLOADING_MESSAGE_OK
                + "\");</script></body></html>";

            return Content(output);
        }

    }
}
