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
        internal static IList<string> GetListString(this object source)
        {
            var result = new List<string>();
            if (source == null) return result;
            foreach (var item in source as System.Collections.IEnumerable)
            {
                if (item == null) continue;
                result.Add(item.ToString());
            }
            return result;
        }

        /// <summary>
        /// 获取非 null 字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetString(this object source)
        {
            return source == null ? string.Empty : source.ToString();
        }
        
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
        public static HtmlElement CreateElement(this FontAwesome source)
        {
            var result = new HtmlElement(HtmlTag.I);
            result.AddClass(string.Format("fa fa-{0}", source.ToString().ToLower().Replace("_", "-")));
            return result;
        }

        internal static void SetTargetAttribute(this LinkBehave source, IHtmlElement element)
        {
            switch (source)
            {
                case LinkBehave.Open:
                    element.Attribute(HtmlAttribute.Target, "_open");
                    break;
                case LinkBehave.Call:
                    element.Attribute(HtmlAttribute.Target, "_call");
                    break;
                case LinkBehave.Self:
                    element.Attribute(HtmlAttribute.Target, "_self");
                    break;
                case LinkBehave.Blank:
                    element.Attribute(HtmlAttribute.Target, "_blank");
                    break;
            }
        }

        /// <summary>
        /// 创建元素
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static HtmlElement CreateElement(this ILocation source)
        {
            var result = new HtmlElement(HtmlTag.A);
            result.Append(source.Icon.CreateElement());

            if (!string.IsNullOrEmpty(source.Display))
            {
                var span = new HtmlElement(HtmlTag.Span).Text(source.Display);
                result.Append(span);
            }

            if (source.Path != null) result.Attribute(HtmlAttribute.Href, source.Path);
            source.Behave.SetTargetAttribute(result);
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

        /// <summary>
        /// 获取序号生成名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetGeneratorName(this IGenerator<int> source)
        {
            return string.Format("Generator-{0}", source.Next());
        }

        /// <summary>
        /// 添加事件脚本
        /// </summary>
        /// <param name="source"></param>
        /// <param name="script"></param>
        public static void AddEvent(this IHtmlOutput source, string script)
        {
            source.Items.Add(script);
            if (source.OnCompleted != null) return;

            source.OnCompleted += (outer) =>
            {
                if (outer.Items.Count == 0) return;

                outer.Append("<script>$app.event().onLoad(function (view) {");
                outer.Append("\r\n");
                foreach (var item in outer.Items)
                {
                    outer.Append(item);
                    outer.Append("\r\n");
                }
                outer.Append("});</script>");
            };
        }

        /// <summary>
        /// 处理上传
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="upload"></param>
        /// <param name="delete"></param>
        /// <param name="expression"></param>
        public static void DealUpload<TModel>(this TModel source, Action<System.Web.HttpPostedFileBase> upload, Action delete, System.Linq.Expressions.Expression<Func<TModel, System.Web.HttpPostedFileBase>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            if (source == null) return;
            var httpPostedFile = expression.Compile().Invoke(source);

            var isDelete = System.Web.HttpContext.Current.Request.Form[string.Format("{0}_DeleteMark", expression.GetProperty().Name)] == "1";

            if (isDelete && delete != null) delete();

            if (httpPostedFile == null || httpPostedFile.ContentLength == 0) return;
            if (upload != null) upload(httpPostedFile);
        }

        /// <summary>
        /// 处理上传
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="upload"></param>
        /// <param name="expression"></param>
        public static void DealUpload<TModel>(this TModel source, Action<System.Web.HttpPostedFileBase> upload, System.Linq.Expressions.Expression<Func<TModel, System.Web.HttpPostedFileBase>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            DealUpload(source, upload, null, expression);
        }
    }
}