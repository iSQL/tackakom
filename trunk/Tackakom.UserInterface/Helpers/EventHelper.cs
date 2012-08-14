using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tackakom.Model;

namespace Tackakom.UserInterface.Helpers
{
    public static class EventHelper
    {
        public static MvcHtmlString Event(this HtmlHelper helper, int id,  Event _event, int count)
        {

            var article = new TagBuilder("article");
            article.AddCssClass("event" );
            article.MergeAttribute("id", id.ToString());
            article.MergeAttribute("onclick", "SelectEvent(" + id + ", " + count + ")");

            var image = new TagBuilder("img");
            image.MergeAttribute("src", _event.EventCategory.Icon);

            var h1 = new TagBuilder("h1");
            h1.SetInnerText(_event.Title);

            var p = new TagBuilder("p");
            p.SetInnerText(_event.Description);
            
            var span = new TagBuilder("span");
            span.InnerHtml=_event.Description;

            var emVreme = new TagBuilder("em");
            emVreme.AddCssClass("datum");
            emVreme.SetInnerText(_event.StartDate.ToShortDateString());

            var emOd = new TagBuilder("em");
            emOd.AddCssClass("poc");
            emOd.SetInnerText("Od "+_event.StartDate.ToShortTimeString());

            var emDo = new TagBuilder("em");
            emDo.AddCssClass("kraj");
            emDo.SetInnerText("Do " + _event.EndDate.ToShortTimeString());

            var host = new TagBuilder("a");
            host.MergeAttribute("href", "#");
            host.SetInnerText(_event.Host.Name);

            var entry = new TagBuilder("p");
            entry.SetInnerText(_event.Entry);


            span.InnerHtml = emVreme.ToString(TagRenderMode.Normal) + emOd.ToString(TagRenderMode.Normal) + emDo.ToString(TagRenderMode.Normal);

            var eventId = new TagBuilder("Input");
            eventId.MergeAttribute("type", "hidden");
            eventId.MergeAttribute("value", _event.Id.ToString());

            
            var stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append(article.ToString(TagRenderMode.StartTag));
            stringBuilder.Append(image.ToString(TagRenderMode.SelfClosing));
            stringBuilder.Append(h1.ToString(TagRenderMode.Normal));
            stringBuilder.Append(p.ToString(TagRenderMode.Normal));
            stringBuilder.Append(span.ToString(TagRenderMode.Normal));
            stringBuilder.Append(host.ToString(TagRenderMode.Normal));
            stringBuilder.Append(entry.ToString(TagRenderMode.Normal));
            stringBuilder.Append(eventId.ToString(TagRenderMode.Normal));
            stringBuilder.Append(article.ToString(TagRenderMode.EndTag));


            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}