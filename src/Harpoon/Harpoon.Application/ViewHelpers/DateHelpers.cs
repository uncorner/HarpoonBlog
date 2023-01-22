using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Harpoon.Application.ViewHelpers
{
    public static class DateHelpers
    {
        private static readonly IDictionary<int, string> monthMap;

        static DateHelpers()
        {
            monthMap = new Dictionary<int, string>
                             {
                                 {1, "января"}, {2, "февраля"}, {3, "марта"},
                                 {4, "апреля"}, {5, "мая"}, {6, "июня"},
                                 {7, "июля"}, {8, "августа"}, {9, "сентября"},
                                 {10, "октября"}, {11, "ноября"}, {12, "декабря"}
                             };
        }

        public static MvcHtmlString DateTimeHm(this HtmlHelper htmlHelper, DateTime dateTime)
        {
            var formatted = string.Format("{0} {1} {2:yyyy}, {2:HH:mm}",
                dateTime.Day, monthMap[dateTime.Month], dateTime);
            return new MvcHtmlString(formatted);
        }

        public static MvcHtmlString DateOnly(this HtmlHelper htmlHelper, DateTime dateTime)
        {
            var formatted = string.Format("{0} {1} {2:yyyy}",
                dateTime.Day, monthMap[dateTime.Month], dateTime);
            return new MvcHtmlString(formatted);
        }

        public static MvcHtmlString NumericDateOnly(this HtmlHelper htmlHelper, DateTime dateTime)
        {
            return new MvcHtmlString(dateTime.ToString("dd.MM.yyyy"));
        }

    }
}