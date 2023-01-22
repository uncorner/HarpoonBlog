using System;
using System.Web.Mvc;
using Harpoon.Core;
using Harpoon.Core.Entities;

namespace Harpoon.Application.ViewHelpers.Rendering
{
    public class GuestComentRenderer : CommentRendererBase
    {
        private readonly GuestComment comment;

        public GuestComentRenderer(HtmlHelper htmlHelper, GuestComment comment, bool isEditMode)
            : base(htmlHelper, isEditMode)
        {
            ArgumentHelper.EnsureNotNull("comment", comment);
            this.comment = comment;
        }

        #region Overrides of CommentRendererBase

        protected override Comment GetComment()
        {
            return comment;
        }

        protected override string GetDeletionConfirmMessage()
        {
            return string.Format("Вы хотите удалить комментарий пользователя {0}?", comment.Name); 
        }

        protected override string GetUserName()
        {
            return comment.Name;
        }

        #endregion
    }
}