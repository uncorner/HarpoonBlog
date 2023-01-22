using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Harpoon.Core;
using Harpoon.Core.Entities;

namespace Harpoon.Application.ViewHelpers.Rendering
{
    public abstract class CommentRendererBase
    {
        protected HtmlHelper htmlHelper;
        protected bool isEditMode;
        
        protected CommentRendererBase(HtmlHelper htmlHelper, bool isEditMode)
        {
            ArgumentHelper.EnsureNotNull("htmlHelper", htmlHelper);
            this.htmlHelper = htmlHelper;
            this.isEditMode = isEditMode;
        }

        protected abstract Comment GetComment();

        protected abstract string GetDeletionConfirmMessage();

        protected virtual string GetCommentBoxClasses()
        {
            return "comment-box";
        }

        protected abstract string GetUserName();
        
        public MvcHtmlString Render()
        {
            var comment = GetComment();
            var commentId = "comment-" + comment.Id;

            var html = string.Format(@"<div id=""{0}"" class=""{1}"">", commentId, GetCommentBoxClasses());

            html += @"<div class=""comment-header"">";
            html += string.Format(@"<span class=""comment-name"">{0}</span>", GetUserName());
            html += RenderDeletionLink();
            html += string.Format(@"<span class=""comment-date"">{0}</span>",
                htmlHelper.DateTimeHm(comment.CreatedAt));
            html += @"</div>";

            html += string.Format(@"<div class=""comment-content"">{0}</div>", comment.Content);

            html += @"</div>";

            return new MvcHtmlString(html);
        }

        protected string RenderDeletionLink()
        {
            if (!isEditMode)
            {
                return string.Empty;
            }

            var ajaxHelper = new AjaxHelper(htmlHelper.ViewContext, htmlHelper.ViewDataContainer);
            return ajaxHelper.ActionLink("Удалить", "DeleteComment", "Article",
                                         new {id = GetComment().Id},
                                         new AjaxOptions
                                             {
                                                 HttpMethod = "post",
                                                 OnSuccess = "harpoon.onSuccessCommentDelete",
                                                 Confirm = GetDeletionConfirmMessage()
                                             },
                                         new {@class = "comment-del"})
                                         .ToHtmlString();
        }

    }
}
