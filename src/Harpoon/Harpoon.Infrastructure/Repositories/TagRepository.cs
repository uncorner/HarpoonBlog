using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Entities.Projections;
using Harpoon.Core.Repositories;
using Harpoon.Infrastructure.Ef;

namespace Harpoon.Infrastructure.Repositories
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        static TagRepository()
        {
            Mapper.CreateMap<Tag, TagInfo>();
        }

        #region FetchAllActual

        public IEnumerable<TagInfo> FetchAllActual()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var tags = BuildAllActualQuery(dispatcher)
                    .ToList();

                return Mapper.Map<IList<Tag>, IList<TagInfo>>(tags);
            }
        }

        public IEnumerable<TagInfo> FetchAllActual(int limit)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var tags = BuildAllActualQuery(dispatcher)
                    .Take(limit)
                    .ToList();

                return Mapper.Map<IList<Tag>, IList<TagInfo>>(tags);
            }
        }

        public IEnumerable<string> FetchAllActualByPattern(IEnumerable<string> excludedTags, string pattern)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("pattern", pattern);
            ArgumentHelper.EnsureNotNull("excludedTags", excludedTags);

            using (var dispatcher = GetDbContextDispatcher())
            {
                return BuildAllActualQuery(dispatcher)
                    .Where(e => !excludedTags.Contains(e.Name))
                    .Where(e => e.Name.StartsWith(pattern))
                    .Select(e => new {e.Name})
                    .ToList()
                    .Select(s => s.Name);
            }
        }

        public int GetActualCount()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return BuildAllActualQuery(dispatcher)
                    .Count();
            }
        }

        private IQueryable<Tag> BuildAllActualQuery(DbContextDispatcherBase dispatcher)
        {
            return dispatcher.DbContext
                .Set<Tag>()
                .Include("Articles")
                .Where(e => e.Articles.Any(a => a.IsPublished && !a.IsDeleted));
        }

        #endregion

        public Tag FetchByName(string name)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("name", name);

            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<Tag>()
                    .Where(e => e.Name == name)
                    .SingleOrDefault();
            }
        }

        public TagInfo FetchById(string tagId)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("tagId", tagId);
            
            using (var dispatcher = GetDbContextDispatcher())
            {
                var tag = dispatcher.DbContext
                    .Set<Tag>()
                    .Where(e => e.Id == tagId)
                    .SingleOrDefault();

                return Mapper.Map<Tag, TagInfo>(tag);
            }
        }

    }
}