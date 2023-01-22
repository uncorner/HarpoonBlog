using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace Harpoon.Application.ViewModels
{
    public class RssActionResult : ActionResult
    {
        private readonly SyndicationFeed rssFeed;

        public RssActionResult(SyndicationFeed rssFeed)
        {
            this.rssFeed = rssFeed;
        }

        #region Overrides of ActionResult

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";
            var rssFormatter = new Rss20FeedFormatter(rssFeed);

            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                rssFormatter.WriteTo(writer);
            }
        }

        #endregion
    }
}