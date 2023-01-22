using System.Collections.Generic;
using Harpoon.Core.Entities;
using Harpoon.Core.Entities.Projections;

namespace Harpoon.Core.Repositories
{
    public interface IChapterRepository
    {
        IEnumerable<ChapterInfo> FetchPublishedChapters();
        Chapter FetchByTagName(string tagName);
        void Add(Chapter chapter);
        IEnumerable<ChapterInfo> FetchAllChapters();
        int GetMaxOrderValue();
        bool HasChapterWithTagName(string tagName);
        Chapter FetchById(int id);
        void UpdateOrder(int id, int orderValue);
    }
}