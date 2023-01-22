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
    public class ChapterRepository : RepositoryBase<Chapter>, IChapterRepository
    {
        static ChapterRepository()
        {
            Mapper.CreateMap<Chapter, ChapterInfo>();
        }
        
        public IEnumerable<ChapterInfo> FetchPublishedChapters()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var chapters = dispatcher.DbContext
                    .Set<Chapter>()
                    .Where(e => !e.IsDeleted && e.IsPublished)
                    .OrderBy(e => e.OrderValue)
                    .ToList();

                return Mapper.Map<IList<Chapter>, IList<ChapterInfo>>(chapters);
            }
        }

        public Chapter FetchByTagName(string tagName)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("tagName", tagName);

            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<Chapter>()
                    .Include("Content")
                    .Where(e => !e.IsDeleted && e.TagName == tagName)
                    .SingleOrDefault();
            }
        }

        public IEnumerable<ChapterInfo> FetchAllChapters()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var chapters = dispatcher.DbContext
                    .Set<Chapter>()
                    .Where(e => !e.IsDeleted)
                    .OrderBy(e => e.OrderValue)
                    .ToList();

                return Mapper.Map<IList<Chapter>, IList<ChapterInfo>>(chapters);
            }
        }

        public int GetMaxOrderValue()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                           .Set<Chapter>()
                           .Max(e => (int?) e.OrderValue)
                       ?? 0;
            }
        }

        public bool HasChapterWithTagName(string tagName)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("tagName", tagName);

            using (var dispatcher = GetDbContextDispatcher())
            {
                var chapter = dispatcher.DbContext
                    .Set<Chapter>()
                    .Where(e => e.TagName == tagName)
                    .FirstOrDefault();

                return chapter != null ? true : false;
            }
        }

        public Chapter FetchById(int id)
        {
            return FetchById(id, true);
        }

        private Chapter FetchById(int id, bool includeContent)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<Chapter>()
                    .Include("Content", includeContent)
                    .Where(e => e.Id == id && !e.IsDeleted)
                    .FirstOrDefault();
            }
        }

        public void UpdateOrder(int id, int orderValue)
        {
            var chapter = FetchById(id, false);
            if (chapter == null)
            {
                throw new InfrastructureException(
                    string.Format("Chapter entity with id={0} is not found", id));
            }

            chapter.SetOrderValue(orderValue);
        }
        
    }
}