using System;
using System.Linq;
using Harpoon.Core.Entities;

namespace Harpoon.Application.Backend.ViewModels
{
    public class EditArticleForm : NewArticleForm
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int Id { get; set; }
        public int CommentCount { get; set; }
        
        public bool HasComments
        {
            get { return CommentCount > 0; }
        }
         
        public EditArticleForm()
        {
        }

        public EditArticleForm(Article article)
        {
            Title = article.Title;
            IsPublished = article.IsPublished;
            Content = article.Content.Data;
            UpdatedAt = article.UpdatedAt;
            PublishedAt = article.PublishedAt;
            Id = article.Id;
            CommentCount = article.Comments.Count;

            CreatedAt = article.CreatedAt;

            var tagNames = article.Tags
                .Select(e => e.Name)
                .ToArray();

            TagLine = CreateConverter().ToTagLine(tagNames);
        }

        public override string GetFormTitle()
        {
            return "Редактирование заметки";
        }

        public override string GetSubmitText()
        {
            return "Сохранить";
        }
        
    }
}