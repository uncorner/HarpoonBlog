using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Harpoon.Application.Frontend.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Ru;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;
using NLog;

namespace Harpoon.Application.Searching
{
    public class SearchEngine : ISearchEngine
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private const string INDEX_PATH = "~/App_Data/SearchIndex";
        private const string ID_NAME = "article_id";
        private const string TITLE_NAME = "article_title";
        private const string CONTENT_NAME = "article_content";
        private const int TOP_HITS = 10;
        private const int FRAGMENT_SIZE = 100;
        private const int MAX_NUM_FRAGMENTS = 2;
        private const Version LUCENE_VERSION = Version.LUCENE_30;
        private const string MORE_TERM = "...";
        // static locker
        private readonly static object locker = new object();
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IArticleRepository articleRepository;

        public SearchEngine(IUnitOfWorkFactory unitOfWorkFactory, IArticleRepository articleRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.articleRepository = articleRepository;
        }

        #region Implementation of ISearchEngine

        public void RebuildIndex()
        {
            lock (locker)
            {
                try
                {
                    log.Debug("Start to rebuild the search index");

                    var indexDirInfo = GetIndexDirInfo();
                    using (var indexDir = FSDirectory.Open(indexDirInfo))
                    {
                        var analyzer = GetAnalyzer();
                        using (var indexWriter = new IndexWriter(indexDir, analyzer, true,
                            IndexWriter.MaxFieldLength.UNLIMITED))
                        {
                            var articles = articleRepository.FetchAllPublishedAsIndexed();

                            foreach (var article in articles)
                            {
                                var document = CreateDocument(article.Id, article.Title, article.ContentData);
                                indexWriter.AddDocument(document);
                            }

                            indexWriter.Optimize();
                            indexWriter.Commit();
                        }
                    }

                    log.Debug("Search index is rebuilt");
                }
                catch (Exception ex)
                {
                    log.Error("Can't rebuild search index: {0}", ex);
                }
            }
        }

        public void UpdateIndex(Article article)
        {
            ArgumentHelper.EnsureNotNull("article", article);
            lock (locker)
            {
                try
                {
                    log.Debug("Start to update the search index");

                    var indexDirInfo = GetIndexDirInfo();
                    using (var indexDir = FSDirectory.Open(indexDirInfo))
                    {
                        var analyzer = GetAnalyzer();
                        using (var indexWriter = new IndexWriter(indexDir, analyzer, false,
                            IndexWriter.MaxFieldLength.UNLIMITED))
                        {
                            if (!article.IsDeleted && article.IsPublished)
                            {
                                var document = CreateDocument(article.Id, article.Title, article.Content.Data);
                                indexWriter.UpdateDocument(new Term(ID_NAME, Convert.ToString(article.Id)), document);
                            }
                            else
                            {
                                indexWriter.DeleteDocuments(new Term(ID_NAME, Convert.ToString(article.Id)));
                            }

                            indexWriter.Optimize();
                            indexWriter.Commit();
                        }
                    }

                    log.Debug("Search index is updated");
                }
                catch (Exception ex)
                {
                    log.Error("Can't update search index: {0}", ex);
                }
            }
        }

        public IEnumerable<FoundArticle> Find(string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            { 
                return Enumerable.Empty<FoundArticle>();
            }
            queryString = queryString.Trim();

            lock (locker)
            {
                var analyzer = GetAnalyzer();
                var parser = new MultiFieldQueryParser(LUCENE_VERSION, new [] { TITLE_NAME, CONTENT_NAME },
                    analyzer);
                var query = parser.Parse(queryString);

                var indexDirInfo = GetIndexDirInfo();
                using (var indexDir = FSDirectory.Open(indexDirInfo))
                {
                    using (var searcher = new IndexSearcher(indexDir))
                    {
                        var hits = searcher.Search(query, TOP_HITS);
                        var articles = new List<FoundArticle>();

                        using (unitOfWorkFactory.Create())
                        {
                            var titleHighlighter = GetHighlighter(query);
                            var contentHighlighter = GetHighlighter(query);
                            contentHighlighter.TextFragmenter = new SimpleFragmenter(FRAGMENT_SIZE);
                            
                            foreach (var scoreDoc in hits.ScoreDocs)
                            {
                                var docId = scoreDoc.Doc;
                                var doc = searcher.Doc(docId);
                                var id = Convert.ToInt32(doc.Get(ID_NAME));
                                var title = Convert.ToString(doc.Get(TITLE_NAME));
                                var content = Convert.ToString(doc.Get(CONTENT_NAME));
                                var publishedAt = articleRepository.FetchPublishedAtById(id);
                                var processedContent = GetProcessedContent(analyzer, contentHighlighter, content);
                                var processedTitle = GetProcessedTitle(analyzer, titleHighlighter, title);

                                articles.Add(new FoundArticle(id, processedTitle, processedContent)
                                                 {
                                                     PublishedAt = publishedAt
                                                 });
                            }

                            return articles;
                        }
                    }
                } // indexDir

            }
        }
        
        #endregion

        private string GetProcessedTitle(Analyzer analyzer, Highlighter highlighter, string title)
        {
            var processedTitle = highlighter.GetBestFragment(analyzer, TITLE_NAME, title);
            if (string.IsNullOrEmpty(processedTitle))
            {
                processedTitle = title;
            }
            return processedTitle;
        }

        private string GetProcessedContent(Analyzer analyzer, Highlighter highlighter, string content)
        {
            var stream = analyzer.TokenStream(CONTENT_NAME, new StringReader(content));
            var processedContent = highlighter.GetBestFragments(stream, content, MAX_NUM_FRAGMENTS, MORE_TERM);
            if (string.IsNullOrEmpty(processedContent))
            {
                processedContent = content.Length > FRAGMENT_SIZE
                                       ? content.Substring(0, FRAGMENT_SIZE) + MORE_TERM
                                       : content;
            }
            return processedContent;
        }

        private Highlighter GetHighlighter(Query query)
        {
            var formatter = new SimpleHTMLFormatter(@"<span class=""highlighted-fragment"">", "</span>");
            return new Highlighter(formatter, new QueryScorer(query));
        }

        private Document CreateDocument(int id, string title, string content)
        {
            var document = new Document();
            document.Add(new Field(ID_NAME, id.ToString(),
                                   Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO));

            document.Add(new Field(TITLE_NAME, title,
                                   Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));

            document.Add(new Field(CONTENT_NAME, HtmlCleaner.RemoveTags(content) ,
                                   Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));

            return document;
        }

        private DirectoryInfo GetIndexDirInfo()
        {
            var indexPath = System.Web.Hosting.HostingEnvironment.MapPath(INDEX_PATH);
            if (indexPath == null)
            {
                throw new NullReferenceException("Index path is a null");
            }

            return new DirectoryInfo(indexPath);
        }

        private Analyzer GetAnalyzer()
        {
            return new RussianAnalyzer(LUCENE_VERSION);
        }

    }
}