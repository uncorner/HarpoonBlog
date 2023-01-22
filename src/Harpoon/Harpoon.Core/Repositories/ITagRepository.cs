using System.Collections.Generic;
using Harpoon.Core.Entities;
using Harpoon.Core.Entities.Projections;

namespace Harpoon.Core.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<TagInfo> FetchAllActual();
        IEnumerable<TagInfo> FetchAllActual(int limit);
        IEnumerable<string> FetchAllActualByPattern(IEnumerable<string> excludeTags, string pattern);
        int GetActualCount();
        Tag FetchByName(string name);
        TagInfo FetchById(string tagId);
        void Add(Tag tag);
    }
}