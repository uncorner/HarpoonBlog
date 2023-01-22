using System;
using Harpoon.Core;

namespace Harpoon.Application.Frontend.ViewModels
{
    public class FoundArticle
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime? PublishedAt { get; set; }

        public FoundArticle(int id, string title, string content)
        {
            ArgumentHelper.EnsureNotNull("title", title);
            ArgumentHelper.EnsureNotNull("content", content);

            Id = id;
            Title = title;
            Content = content;
        }
        
    }
}