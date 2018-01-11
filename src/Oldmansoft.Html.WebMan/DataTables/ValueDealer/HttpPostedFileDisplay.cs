using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Util;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class HttpPostedFileDisplay : IValueDisplay
    {
        public HtmlNode Convert(object value, ModelItemInfo modelItem)
        {
            if (value is HttpPostedFileCustom)
            {
                return DealCustom(value as HttpPostedFileCustom, modelItem);
            }
            else
            {
                return DealBase(value as System.Web.HttpPostedFileBase);
            }
        }

        private HtmlNode DealCustom(HttpPostedFileCustom file, ModelItemInfo modelItem)
        {
            var icon = ContentTypeMapping.Instance.ToIcon(file.ContentType, file.FileName);
            if (icon == FontAwesome.Picture_O)
            {
                var a = new HtmlElement(HtmlTag.A);
                a.Attribute(HtmlAttribute.Href, file.Location);
                a.Attribute(HtmlAttribute.Target, "_none");
                modelItem.HtmlData.SetContext(a);
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
                modelItem.HtmlData.SetContext(a);
                a.Text(file.FileName);
                return new HtmlNodeContainer(icon.CreateElement(), a);
            }
        }

        private HtmlNode DealBase(System.Web.HttpPostedFileBase file)
        {
            var icon = ContentTypeMapping.Instance.ToIcon(file.ContentType, file.FileName);
            var span = new HtmlElement(HtmlTag.Span);
            span.AddClass("icon-fa-text");
            span.Text(file.FileName);
            return new HtmlNodeContainer(icon.CreateElement(), span);
        }
    }
}
