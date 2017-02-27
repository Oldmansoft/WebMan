using Oldmansoft.Html.Util;
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
        private static IList<Col> ColValues { get; set; }

        /// <summary>
        /// 转换为样式名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToClassName(this Col source)
        {
            if (ColValues == null)
            {
                var list = new List<Col>();
                foreach (var item in Enum.GetValues(typeof(Col)))
                {
                    list.Add((Col)item);
                }
                ColValues = list;
            }

            var result = new StringBuilder();
            foreach (var item in ColValues)
            {
                if ((item & source) == item)
                {
                    if (result.Length > 0) result.Append(" ");
                    result.Append("col-");
                    result.Append(item.ToString().Substring(0, 2).ToLower());
                    result.Append("-");
                    result.Append(item.ToString().Substring(2));
                }
            }
            return result.ToString();
        }
        
        /// <summary>
        /// 创建元素
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static HtmlElement CreateElement(this LinkContent source)
        {
            var result = new HtmlElement(HtmlTag.A);

            var icon = new HtmlElement(HtmlTag.I).AddClass(string.Format("fa fa-{0}", source.Icon.ToString().ToLower().Replace("_", "-")));
            result.Append(source.Icon.CreateElement());

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
