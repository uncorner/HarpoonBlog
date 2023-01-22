using System.Text.RegularExpressions;

namespace Harpoon.Application
{
    public static class HtmlCleaner
    {
        private static readonly Regex htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string RemoveTags(string source)
        {
            return htmlRegex.Replace(source, string.Empty);
        }
    }
}