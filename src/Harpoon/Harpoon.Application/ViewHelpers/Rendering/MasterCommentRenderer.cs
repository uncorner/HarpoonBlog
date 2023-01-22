using System;
using System.Web.Mvc;
using Harpoon.Application.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Entities;

namespace Harpoon.Application.ViewHelpers.Rendering
{
    public class MasterCommentRenderer : CommentRendererBase
    {
        private readonly CommentModel commentModel;

        #region Overrides of CommentRendererBase

        public MasterCommentRenderer(HtmlHelper htmlHelper, CommentModel commentModel)
            : base(htmlHelper, commentModel.IsEditMode)
        {
            ArgumentHelper.EnsureNotNull("commentModel", commentModel);
            this.commentModel = commentModel;
        }

        protected override Comment GetComment()
        {
            return commentModel.Comment;
        }

        protected override string GetDeletionConfirmMessage()
        {
            return "Вы хотите удалить собственный комментарий?";
        }

        protected override string GetUserName()
        {
            return commentModel.MasterName;
        }

        protected override string GetCommentBoxClasses()
        {
            return base.GetCommentBoxClasses() + " master-comment";
        }
        
        #endregion

    }
}