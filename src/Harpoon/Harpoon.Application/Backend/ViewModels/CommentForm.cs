using System;
using System.Collections.Generic;
using System.Linq;
using Harpoon.Application.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Entities;

namespace Harpoon.Application.Backend.ViewModels
{
    public class CommentForm : CommentFormBase, ICommentListModel
    {
        public CommentForm Init(Article article)
        {
            ArgumentHelper.EnsureNotNull("article", article);

            Comments = article.Comments;
            ArticleTitle = article.Title;
            Init(article.Id);

            return this;
        }
        
        public IEnumerable<Comment> Comments { get; private set; }
        public string ArticleTitle { get; private set; }

        public bool IsEditMode
        {
            get { return true; }
        }

        public string MasterName
        {
            get { return CommentModel.MASTER_NAME_WHEN_EDIT_MODE; }
        }

        public bool HasComments
        {
            get { return Comments != null && Comments.ToList().Count > 0; }
        }

        public int CommentCount
        {
            get
            {
                return Comments == null ? 0 : Comments.ToList().Count;
            }
        }
        
    }
}