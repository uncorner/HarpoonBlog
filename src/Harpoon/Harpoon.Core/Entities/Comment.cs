using System;

namespace Harpoon.Core.Entities
{
    public class Comment
    {
        public int Id { get; private set; }
        public int ArticleId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; set; }

        protected Comment()
        {
        }

        public Comment(string content)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("content", content);

            Content = content;
            CreatedAt = DateTime.Now;
        }

    }
}