using Microsoft.AspNetCore.Http;
using Oldmansoft.Html.WebMan.Util;
using System;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class FileLocationDisplay : IValueDisplay
    {
        public Type DealType => typeof(IFormFile);

        public HtmlNode Convert(object value, ModelPropertyContent propertyContent)
        {
            if (value is FileLocation)
            {
                return DealCustom(value as FileLocation, propertyContent);
            }
            else
            {
                return DealBase(value as IFormFile);
            }
        }

        private HtmlNode DealCustom(FileLocation file, ModelPropertyContent propertyContent)
        {
            var icon = ContentTypeMapping.Instance.ToIcon(file.ContentType, file.FileName);
            if (icon == FontAwesome.Picture_O)
            {
                var a = new HtmlElement(HtmlTag.A);
                a.Attribute(HtmlAttribute.Href, file.Location);
                a.Attribute(HtmlAttribute.Target, "_none");
                propertyContent.Attributes.Get<Annotations.HtmlDataAttribute>().SetContext(a);
                var img = new HtmlElement(HtmlTag.Img);
                img.Attribute(HtmlAttribute.Src, file.Location);
                img.AppendTo(a);
                return a;
            }
            else
            {
                var a = new HtmlElement(HtmlTag.A);
                a.AddClass("icon-fa-text");
                a.Attribute(HtmlAttribute.Href, file.Location);
                a.Attribute(HtmlAttribute.Target, "_none");
                propertyContent.Attributes.Get<Annotations.HtmlDataAttribute>().SetContext(a);
                a.Text(file.FileName);
                return new HtmlNodeContainer(icon.CreateElement(), a);
            }
        }

        private HtmlNode DealBase(IFormFile file)
        {
            var icon = ContentTypeMapping.Instance.ToIcon(file.ContentType, file.FileName);
            var span = new HtmlElement(HtmlTag.Span);
            span.AddClass("icon-fa-text");
            span.Text(file.FileName);
            return new HtmlNodeContainer(icon.CreateElement(), span);
        }
    }
}
