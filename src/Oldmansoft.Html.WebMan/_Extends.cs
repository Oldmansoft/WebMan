using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extends
    {
        /// <summary>
        /// 创建链接
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static HtmlElement Create(this LinkContent source)
        {
            var result = new HtmlElement(HtmlTag.A);

            var icon = new HtmlElement(HtmlTag.I).AddClass(string.Format("fa fa-{0}", source.Icon.ToString().ToLower().Replace("_", "-")));
            result.Append(icon);

            if (!string.IsNullOrEmpty(source.Text))
            {
                var span = new HtmlElement(HtmlTag.Span).Text(source.Text);
                result.Append(span);
            }

            if (!string.IsNullOrEmpty(source.Path)) result.Attribute(HtmlAttribute.Href, source.Path);
            return result;
        }
    }
}
