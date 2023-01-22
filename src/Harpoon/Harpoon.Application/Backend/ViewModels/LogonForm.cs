using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Harpoon.Application.Backend.Authentication;

namespace Harpoon.Application.Backend.ViewModels
{
    public class LogonForm
    {
        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Вы забыли ввести пароль")]
        [StringLength(AuthHelper.PASSWORD_MAX_LENGHT, ErrorMessage = "Слишком длинный пароль")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
        
    }
}