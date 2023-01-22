using System.Collections.Generic;

namespace Harpoon.Core.Entities
{
    public class Article : Post
    {
        public int PreviewContentId { get; private set; }
        public PostContent PreviewContent { get; private set; }
        public ICollection<Tag> Tags { get; private set; }
        public ICollection<Comment> Comments { get; private set; }

        private Article()
        {
        }

        public Article(string title, bool isPublished, string contentData, string previewContentData)
            : base(title, isPublished, contentData)
        {
            Tags = new List<Tag>();
            Comments = new List<Comment>();
            PreviewContent = new PostContent(previewContentData);
        }

        public string Title
        {
            get { return title; }
            private set { title = value; }
        }

        public void AddTag(Tag tag)
        {
            if (Tags.Contains(tag))
            {
                return;
            }

            Tags.Add(tag);
        }

        public void ClearTags()
        {
            Tags.Clear();
        }
        
        public bool HasTags()
        {
            return Tags.Count > 0;
        }

        public void AddComment(Comment comment)
        {
            if (Comments.Contains(comment))
            {
                return;
            }

            Comments.Add(comment);
        }

        public bool HasComments()
        {
            return Comments.Count > 0;
        }
        
        public void SetPreviewContentData(string data)
        {
            PreviewContent.SetData(data);
        }

        public void SetPreviewContent(PostContent previewContent)
        {
            ArgumentHelper.EnsureNotNull("previewContent", previewContent);
            PreviewContent = previewContent;
        }

    }
}
