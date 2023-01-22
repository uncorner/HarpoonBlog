using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Harpoon.Application.ViewModels;
using MvcPaging;

namespace Harpoon.Application.ViewHelpers
{
    public static class ExtPagerHelper
    {
        public static MvcHtmlString ExtPager(this HtmlHelper htmlHelper, IPagingModel model, string updateTargetId)
        {
            var pagedList = model.GetPagedList();

            if (pagedList.PageSize >= pagedList.TotalItemCount)
            {
                return MvcHtmlString.Empty;
            }

            var pager = htmlHelper.Pager(pagedList.PageSize, pagedList.PageNumber, pagedList.TotalItemCount,
                new AjaxOptions { UpdateTargetId = updateTargetId })
                .Options(o => o.MaxNrOfPages(model.MaxNumberOfPages))
                .Options(o => o.Action(model.AjaxAction));

            var html = string.Format(@"<div class=""pager"">{0}</div>", pager.ToHtmlString());
            return new MvcHtmlString(html);
        }

    }
}