using System.Collections.Generic;
using Harpoon.Application.ViewModels;
using Harpoon.Core.Entities.Projections;
using MvcPaging;

namespace Harpoon.Application.Backend.ViewModels
{
    public class ArticleGridForm : IPagingModel
    {
        private readonly IPagedList<ArticleInfo> articles;
        public string AjaxAction { get; private set; }
        public int MaxNumberOfPages { get; private set; }

        public IEnumerable<ArticleInfo> Articles
        {
            get { return articles; }
        }

        public bool HasArticles
        {
            get { return articles != null && articles.Count > 0; }
        }

        public ArticleGridForm(IPagedList<ArticleInfo> articles, string ajaxAction, int maxNumberOfPages)
        {
            this.articles = articles;
            AjaxAction = ajaxAction;
            MaxNumberOfPages = maxNumberOfPages;
        }

        public IPagedList GetPagedList()
        {
            return articles;
        }
        
    }
}