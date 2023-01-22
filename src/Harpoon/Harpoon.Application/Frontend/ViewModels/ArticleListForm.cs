using System.Collections.Generic;
using Harpoon.Core;
using Harpoon.Core.Entities.Projections;

namespace Harpoon.Application.Frontend.ViewModels
{
    public class ArticleListForm
    {
        public int TotalCount { get; private set; }
        public bool CanLoadMore { get; private set; }
        public IEnumerable<PreviewArticleInfo> Articles { get; private set; }
        
        public ArticleListForm(IEnumerable<PreviewArticleInfo> articles, int totalCount, bool canLoadMore)
        {
            ArgumentHelper.EnsureNotNull("articles", articles);

            Articles = articles;
            TotalCount = totalCount;
            CanLoadMore = canLoadMore;
        }
        
    }
}