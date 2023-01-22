using System.Web.Mvc;
using Harpoon.Application.ViewHelpers.Rendering;
using Harpoon.Application.ViewModels;
using Harpoon.Core.Entities;

namespace Harpoon.Application.ViewHelpers
{
    public static class CommentHelper
    {
        public static MvcHtmlString Comment(this HtmlHelper htmlHelper, CommentModel commentModel)
        {
            CommentRendererBase render;

            if (commentModel.Comment is GuestComment)
            {
                render = new GuestComentRenderer(htmlHelper, (GuestComment) commentModel.Comment,
                    commentModel.IsEditMode);
            }
            else
            {
                render = new MasterCommentRenderer(htmlHelper, commentModel);
            }

            return render.Render();
        }
        
    }
}