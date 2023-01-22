using System.Threading;
using System.Web.Mvc;
using Harpoon.Application.Backend.ViewModels;
using Harpoon.Application.Searching;
using Harpoon.Application.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;
using MvcPaging;

namespace Harpoon.Application.Backend.Controllers
{
    public class ArticleController : ControllerBase
    {
        private const int DEFAULT_PAGE_SIZE = 7;
        private const int MAX_NUMBER_OF_PAGES = 10;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IArticleRepository articleRepository;
        private readonly ITagRepository tagRepository;
        private readonly ISearchEngine searchEngine;
        
        public ArticleController(IUnitOfWorkFactory unitOfWorkFactory, IArticleRepository articleRepository,
            ITagRepository tagRepository, ISearchEngine searchEngine)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.articleRepository = articleRepository;
            this.tagRepository = tagRepository;
            this.searchEngine = searchEngine;
        }

        #region New article

        [HttpGet]
        public ActionResult NewArticle()
        {
            return View("ProcessArticle", new NewArticleForm());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewArticle(NewArticleForm form)
        {
            return ProcessArticle(form);
        }
        
        #endregion

        #region Edit article

        [HttpGet]
        public ActionResult EditArticle(int? id)
        {
            var idValue = ConvertParameter("id", id);

            var article = articleRepository.FetchById(idValue);
            if (article == null)
            {
                throw new ContentNotFoundException("article", idValue);
            }

            var form = new EditArticleForm(article);
            return View("ProcessArticle", form);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditArticle(int id, EditArticleForm form)
        {
            return ProcessArticle(form, id);
        }

        #endregion

        private Article DoNewArticle(NewArticleForm form)
        {
            var preview = PreviewProcessor.GetFirstParagraph(form.Content);
            return new Article(form.Title, form.IsPublished, form.Content, preview);
        }

        private Article DoEditArticle(EditArticleForm form, int id)
        {
            var article = articleRepository.FetchById(id);
            article.SetTitle(form.Title);
            article.SetIsPublished(form.IsPublished);
            article.SetUpdatedNow();
            article.SetContentData(form.Content);

            var preview = PreviewProcessor.GetFirstParagraph(form.Content);
            article.SetPreviewContentData(preview);
            
            article.ClearTags();

            return article;
        }

        private ActionResult ProcessArticle(NewArticleForm form, int? id = null)
        {
            if (ModelState.IsValid)
            {
                if (!form.HasContent())
                {
                    ModelState.AddModelError("", "Содержание не может быть пустым");
                    return View("ProcessArticle", form);
                }

                using (var unitOfWork = unitOfWorkFactory.Create())
                {
                    var article = form is EditArticleForm && id.HasValue
                                          ? DoEditArticle((EditArticleForm)form, id.Value)
                                          : DoNewArticle(form);

                    var tagNames = form.GetTagNames();
                    foreach (var tagName in tagNames)
                    {
                        var tag = tagRepository.FetchByName(tagName);
                        if (tag == null)
                        {
                            tag = new Tag(tagName);
                        }

                        article.AddTag(tag);
                    }

                    if (IsNewArticleForm(form))
                    {
                        articleRepository.Add(article);
                    }

                    unitOfWork.Commit();
                    UpdateIndex(article);

                    if (IsNewArticleForm(form))
                    {
                        AddFlashMessage(string.Format("Заметка «{0}» была успешно создана", article.Title));
                        return RedirectToAction("ShowArticles");
                    }

                    // else EditArticleForm
                    AddFlashMessage("Заметка была успешно сохранена");
                    return RedirectToAction("EditArticle", new { id = article.Id });
                }
            }

            return View("ProcessArticle", form);
        }

        private bool IsNewArticleForm(NewArticleForm form)
        {
            return !(form is EditArticleForm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var article = articleRepository.FetchById(id);
                if (article == null)
                {
                    throw new ContentNotFoundException("article", id);
                }
                
                article.IsDeleted = true;
                unitOfWork.Commit();

                UpdateIndex(article);

                AddFlashMessage(string.Format(@"Заметка ""{0}"" была удалена", article.Title));

                return RedirectToAction("ShowArticles");
            }
        }

        #region Show Articles

        [HttpGet]
        public ActionResult ShowArticles()
        {
            var articles = articleRepository.FetchAllArticles()
                .ToPagedList(0, DEFAULT_PAGE_SIZE);

            var form = new ArticleGridForm(articles, "GetArticleGrid", MAX_NUMBER_OF_PAGES);

            return View(form);
        }

        [HttpGet]
        public ActionResult GetArticleGrid(int? page)
        {
            var currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var articles = articleRepository.FetchAllArticles()
                .ToPagedList(currentPageIndex, DEFAULT_PAGE_SIZE);

            var form = new ArticleGridForm(articles, "GetArticleGrid", MAX_NUMBER_OF_PAGES);

            return PartialView("ArticleGrid", form);
        }

        private void UpdateIndex(Article article)
        {
            var thread = new Thread(DoUpdateIndex);
            thread.Start(article);
        }

        private void DoUpdateIndex(object param)
        {
            searchEngine.UpdateIndex(param as Article);
        }

        #endregion

        #region Edit Comments
        
        [HttpGet]
        public ActionResult AddComment(int? id)
        {
            var idValue = ConvertParameter("id", id);

            using (unitOfWorkFactory.Create())
            {
                var article = articleRepository.FetchById(idValue);
                if (article == null)
                {
                    throw new ContentNotFoundException("article", idValue);
                }
                
                if (!article.HasComments())
                {
                    return RedirectToRoute("AdminRoot");
                }

                var form = new CommentForm().Init(article);
                               
                return View(form);
            }
        }

        [HttpPost]
        public ActionResult AddComment(int id, CommentForm form)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("AddComment", new { id = id });
            }

            if (ModelState.IsValid)
            {
                var processedContent = CommentContentParser.Parse(form.CommentContent);
                var comment = new Comment(processedContent);

                using (var unitOfWork = unitOfWorkFactory.Create())
                {
                    var article = articleRepository.FetchById(id);
                    article.AddComment(comment);
                    // save comment
                    unitOfWork.Commit();
                }

                //ok
                return Json(new
                {
                    isError = false,
                    commentHtml = RenderPartialView("Comment", CommentModel.CreateEditable(comment)),
                });
            }

            return Json(new { isError = true });
        }

        [HttpPost]
        public JsonResult DeleteComment(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return Json(null);
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var comment = articleRepository.FetchCommentById(id);
                if (comment == null)
                {
                    return Json(null);
                }

                comment.IsDeleted = true;
                unitOfWork.Commit();

                return Json(new { id });
            }
        }

        #endregion

    }
}