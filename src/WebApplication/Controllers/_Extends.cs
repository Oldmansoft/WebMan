using Oldmansoft.Html;
using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers
{
    static class _Extends
    {

        /// <summary>
        /// 添加查找面板
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="location"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="placeHolder"></param>
        public static void AddSearchPanel<TModel>(this DynamicTable<TModel> source, ILocation location, string key, string value, string placeHolder = null) where TModel : class
        {
            if (source == null) return;
            if (location == null) throw new ArgumentNullException("location");
            if (key == null) throw new ArgumentNullException("key");

            var connector = "?";
            if (location.Path.IndexOf("?") > -1) connector = "&";
            var script = string.Format("$app.same('{0}{1}{2}=' + encodeURIComponent($.trim($(this).parent().parent().find('input[name={2}]').val())))", location.Path, connector, key);

            var form = new HtmlElement(HtmlTag.Form);
            form.OnClient(HtmlEvent.Submit, string.Format("$app.same('{0}{1}{2}=' + encodeURIComponent($.trim($(this).find('input[name={2}]').val()))); return false;", location.Path, connector, key));
            form.PrependTo(source);

            var search = new HtmlElement(HtmlTag.Div);
            search.AddClass("form-group");
            search.AppendTo(form);

            var group = new HtmlElement(HtmlTag.Div);
            group.AppendTo(search);
            group.AddClass(Column.Sm5);
            group.AddClass("input-group");
            var input = new HtmlElement(HtmlTag.Input);
            input.AppendTo(group);
            input.Attribute(HtmlAttribute.Name, key);
            input.Attribute(HtmlAttribute.Value, value);
            input.Attribute(HtmlAttribute.PlaceHolder, placeHolder);
            input.AddClass("form-control");
            var span = new HtmlElement(HtmlTag.Span);
            span.AddClass("input-group-addon");
            span.AppendTo(group);
            var button = new HtmlElement(HtmlTag.Input);
            button.AppendTo(span);
            button.Attribute(HtmlAttribute.Value, "查找");
            button.Attribute(HtmlAttribute.Type, "submit");
        }
    }
}