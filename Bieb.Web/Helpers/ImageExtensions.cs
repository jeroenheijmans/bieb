using System.Web;
using System.Web.Mvc;

namespace Bieb.Web.Helpers
{
    public static class ImageExtensions
    {
        public static IHtmlString ImageFlagFor(this HtmlHelper htmlHelper, string nationality)
        {
            var tagBuilder = new TagBuilder("img");
            
            tagBuilder.Attributes.Add("alt", nationality);
            tagBuilder.Attributes.Add("title", nationality);
            tagBuilder.Attributes.Add("class", "flag icon");

            var src = UrlHelper.GenerateContentUrl("~/Content/images/flags/" + nationality + "-icon.png", htmlHelper.ViewContext.HttpContext);
            tagBuilder.Attributes.Add("src", src);

            return new HtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}