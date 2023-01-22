using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Harpoon.Application.ViewModels
{
    public abstract class CommentFormBase
    {
        public int ArticleId { get; private set; }
        
        [Display(Name = "Текст комментария")]
        [Required(ErrorMessage = "Вы забыли ввести текст комментария")]
        [StringLength(2000, ErrorMessage = "Слишком длинный комментарий")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string CommentContent { get; set; }

        protected void Init(int articleId)
        {
            ArticleId = articleId;
        }

    }
}
