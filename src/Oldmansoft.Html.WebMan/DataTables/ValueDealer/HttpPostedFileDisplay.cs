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
        public string Convert(object value, ModelItemInfo modelItem)
        {
            if (value is HttpPostedFileCustom)
            {
                return DealCustom(value as HttpPostedFileCustom);
            }
            else
            {
                return DealBase(value as System.Web.HttpPostedFileBase);
            }
        }

        public string DealCustom(HttpPostedFileCustom file)
        {
            var icon = ContentTypeMapping.Instance.ToIcon(file.ContentType, file.FileName);
            if (icon == FontAwesome.Picture_O)
            {
                var img = new HtmlElement(HtmlTag.Img);
                img.Attribute(HtmlAttribute.Src, file.Location);
                return new HtmlOutput(img).Complete();
            }
            else
            {
                var a = new HtmlElement(HtmlTag.A);
                a.AddClass("icon-fa-text");
                a.Attribute(HtmlAttribute.Href, file.Location);
                a.Attribute(HtmlAttribute.Target, "_blank");
                a.Text(file.FileName);
                return new HtmlOutput(icon.CreateElement(), a).Complete();
            }
        }

        public string DealBase(System.Web.HttpPostedFileBase file)
        {
            var icon = ContentTypeMapping.Instance.ToIcon(file.ContentType, file.FileName);
            var span = new HtmlElement(HtmlTag.Span);
            span.AddClass("icon-fa-text");
            span.Text(file.FileName);
            return new HtmlOutput(icon.CreateElement(), span).Complete();
        }
    }
}
