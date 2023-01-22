using System.Web.Mvc;
using Harpoon.Application.Backend.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Backend.Controllers
{
    public class ProfileController : ControllerBase
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IPersonalSettingRepository personalSettingRepository;

        public ProfileController(IUnitOfWorkFactory unitOfWorkFactory, IPersonalSettingRepository personalSettingRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.personalSettingRepository = personalSettingRepository;
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var setting = personalSettingRepository.Fetch();
            if (setting == null)
            {
                throw new ContentNotFoundException("setting is not found");
            }

            var form = new EditProfileForm
                           {
                               Title = setting.Title,
                               Subject = setting.Subject,
                               FooterText = setting.FooterText,
                               Name = setting.Name,
                               Email = setting.Email
                           };

            return View(form);
        }

        [HttpPost]
        public ActionResult Edit(EditProfileForm form)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = unitOfWorkFactory.Create())
                {
                    var setting = personalSettingRepository.Fetch();
                    setting.SetTitle(form.Title);
                    setting.SetName(form.Name);
                    setting.SetFooterText(form.FooterText);
                    setting.SetEmail(form.Email);
                    setting.Subject = form.Subject;

                    unitOfWork.Commit();
                }
                
                AddFlashMessage("Данные были успешно сохранены");
                
                return RedirectToAction("Edit");
            }

            return View(form);
        }


    }
}
