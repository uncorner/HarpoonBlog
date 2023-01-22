using System.Text.RegularExpressions;

namespace Harpoon.Application
{
    public static class PreviewProcessor
    {
        public static string GetFirstParagraph(string htmlText)
        {
            if (string.IsNullOrWhiteSpace(htmlText))
            {
                return string.Empty;
            }

            const string pattern = @"<\s*p\s*>(.*?)<\s*/\s*p\s*>";
            
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            var match = regex.Match(htmlText);

            while (match.Success)
            {
                var innerText = match.Groups[1].Value;

                if (innerText.Trim() != string.Empty)
                {
                    return string.Format(@"<p>{0}</p>", innerText);
                }

                match = match.NextMatch();
            }

            return htmlText;
        }
        
    }
}