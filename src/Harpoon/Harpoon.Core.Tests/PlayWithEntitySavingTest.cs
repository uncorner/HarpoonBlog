using System;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;
using Harpoon.Infrastructure;
using Harpoon.Infrastructure.Repositories;
using NUnit.Framework;
using System.Linq;

namespace Harpoon.Core.Tests
{
    [TestFixture]
    public class PlayWithEntitySavingTest
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory = new UnitOfWorkFactory();
        private readonly IArticleRepository articleRepository = new ArticleRepository();
        private readonly ITagRepository tagRepository = new TagRepository();
        private readonly IChapterRepository chapterRepository = new ChapterRepository();

        [Test]
        [Ignore]
        public void TestSaveArticle()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var hms = GetHMS();
                var tag = new Tag("new_" + hms);
                
                var article = new Article("NewArticle " + DateTime.Now, true, "Content text", "Preview text");
                article.AddTag(tag);

                var comment = new GuestComment("Andrey", "Это комментарий к Заметке");
                comment.Email = "andrey@gmail.com";
                article.AddComment(comment);

                tagRepository.Add(tag);
                articleRepository.Add(article);

                unitOfWork.Commit();
            }
        }

        [Test]
        [Ignore]
        public void TestSaveChapter()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var tagName = "tagname_" + GetHMS();
                var chapter = new Chapter("new chapter", 5000, tagName, true, "Content text");

                chapterRepository.Add(chapter);

                unitOfWork.Commit();
            }
        }
        
        [Test]
        [Ignore]
        public void TestEditArticleTags()
        {
            const string tagOneName = "tag_one";
            const string tagTwoName = "tag_two";
            const string tagThreeName = "tag_three";
            const string tagFourName = "tag_four";

            Article savedArticle;

            // create
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = new Article("NewArticle " + DateTime.Now, true, "Content text", "Preview text");

                var tagOne = tagRepository.FetchByName(tagOneName)
                    ?? new Tag(tagOneName);

                article.AddTag(tagOne);

                var tagTwo = tagRepository.FetchByName(tagTwoName)
                    ?? new Tag(tagTwoName);

                article.AddTag(tagTwo);

                articleRepository.Add(article);
                savedArticle = article;

                unitOfWork.Commit();
            }

            // edit
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = articleRepository.FetchById(savedArticle.Id);

                Assert.IsNotNull(article.Tags);
                Assert.AreEqual(2, article.Tags.Count);

                var tagTwo = tagRepository.FetchByName(tagTwoName);
                article.Tags.Remove(tagTwo);
                
                var tagThree = tagRepository.FetchByName(tagThreeName)
                    ?? new Tag(tagThreeName);

                article.AddTag(tagThree);

                var tagFour = tagRepository.FetchByName(tagFourName)
                    ?? new Tag(tagFourName);
                
                article.AddTag(tagFour);
                
                unitOfWork.Commit();
            }

            // read
            var fetchedArticle = articleRepository.FetchById(savedArticle.Id);

            Assert.IsNotNull(fetchedArticle.Tags);
            Assert.AreEqual(3, fetchedArticle.Tags.Count);

            Assert.IsTrue(fetchedArticle.Tags.Any(e => e.Name == tagOneName));
            Assert.IsTrue(fetchedArticle.Tags.Any(e => e.Name == tagThreeName));
            Assert.IsTrue(fetchedArticle.Tags.Any(e => e.Name == tagFourName));
        }

        [Test]
        [Ignore]
        public void TestEditArticleComment()
        {
            Article savedArticle;
            Article savedArticle_2;

            // create
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = new Article("NewArticle " + DateTime.Now, true, "Content text", "Preview text");
                
                var commentA = new GuestComment("A", "aaa");
                var commentB = new GuestComment("B", "bbb");
                var commentC = new GuestComment("C", "ccc") { IsDeleted = true };

                article.AddComment(commentA);
                article.AddComment(commentB);
                article.AddComment(commentC);

                articleRepository.Add(article);
                savedArticle = article;

                var article_2 = new Article("NewArticle without comments " + DateTime.Now, true,
                    "Content text", "Preview text");
                articleRepository.Add(article_2);
                savedArticle_2 = article_2;

                unitOfWork.Commit();
            }

            // edit
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = articleRepository.FetchById(savedArticle.Id);

                Assert.IsNotNull(article.Comments);
                Assert.AreEqual(2, article.Comments.Count);

                // add D
                var commentD = new GuestComment("D", "ddd");
                article.Comments.Add(commentD);

                var article_2 = articleRepository.FetchById(savedArticle_2.Id);

                Assert.IsNotNull(article_2.Comments);
                Assert.AreEqual(0, article_2.Comments.Count);

                unitOfWork.Commit();
            }
            
            // read
            var fetchedArticle = articleRepository.FetchById(savedArticle.Id);

            var comments = fetchedArticle.Comments.Cast<GuestComment>().ToList();
            Assert.IsNotNull(comments);
            Assert.AreEqual(3, comments.Count);

            Assert.IsTrue(comments.Any(e => e.Name == "A"));
            Assert.IsTrue(comments.Any(e => e.Name == "B"));
            Assert.IsTrue(comments.Any(e => e.Name == "D"));
        }

        [Test]
        [Ignore]
        public void TestFetchCommentatorEmails()
        {
            var article = SetupCommentatorEmailData();
            var emails = articleRepository.FetchCommentatorEmails(article.Id, "gregory@gzzzzzz.org")
                .ToList();

            Assert.AreEqual(2, emails.Count);
            Assert.IsTrue(emails.Any(e => e == "artem@gzzzzzz.org"));
            Assert.IsTrue(emails.Any(e => e == "vitaliy@gzzzzzz.org"));
        }

        private Article SetupCommentatorEmailData()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = new Article("Article for commentator emails " + DateTime.Now,
                                          true, "Content text", "Preview text");

                article.AddComment(new GuestComment("Артем", "текст")
                                       {
                                           Email = "  artem@gzzzzzz.org    "
                                       });

                article.AddComment(new GuestComment("Колян", "текст")
                                       {
                                           IsDeleted = true,
                                           Email = "kolian@gzzzzzz.org"
                                       });

                article.AddComment(new GuestComment("Валера", "текст")
                                       {
                                           Email = ""
                                       });

                article.AddComment(new GuestComment("Антон", "текст"));

                article.AddComment(new Comment("текст"));

                article.AddComment(new GuestComment("Виталий", "текст")
                                       {
                                           Email = "vitaliy@gzzzzzz.org"
                                       });

                article.AddComment(new GuestComment("Артем", "текст")
                                       {
                                           Email = "artem@gzzzzzz.org"
                                       });

                article.AddComment(new GuestComment("Григорий", "текст")
                {
                    Email = "gregory@gzzzzzz.org"
                });

                articleRepository.Add(article);
                unitOfWork.Commit();

                return article;
            }
        }

        private string GetHMS()
        {
            return DateTime.Now.ToString("HHmmss");
        }

    }
}