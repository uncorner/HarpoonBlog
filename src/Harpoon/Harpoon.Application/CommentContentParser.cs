using CodeKicker.BBCode;

namespace Harpoon.Application
{
    public static class CommentContentParser
    {
        private static readonly BBCodeParser bbParser;

        static CommentContentParser()
        {
            bbParser = new BBCodeParser(new[]
                {
                    new BBTag("b", "<b>", "</b>"), 
                    new BBTag("i", "<span style=\"font-style:italic;\">", "</span>"), 
                    new BBTag("code", "<pre class=\"prettyprint\">", "</pre>"), 
                    new BBTag("quote", "<blockquote>", "</blockquote>"), 
                    new BBTag("url", "<a href=\"${href}\">", "</a>", new BBAttribute("href", ""),
                        new BBAttribute("href", "href")), 
                });
        }

        public static string Parse(string content, int maxLength = 2000)
        {
            var processed = bbParser.ToHtml(content).Replace("\n", "<br/>");

            if (processed.Length > maxLength)
            {
                processed = processed.Substring(0, maxLength);
            }

            return processed;
        }
        
    }
}