using System.Collections.Generic;
using Harpoon.Core;

namespace Harpoon.Application.Frontend.ViewModels
{
    public class TaggedArticleListForm
    {
        public IEnumerable<FoundArticle> Articles { get; private set; }
        public string TagName { get; private set; }
        
        public TaggedArticleListForm(IEnumerable<FoundArticle> articles, string tagName)
        {
            ArgumentHelper.EnsureNotNull("articles", articles);
            ArgumentHelper.EnsureNotNullOrEmpty("tagName", tagName);

            Articles = articles;
            TagName = tagName;
        }

    }
}