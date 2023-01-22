using System;
using System.Collections.Generic;
using Transliteration;

namespace Harpoon.Core.Entities
{
    public class Tag
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Article> Articles { get; private set; }

        private Tag()
        {
        }

        public Tag(string name)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("name", name);

            Name = name.Trim();
            Id = GenerateIdByName(Name);

            Articles = new List<Article>();
        }

        private string GenerateIdByName(string name)
        {
            var processedName = name.ToLowerInvariant().Replace(' ', '-');
            return Converter.Front(processedName, TransliterationType.Gost);
        }

    }
}
