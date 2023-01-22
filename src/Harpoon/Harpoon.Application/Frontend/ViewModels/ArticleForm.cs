using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Harpoon.Application.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Entities;
using System.Web.Mvc;

namespace Harpoon.Application.Frontend.ViewModels
{
    public class ArticleForm : CommentFormBase, ICommentListModel
    {
        public Article Article { get; private set; } 

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Вы забыли указать имя")]
        [StringLength(30, ErrorMessage = "Слишком длинное имя")]
        public string CommentName { get; set; }

        [Email(ErrorMessage = "Некорректный адрес эл. почты")]
        [StringLength(50, ErrorMessage = "Слишком длинный адрес эл. почты")]
        public string Email { get; set; }

        [Display(Name = "Введите проверочное значение")]
        [Required(ErrorMessage = "Вы забыли указать значение")]
        [StringLength(10, ErrorMessage = "Слишком длинная строка")]
        public string Captcha { get; set; }
        
        public string MasterName { get; private set; }

        public ArticleForm Init(Article article, string masterName)
        {
            ArgumentHelper.EnsureNotNull("article", article);
            ArgumentHelper.EnsureNotNullOrEmpty("masterName", masterName);

            Article = article;
            MasterName = masterName;
            Init(article.Id);

            return this;
        }
        
        #region Implementation of ICommentListModel

        public bool IsEditMode
        {
            get { return false; }
        }

        public IEnumerable<Comment> Comments
        {
            get { return Article.Comments; }
        }
        
        public bool HasComments
        {
            get { return Article != null && Article.HasComments(); }
        }

        public int CommentCount
        {
            get
            {
                return Article == null ? 0 : Article.Comments.Count;
            }
        }

        public string ArticleTitle
        {
            get
            {
                return Article == null ? string.Empty : Article.Title;
            }
        }

        #endregion
    }
}