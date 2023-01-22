using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Harpoon.Application.Backend.ViewModels
{
    public class EditProfileForm
    {
        [DisplayName("Заголовок блога")]
        [Required(ErrorMessage = "Вы забыли указать заголовок")]
        [StringLength(100, ErrorMessage = "Слишком длинный заголовок")]
        public string Title { get; set; }

        [DisplayName("Тематика или краткое описание блога")]
        [StringLength(200, ErrorMessage = "Слишком длинное описание")]
        public string Subject { get; set; }

        [DisplayName("Нижняя подпись")]
        [Required(ErrorMessage = "Вы забыли указать текст подписи")]
        [StringLength(50, ErrorMessage = "Слишком длинный текст")]
        public string FooterText { get; set; }

        [DisplayName("Ваше имя (будет использоваться в комментариях)")]
        [Required(ErrorMessage = "Вы забыли указать имя")]
        [StringLength(50, ErrorMessage = "Слишком длинное имя")]
        public string Name { get; set; }

        [DisplayName("Электронная почта")]
        [Required(ErrorMessage = "Вы забыли указать e-mail")]
        [StringLength(50, ErrorMessage = "Слишком длинный e-mail")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
           ErrorMessage = "Введите корректный адрес электронной почты")]
        public string Email { get; set; }

    }
}