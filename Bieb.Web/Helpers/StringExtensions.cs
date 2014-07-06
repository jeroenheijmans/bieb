using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bieb.Web.Helpers
{
    public static class StringExtensions
    {
        public static IHtmlString ShowIfNotNull(this HtmlHelper htmlHelper, object item, string tagName)
        {
            var builder = new TagBuilder(tagName);
            builder.SetInnerText(item == null ? "" : item.ToString());
            return new HtmlString(builder.ToString());
        }
    }
}