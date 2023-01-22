using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Harpoon.Application.Backend.ViewModels
{
    public class NewArticleForm
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Вы забыли указать заголовок")]
        public string Title { get; set; }

        [Display(Name = "Публиковать")]
        public bool IsPublished { get; set; }

        [Display(Name = "Содержание")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Тэги")]
        public string TagLine { get; set; }

        public NewArticleForm()
        {
            IsPublished = true;
        }

        public bool HasContent()
        {
            return !string.IsNullOrWhiteSpace(Content);
        }
        
        public IEnumerable<string> GetTagNames()
        {
            return CreateConverter().ToTags(TagLine);
        }

        public virtual string GetFormTitle()
        {
            return "Создание новой заметки";
        }

        public virtual string GetSubmitText()
        {
            return "Создать";
        }

        protected TagLineConverter CreateConverter()
        {
            return new TagLineConverter(';');
        }

    }
}