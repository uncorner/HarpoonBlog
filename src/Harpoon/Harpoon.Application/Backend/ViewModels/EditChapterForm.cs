using System;
using Harpoon.Core.Entities;

namespace Harpoon.Application.Backend.ViewModels
{
    public class EditChapterForm : NewChapterForm
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int Id { get; set; }
        
        public EditChapterForm()
        {
        }

        public EditChapterForm(Chapter chapter)
        {
            Title = chapter.Title;
            TagName = chapter.TagName;
            IsPublished = chapter.IsPublished;
            Content = chapter.Content.Data;
            UpdatedAt = chapter.UpdatedAt;
            PublishedAt = chapter.PublishedAt;
            Id = chapter.Id;
            CreatedAt = chapter.CreatedAt;
        }

        public override string GetFormTitle()
        {
            return "Редактирование раздела";
        }

        public override string GetSubmitText()
        {
            return "Сохранить";
        }
        
    }
}