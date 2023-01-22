using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Linq;

namespace Harpoon.Application.ViewHelpers
{
    public static class ExtLabelForHelper
    {

        public static MvcHtmlString ExtLabelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, bool? isRequired = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName
                ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            bool isRequiredFlag = isRequired.HasValue ? isRequired.Value : metadata.IsRequired;
            if (isRequiredFlag)
            {
                tag.InnerHtml = String.Format(@"{0} <span class=""required-mark"">*</span>", labelText);
            }
            else
            {
                tag.SetInnerText(labelText);
            }

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
        
    }
}