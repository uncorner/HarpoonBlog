using System.ComponentModel.DataAnnotations;

namespace Harpoon.Application.Backend.ViewModels
{
    public class NewChapterForm
    {
        private string tagName;

        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Вы забыли указать заголовок")]
        public string Title { get; set; }

        [Display(Name = "Метка раздела (будет отображаться в url)")]
        [Required(ErrorMessage = "Вы забыли указать метку раздела")]
        public string TagName
        {
            get { return tagName != null ? tagName.Trim() : string.Empty; }
            set { tagName = value; }
        }

        [Display(Name = "Публиковать")]
        public bool IsPublished { get; set; }

        [Display(Name = "Содержание")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        
        public NewChapterForm()
        {
            IsPublished = true;
        }

        public bool HasContent()
        {
            return !string.IsNullOrWhiteSpace(Content);
        }
        
        public virtual string GetFormTitle()
        {
            return "Создание нового раздела";
        }

        public virtual string GetSubmitText()
        {
            return "Создать";
        }
        
    }
}