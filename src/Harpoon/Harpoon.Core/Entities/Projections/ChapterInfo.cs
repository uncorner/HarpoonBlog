using System;

namespace Harpoon.Core.Entities.Projections
{
    public class ChapterInfo
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string TagName { get; private set; }
        public int OrderValue { get; private set; }
    }
}