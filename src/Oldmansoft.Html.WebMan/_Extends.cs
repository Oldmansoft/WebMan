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
        private static IList<Column> ColumnValues { get; set; }

        private static IList<ColumnOffset> ColumnOffsetValues { get; set; }

        /// <summary>
        /// 获取样式名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetCssName(this Column source)
        {
            if (ColumnValues == null)
            {
                var list = new List<Column>();
                foreach (var item in Enum.GetValues(typeof(Column)))
                {
                    list.Add((Column)item);
                }
                ColumnValues = list;
            }

            var result = new StringBuilder();
            foreach (var item in ColumnValues)
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
        /// 获取样式名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetCssName(this ColumnOffset source)
        {
            if (ColumnOffsetValues == null)
            {
                var list = new List<ColumnOffset>();
                foreach (var item in Enum.GetValues(typeof(ColumnOffset)))
                {
                    list.Add((ColumnOffset)item);
                }
                ColumnOffsetValues = list;
            }

            var result = new StringBuilder();
            foreach (var item in ColumnOffsetValues)
            {
                if ((item & source) == item)
                {
                    if (result.Length > 0) result.Append(" ");
                    result.Append("col-");
                    result.Append(item.ToString().Substring(0, 2).ToLower());
                    result.Append("-offset-");
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

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="source"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static IHtmlElement AddClass(this IHtmlElement source, Column column)
        {
            source.AddClass(column.GetCssName());
            return source;
        }

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="source"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static IHtmlElement AddClass(this IHtmlElement source, ColumnOffset column)
        {
            source.AddClass(column.GetCssName());
            return source;
        }

        /// <summary>
        /// 移除样式
        /// </summary>
        /// <param name="source"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static IHtmlElement RemoveClass(this IHtmlElement source, Column column)
        {
            source.RemoveClass(column.GetCssName());
            return source;
        }

        /// <summary>
        /// 移除样式
        /// </summary>
        /// <param name="source"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static IHtmlElement RemoveClass(this IHtmlElement source, ColumnOffset column)
        {
            source.RemoveClass(column.GetCssName());
            return source;
        }

        /// <summary>
        /// 创建布局元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static GridOption CreateGrid(this IHtmlElement source, Column column = Column.Sm12)
        {
            var result = new GridOption(column);
            result.Append(source);
            return result;
        }
    }
}
