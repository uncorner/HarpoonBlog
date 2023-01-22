using MvcPaging;

namespace Harpoon.Application.ViewModels
{
    public interface IPagingModel
    {
        IPagedList GetPagedList();
        string AjaxAction { get; }
        int MaxNumberOfPages { get; }
    }
}