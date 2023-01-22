using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Entities.Projections;
using Harpoon.Core.Repositories;
using Harpoon.Infrastructure.Ef;

namespace Harpoon.Infrastructure.Repositories
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        static ArticleRepository()
        {
            Mapper.CreateMap<Article, ArticleInfo>();
            Mapper.CreateMap<Article, PreviewArticleInfo>();
            Mapper.CreateMap<Article, IndexedArticleInfo>();
        }

        #region Fetch Published

        public IEnumerable<ArticleInfo> FetchAllPublished(int takingCount)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var articles = GetAllPublishedQuery(dispatcher)
                    .Take(takingCount)
                    .ToList();

                return Mapper.Map<IList<Article>, IList<ArticleInfo>>(articles);
            }
        }

        public IEnumerable<ArticleInfo> FetchAllPublished(int skipingCount, int takingCount)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var articles = GetAllPublishedQuery(dispatcher)
                    .Skip(skipingCount)
                    .Take(takingCount)
                    .ToList();

                return Mapper.Map<IList<Article>, IList<ArticleInfo>>(articles);
            }
        }

        public IEnumerable<PreviewArticleInfo> FetchAllPublishedAsPreview(int takingCount)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var articles = GetAllPublishedQuery(
                    dispatcher.DbContext
                    .Set<Article>()
                    .Include("PreviewContent"))
                    .Take(takingCount)
                    .ToList();

                return Mapper.Map<IList<Article>, IList<PreviewArticleInfo>>(articles);
            }
        }

        public IEnumerable<PreviewArticleInfo> FetchAllPublishedAsPreview(int skipingCount, int takingCount)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var articles = GetAllPublishedQuery(
                    dispatcher.DbContext
                    .Set<Article>()
                    .Include("PreviewContent"))
                    .Skip(skipingCount)
                    .Take(takingCount)
                    .ToList();

                return Mapper.Map<IList<Article>, IList<PreviewArticleInfo>>(articles);
            }
        }

        public int GetPublishedCount()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<Article>()
                    .Where(e => e.IsPublished && !e.IsDeleted)
                    .Count();
            }
        }

        public IEnumerable<PreviewArticleInfo> FetchAllPublishedByTag(string tag)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("tag", tag);

            using (var dispatcher = GetDbContextDispatcher())
            {
                var articles = dispatcher.DbContext
                    .Set<Article>()
                    .Include("Tags")
                    .Include("PreviewContent")
                    .Where(e => e.IsPublished && !e.IsDeleted && e.Tags.Any(t => t.Id == tag))
                    .OrderByDescending(e => e.PublishedAt)
                    .ToList();

                return Mapper.Map<IList<Article>, IList<PreviewArticleInfo>>(articles);
            }
        }

        private IQueryable<Article> GetAllPublishedQuery(DbContextDispatcherBase dispatcher)
        {
            return GetAllPublishedQuery(dispatcher.DbContext.Set<Article>());
        }

        private IQueryable<Article> GetAllPublishedQuery(IQueryable<Article> sourceQuery)
        {
            return sourceQuery
                .Where(e => e.IsPublished && !e.IsDeleted)
                .OrderByDescending(e => e.PublishedAt);
        }

        #endregion

        public IEnumerable<ArticleInfo> FetchAllArticles()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var articles = dispatcher.DbContext
                    .Set<Article>()
                    .Where(e => !e.IsDeleted)
                    .OrderByDescending(e => e.CreatedAt)
                    .ToList();

                return Mapper.Map<IList<Article>, IList<ArticleInfo>>(articles);
            }
        }
        
        public Article FetchById(int id)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var article = dispatcher.DbContext
                    .Set<Article>()
                    .Include("Tags")
                    .Include("Content")
                    .Include("PreviewContent")
                    .Where(e => e.Id == id && !e.IsDeleted)
                    .FirstOrDefault();

                if (article != null)
                {
                    dispatcher.DbContext.Entry(article)
                        .InitializedCollection(e => e.Comments)
                        .Query()
                        .Where(c => !c.IsDeleted)
                        .Load();
                }

                return article;
            }
        }

        public DateTime? FetchPublishedAtById(int id)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<Article>()
                    .Where(e => e.Id == id && !e.IsDeleted && e.IsPublished)
                    .SingleOrDefault()
                    .PublishedAt;
            }
        }

        public IEnumerable<Article> FetchAllWithTags()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<Article>()
                    .Include("Tags")
                    .Where(e => !e.IsDeleted)
                    .ToList();
            }
        }

        public IEnumerable<IndexedArticleInfo> FetchAllPublishedAsIndexed()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var articles = GetAllPublishedQuery(dispatcher)
                    .Include("Tags")
                    .Include("Content")
                    .ToList();

                return Mapper.Map<IList<Article>, IList<IndexedArticleInfo>>(articles);
            }
        }

        public Comment FetchCommentById(int id)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<Comment>()
                    .Where(e => e.Id == id && !e.IsDeleted)
                    .FirstOrDefault();
            }
        }

        public IEnumerable<string> FetchCommentatorEmails(int articleId, string excludedEmail = null)
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                var query = dispatcher.DbContext
                    .Set<Comment>()
                    .OfType<GuestComment>()
                    .Where(c => !c.IsDeleted && c.ArticleId == articleId)
                    .Select(gc => new {gc.Email})
                    .Distinct();

                query = !string.IsNullOrEmpty(excludedEmail)
                    ? query.Where(s => !string.IsNullOrEmpty(s.Email) && s.Email != excludedEmail)
                    : query.Where(s => !string.IsNullOrEmpty(s.Email));
                    
                return query.ToList()
                    .Select(s => s.Email);
            }
        }

    }
}
