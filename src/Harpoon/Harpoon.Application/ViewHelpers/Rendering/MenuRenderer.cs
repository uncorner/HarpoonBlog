using System.Web.Mvc;
using Harpoon.Application.ViewModels.Navigation;

namespace Harpoon.Application.ViewHelpers.Rendering
{
    public class MenuRenderer : ListMenuRenderer
    {
        public MenuRenderer(HtmlHelper htmlHelper, Menu menu) : base(htmlHelper, menu)
        {
        }
        
        protected override string RenderTopOfList()
        {
            return string.Format(@"<ul id=""{0}"" class=""tuned-ul"">", menu.Id);
        }

        protected override string RenderBottomOfList()
        {
            return "</ul>";
        }

    }
}