using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Harpoon.Application.Backend.Authentication;

namespace Harpoon.Application.Backend.ViewModels
{
    public class ChangePasswordForm : LogonForm
    {
        [DisplayName("Текущий пароль")]
        public override string Password { get; set; }

        [DisplayName("Новый пароль")]
        [Required(ErrorMessage = "Вы забыли ввести новый пароль")]
        [StringLength(AuthHelper.PASSWORD_MAX_LENGHT, MinimumLength = AuthHelper.PASSWORD_MIN_LENGHT,
            ErrorMessage = "Пароль должен содержать от 7 до 20 символов")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Подтверждение пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        
        public void Clear()
        {
            Password = "";
            NewPassword = "";
            ConfirmNewPassword = "";
        }

    }
}