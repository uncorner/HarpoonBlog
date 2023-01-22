using System.Collections.Generic;
using Harpoon.Application.Frontend.ViewModels;
using Harpoon.Core.Entities;

namespace Harpoon.Application.Searching
{
    public interface ISearchEngine
    {
        void RebuildIndex();
        IEnumerable<FoundArticle> Find(string queryString);
        void UpdateIndex(Article article);
    }
}