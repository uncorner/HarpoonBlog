using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using Harpoon.Application.Frontend.ViewModels;
using Harpoon.Application.Notification;
using Harpoon.Application.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;
using NLog;

namespace Harpoon.Application.Frontend.Controllers
{
    public class ArticleController : ControllerBase
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private const int TAKING_ARTICLE_COUNT = 5;
        private readonly IArticleRepository articleRepository;
        private readonly ITagRepository tagRepository;
        private readonly IPersonalSettingRepository settingRepository;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly ICommentNotificationSender notificationSender;

        public ArticleController(ICommentNotificationSender notificationSender, IUnitOfWorkFactory unitOfWorkFactory,
            IArticleRepository articleRepository, ITagRepository tagRepository,
            IPersonalSettingRepository settingRepository)
        {
            this.notificationSender = notificationSender;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.articleRepository = articleRepository;
            this.tagRepository = tagRepository;
            this.settingRepository = settingRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var totalCount = articleRepository.GetPublishedCount();

                var canLoadMore = TAKING_ARTICLE_COUNT < totalCount;
                var articles = articleRepository.FetchAllPublishedAsPreview(TAKING_ARTICLE_COUNT);

                var form = new ArticleListForm(articles, totalCount, canLoadMore);
                return View(form);
            }
        }

        [HttpGet]
        public ActionResult ShowTaggedList(string key)
        {
            CheckParameter("key", key);

            using (unitOfWorkFactory.Create())
            {
                var tag = tagRepository.FetchById(key);
                if (tag == null)
                {
                    throw new ContentNotFoundException("tag", key);
                }

                var foundArticles = articleRepository.FetchAllPublishedByTag(key)
                    .Select(e =>
                    new FoundArticle(e.Id, e.Title, e.PreviewContentData)
                        {
                            PublishedAt = e.PublishedAt
                        });

                var form = new TaggedArticleListForm(foundArticles, tag.Name);
                return View(form);
            }
        }

        [HttpPost]
        public ActionResult GetList(int skippingCount)
        {
            var articles = articleRepository.FetchAllPublishedAsPreview(skippingCount, TAKING_ARTICLE_COUNT);
            return PartialView("PreviewArticleList", articles);
        }

        [HttpGet]
        public ActionResult Show(int? key)
        {
            var id = ConvertParameter("key", key);

            using (unitOfWorkFactory.Create())
            {
                var article = articleRepository.FetchById(id);
                if (article == null)
                {
                    throw new ContentNotFoundException("article", id);
                }

                var setting = settingRepository.Fetch();

                var form = new ArticleForm().Init(article, setting.Name);

                return View(form);
            }
        }

        [HttpPost]
        public ActionResult Show(int key, ArticleForm form)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Show");
            }

            if (ModelState.IsValid)
            {
                bool isError = false;
                try
                {
                    var resCaptchaValue = form.Captcha.Trim();
                    var srcCaptchaValue = Session["captcha_comment_form"].ToString().Trim();
                    if (srcCaptchaValue != resCaptchaValue)
                    {
                        throw new InvalidOperationException();
                    }
                }
                catch (Exception)
                {
                    isError = true;
                }

                // add comment
                if (!isError)
                {
                    var processedContent = CommentContentParser.Parse(form.CommentContent);
                    var comment = new GuestComment(form.CommentName, processedContent)
                    {
                        Email = form.Email
                    };

                    using (var unitOfWork = unitOfWorkFactory.Create())
                    {
                        var article = articleRepository.FetchById(key);
                        article.AddComment(comment);

                        // save comment
                        unitOfWork.Commit();
                        // send notification
                        SendCommentNotification(comment, article);
                    }

                    //ok
                    return Json(new
                    {
                        isError = false,
                        commentHtml = RenderPartialView("Comment", CommentModel.CreateNotEditable(comment)),
                    });
                }

                // if error then tiny pause before response
                System.Threading.Thread.CurrentThread.Join(500);
                // error
                return Json(new
                {
                    isError = true,
                    captchaError = new[] { "Проверочное значение указано некорректно. Попробуйте еще раз" },
                });
            }
            
            return Json(new { isError = true });
        }

        private void SendCommentNotification(GuestComment comment, Article article)
        {
            try
            {
                var articleUrl = Request.Url != null ? Request.Url.AbsoluteUri : string.Empty;
                notificationSender.Send(comment, article, articleUrl);
            }
            catch (Exception ex)
            {
                log.Error("Не удалось отправить уведомление о комментарии: " + ex);
            }
        }

        [HttpGet]
        public ActionResult Rss()
        {
            using (unitOfWorkFactory.Create())
            {
                var setting = settingRepository.Fetch();

                var siteUrlString = GetSiteUrlString();
                var rssUri = new Uri(siteUrlString + "/Rss");

                var feed = new SyndicationFeed(setting.Title, setting.Title + " RSS", rssUri);
                feed.Authors.Add(new SyndicationPerson(setting.Email, setting.Name, siteUrlString));
                feed.Description = new TextSyndicationContent("Заметки");

                var articles = articleRepository.FetchAllPublishedAsPreview(TAKING_ARTICLE_COUNT);
                feed.Items = articles.Select(article =>
                                             new SyndicationItem(
                                                 article.Title,
                                                 new TextSyndicationContent(article.PreviewContentData,
                                                     TextSyndicationContentKind.Html),
                                                 new Uri(string.Format("{0}/article/{1}", siteUrlString, article.Id)),
                                                 article.Id.ToString(),
                                                 new DateTimeOffset(article.PublishedAt.Value)
                                                 ))
                    .ToList();

                return new RssActionResult(feed);
            }
        }

        private string GetSiteUrlString()
        {
            var url = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host;

            if (Request.IsLocal)
            {
                return url + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            }
            return url;
        }

    }
}
