using System.Web.Mvc;
using Harpoon.Application.Backend.Authentication;
using Harpoon.Application.Backend.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Backend.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthProvider authProvider;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IPersonalSettingRepository settingRepository;

        public AuthController(IAuthProvider authProvider, IUnitOfWorkFactory unitOfWorkFactory,
            IPersonalSettingRepository settingRepository)
        {
            this.authProvider = authProvider;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.settingRepository = settingRepository;
        }

        [HttpGet]
        public ActionResult Logon()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToRoute("AdminRoot");
            }

            return View(new LogonForm());
        }

        [HttpPost]
        public ActionResult Logon(LogonForm form, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var setting = settingRepository.Fetch();

                if (authProvider.TrySignIn(setting.PasswordHash, form.Password))
                {
                    if (CheckReturnUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToRoute("AdminRoot");
                }

                // if wrong pwd then pause
                System.Threading.Thread.CurrentThread.Join(1000);
                ModelState.AddModelError("", "Пароль указан неверно. Попробуйте еще раз.");
            }

            form.Password = "";
            return View(form);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Logoff()
        {
            authProvider.SignOut();
            
            return RedirectToAction("Logon");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordForm());
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordForm form)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = unitOfWorkFactory.Create())
                {
                    var setting = settingRepository.Fetch();
                    if (setting == null)
                    {
                        throw new ContentNotFoundException("setting is not found");
                    }
                    
                    if (authProvider.CheckPassword(setting.PasswordHash, form.Password))
                    {
                        var hash = authProvider.GetPasswordHash(form.NewPassword);
                        setting.SetPasswordHash(hash);

                        AddFlashMessage("Пароль был успешно изменен.");
                        AddFlashMessage("Новый пароль будет использоваться при следующем входе.");

                        // save
                        unitOfWork.Commit();
                        
                        return RedirectToAction("ChangePassword");
                    }

                    ModelState.AddModelError("", "Текущий пароль указан неверно. Попробуйте еще раз.");
                }
            }

            form.Clear();
            return View(form);
        }
        
        private bool CheckReturnUrl(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                   && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//")
                   && !returnUrl.StartsWith("/\\");
        }

    }
}