using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tackakom.UserInterface.Helpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, int id, string url, string urlLink, string alternateText)
        {   
            return Image(helper, id, url, urlLink, alternateText, null);
        }
        public static MvcHtmlString Image(this HtmlHelper helper, int id, string url, string urlLink, string alternateText, object htmlAttributes)
        {
            // Kreiranje generatora taga
            var builder = new TagBuilder("img");

            // Kreiranje unikatnog Id-a
            builder.GenerateId(id.ToString());

            // Add attributes
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alternateText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            string slika = builder.ToString(TagRenderMode.SelfClosing);

            builder = new TagBuilder("a");
            builder.MergeAttribute("href", urlLink);
            builder.InnerHtml = slika;
            string anchorHtml = builder.ToString(TagRenderMode.Normal);


            // Renderovaje taga
            return MvcHtmlString.Create(anchorHtml);
        }

    }
}