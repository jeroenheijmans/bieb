using System.Web;
using System.Web.Mvc;
using Bieb.Domain.Entities;

namespace Bieb.Web.Helpers
{
    public static class ImageExtensions
    {
        // TODO: Need to wrap VirtualPathUtility in an interface/class to be able to mock it?

        public static IHtmlString ImageFlagFor(this HtmlHelper htmlHelper, string Nationality)
        {
            var tagBuilder = new TagBuilder("img");
            tagBuilder.Attributes.Add("alt", Nationality);
            tagBuilder.Attributes.Add("title", Nationality);
            tagBuilder.Attributes.Add("src", VirtualPathUtility.ToAbsolute("~/Content/images/flags/" + Nationality + "-icon.png"));
            tagBuilder.Attributes.Add("class", "flag icon");
            return new HtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}