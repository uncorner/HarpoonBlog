using System;

namespace Harpoon.Core.Entities
{
    public abstract class Post
    {
        protected string title;
        
        public int Id { get; private set; }
        public int ContentId { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? PublishedAt { get; private set; }
        public PostContent Content { get; private set; }
        public bool IsDeleted { get; set; }
        
        protected Post()
        {
        }

        protected Post(string title, bool isPublished, string contentData)
        {
            SetTitle(title);
            SetIsPublished(isPublished);

            Content = new PostContent(contentData);
            CreatedAt = DateTime.Now;
        }

        public void SetTitle(string title)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("title", title);
            this.title = title;
        }

        public void SetIsPublished(bool isPublished)
        {
            IsPublished = isPublished;

            if (IsPublished && ! PublishedAt.HasValue)
            {
                PublishedAt = DateTime.Now;
            }
        }

        public void SetUpdatedNow()
        {
            UpdatedAt = DateTime.Now;
        }

        public void SetContentData(string data)
        {
            Content.SetData(data);
        }

        public void SetContent(PostContent content)
        {
            ArgumentHelper.EnsureNotNull("content", content);
            Content = content;
        }
        
    }
}