using System;
using System.Collections.Generic;
using Harpoon.Core.Entities;
using Harpoon.Core.Entities.Projections;

namespace Harpoon.Core.Repositories
{
    public interface IArticleRepository
    {
        IEnumerable<ArticleInfo> FetchAllPublished(int takingCount);
        IEnumerable<ArticleInfo> FetchAllPublished(int skipingCount, int takingCount);
        IEnumerable<PreviewArticleInfo> FetchAllPublishedAsPreview(int takingCount);
        IEnumerable<PreviewArticleInfo> FetchAllPublishedAsPreview(int skipingCount, int takingCount);
        int GetPublishedCount();
        IEnumerable<PreviewArticleInfo> FetchAllPublishedByTag(string tag);
        IEnumerable<ArticleInfo> FetchAllArticles();
        Article FetchById(int id);
        DateTime? FetchPublishedAtById(int id);
        IEnumerable<Article> FetchAllWithTags();
        IEnumerable<IndexedArticleInfo> FetchAllPublishedAsIndexed();
        void Add(Article article);
        void Remove(Article article);
        Comment FetchCommentById(int id);
        IEnumerable<string> FetchCommentatorEmails(int articleId, string excludedEmail);
    }
}
