using System;

namespace Harpoon.Core.Entities
{
    public class Chapter : Post
    {
        public string TagName { get; private set; }
        public int OrderValue { get; private set; }

        private Chapter()
        {
        }

        public Chapter(string title, int orderValue, string tagName, bool isPublished, string contentData)
            : base(title, isPublished, contentData)
        {
            SetOrderValue(orderValue);
            SetTagName(tagName);
        }

        public string Title
        {
            get { return title; }
            private set { title = value; }
        }

        public void SetTagName(string tagName)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("tagName", tagName);
            TagName = tagName.Trim().ToLowerInvariant();
        }

        public void SetOrderValue(int orderValue)
        {
            OrderValue = orderValue;
        }

    }
}