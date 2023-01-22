using System.Collections.Generic;
using System.Linq;
using Harpoon.Core;

namespace Harpoon.Application.Frontend.ViewModels
{
    public class SearchForm
    {
        public IEnumerable<FoundArticle> Articles { get; private set; }
        public string QueryString { get; private set; }

        public bool HasArticles
        {
            get
            {
                return Articles.ToList().Count > 0;
            }
        }

        public SearchForm(string queryString, IEnumerable<FoundArticle> articles)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("queryString", queryString);
            ArgumentHelper.EnsureNotNull("articles", articles);

            QueryString = queryString;
            Articles = articles;
        }
        
    }
}