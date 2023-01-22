using System;
using System.Linq;

namespace Harpoon.Core
{
    public class TagLineConverter
    {
        private readonly char separator;

        public TagLineConverter(char separator)
        {
            this.separator = separator;
        }

        public string[] ToTags(string tagLine)
        {
            if (string.IsNullOrEmpty(tagLine))
            {
                return new string[] {};
            }

            var line = tagLine.ToLowerInvariant().Trim();
            return line.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Distinct()
                .ToArray();
        }

        public string ToTagLine(string[] tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException("tags");
            }

            var orderedTags = tags.OrderBy(e => e);
            return string.Join(separator + " ", orderedTags);
        }
        
    }
}