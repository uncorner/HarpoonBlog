using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;

namespace Harpoon.Application
{
    public class ControllerBase : Controller
    {
        public const string FLASH_MESSAGE_KEY = "_FLASH_MESSAGE";
        private IList<string> flashMessages;

        protected void AddFlashMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }

            if (flashMessages == null)
            {
                flashMessages = new List<string>();
            }

            flashMessages.Add(message);

            TempData[FLASH_MESSAGE_KEY] = flashMessages;
        }

        protected void CheckParameter(string paramName, string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ActionParameterNullException(paramName);
            }
        }

        protected int ConvertParameter(string paramName, int? param)
        {
            if (!param.HasValue)
            {
                throw new ActionParameterNullException(paramName);
            }

            return param.Value;
        }

        protected string RenderPartialView(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        
    }
} 