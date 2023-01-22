using System;
using System.Web.Mvc;
using Harpoon.Application.Backend.ViewModels;
using Harpoon.Core;
using Harpoon.Core.Entities;
using Harpoon.Core.Repositories;

namespace Harpoon.Application.Backend.Controllers
{
    public class ChapterController : ControllerBase
    {
        private const int ORDER_VALUE_STEP = 100;
        private const string ADMIN_URL = "admin";
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IChapterRepository chapterRepository;

        public ChapterController(IUnitOfWorkFactory unitOfWorkFactory, IChapterRepository chapterRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.chapterRepository = chapterRepository;
        }

        #region Show chapters

        [HttpGet]
        public ActionResult ShowChapters()
        {
            var chapters = chapterRepository.FetchAllChapters();
            return View(chapters);
        }
        
        #endregion

        #region New chapter

        [HttpGet]
        public ActionResult NewChapter()
        {
            return View("ProcessChapter", new NewChapterForm());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewChapter(NewChapterForm form)
        {
            return ProcessChapter(form);
        }

        #endregion

        #region Edit article

        [HttpGet]
        public ActionResult EditChapter(int? id)
        {
            var idValue = ConvertParameter("id", id);

            var chapter = chapterRepository.FetchById(idValue);
            if (chapter == null)
            {
                throw new ContentNotFoundException("chapter", idValue);
            }

            var form = new EditChapterForm(chapter);
            return View("ProcessChapter", form);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditChapter(int id, EditChapterForm form)
        {
            return ProcessChapter(form, id);
        }

        #endregion
        
        private Chapter DoNewChapter(NewChapterForm form)
        {
            ValidateTagName(form.TagName);
            var newOrderValue = chapterRepository.GetMaxOrderValue() + ORDER_VALUE_STEP;

            return new Chapter(form.Title, newOrderValue, form.TagName,
                form.IsPublished, form.Content);
        }

        private Chapter DoEditChapter(EditChapterForm form, int id)
        {
            var chapter = chapterRepository.FetchById(id);

            // если метка изменилась
            var checkForUnique = !form.TagName.Equals(chapter.TagName, StringComparison.InvariantCultureIgnoreCase);
            ValidateTagName(form.TagName, checkForUnique);
            
            chapter.SetTitle(form.Title);
            chapter.SetIsPublished(form.IsPublished);
            chapter.SetUpdatedNow();
            chapter.SetContentData(form.Content);
            chapter.SetTagName(form.TagName);

            return chapter;
        }

        private void ValidateTagName(string tagName, bool checkForUnique = true)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("tagName", tagName);

            if (tagName.Equals(ADMIN_URL, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ApplicationException(
                    string.Format("Значение {0} не может использоваться как метка раздела."
                        + " Задайте другое значение метки.", tagName));
            }

            if (checkForUnique)
            {
                var isUsed = chapterRepository.HasChapterWithTagName(tagName);
                if (isUsed)
                {
                    throw new ApplicationException(
                        string.Format("Раздел с меткой {0} уже существует."
                            + " Задайте другое значение метки.", tagName));
                }
            }
        }

        private ActionResult ProcessChapter(NewChapterForm form, int? id = null)
        {
            if (ModelState.IsValid)
            {
                if (!form.HasContent())
                {
                    ModelState.AddModelError("", "Содержание не может быть пустым");
                    return View("ProcessChapter", form);
                }

                using (var unitOfWork = unitOfWorkFactory.Create())
                {
                    Chapter chapter;

                    try
                    {
                        chapter = form is EditChapterForm && id.HasValue
                                              ? DoEditChapter((EditChapterForm)form, id.Value)
                                              : DoNewChapter(form);
                    }
                    catch (ApplicationException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View("ProcessChapter", form);
                    }

                    if (!(form is EditChapterForm))
                    {
                        chapterRepository.Add(chapter);
                    }

                    unitOfWork.Commit();
                }

                return RedirectToAction("ShowChapters");
            }

            return View("ProcessChapter", form);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var chapter = chapterRepository.FetchById(id);
                if (chapter == null)
                {
                    throw new ContentNotFoundException("chapter", id);
                }

                chapter.IsDeleted = true;
                unitOfWork.Commit();

                AddFlashMessage(string.Format(@"Раздел ""{0}"" был удален", chapter.Title));

                return RedirectToAction("ShowChapters");
            }
        }

        [HttpPost]
        public JsonResult Sort(int[] itemIds)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var orderValue = 0;
                foreach (var id in itemIds)
                {
                    orderValue += ORDER_VALUE_STEP;
                    chapterRepository.UpdateOrder(id, orderValue);
                }

                unitOfWork.Commit();
            }

            return null;
        }

    }
}