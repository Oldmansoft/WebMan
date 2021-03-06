﻿using Oldmansoft.Html.WebMan.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extends
    {
        /// <summary>
        /// 获取表达式中的属性
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="source">表达式</param>
        /// <returns></returns>
        internal static PropertyInfo GetProperty<TEntity>(this Expression<Func<TEntity, object>> source)
        {
            var member = source.Body;
            if (member.NodeType == ExpressionType.Convert && source.Body is UnaryExpression)
            {
                member = ((UnaryExpression)member).Operand;
            }
            if (!(member is MemberExpression)) return null;
            return ((MemberExpression)member).Member as PropertyInfo;
        }

        /// <summary>
        /// 获取属性表达式的全名
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyFullName<TEntity>(this Expression<Func<TEntity, object>> expression)
        {
            var member = expression.Body;
            if (member.NodeType == ExpressionType.Convert && expression.Body is UnaryExpression)
            {
                member = ((UnaryExpression)member).Operand;
            }
            if (!(member is MemberExpression)) return null;

            var names = new List<string>();
            var memberExpression = (MemberExpression)member;
            names.Add(memberExpression.Member.Name);
            while (memberExpression.Expression.GetType().Name == "PropertyExpression")
            {
                memberExpression = (MemberExpression)memberExpression.Expression;
                names.Add(memberExpression.Member.Name);
            }
            names.Reverse();
            return string.Join(".", names);
        }

        /// <summary>
        /// 获取字符串列表
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
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

        private static readonly Util.ContentTypeFlags ContentTypeFlags = new Util.ContentTypeFlags();

        /// <summary>
        /// 生成数组
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ContentType[] ToArray(this ContentType source)
        {
            return ContentTypeFlags.From(source);
        }

        /// <summary>
        /// 是否在类型中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static bool In(this ContentType source, string contentType)
        {
            var header = source.ToString().ToLower().Replace('_', '-');
            return contentType.IndexOf(header) == 0;
        }

        /// <summary>
        /// 获取非 null 字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetNotNullString(this object source)
        {
            return source == null ? string.Empty : source.ToString();
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetString(this object source)
        {
            return source?.ToString();
        }

        /// <summary>
        /// 获取修复的脚本尾
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FixScriptTail(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return source;
            if (source.Substring(source.Length - 1, 1) == ";") return source;
            return string.Format("{0};", source);
        }

        private static readonly Util.ColumnFlags ColumnFlags = new Util.ColumnFlags();

        /// <summary>
        /// 获取样式名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetCssName(this Column source)
        {
            var result = new StringBuilder();
            foreach (var item in ColumnFlags.From(source))
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

        private static readonly Util.ColumnOffsetFlags ColumnOffsetFlags = new Util.ColumnOffsetFlags();

        /// <summary>
        /// 获取样式名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetCssName(this ColumnOffset source)
        {
            var result = new StringBuilder();
            foreach (var item in ColumnOffsetFlags.From(source))
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

        /// <summary>
        /// 获取目标内容
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static string GetTarget(this LinkBehave source)
        {
            switch (source)
            {
                case LinkBehave.Open:
                    return "_open";
                case LinkBehave.Call:
                    return "_call";
                case LinkBehave.Self:
                    return "_self";
                case LinkBehave.Blank:
                    return "_blank";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 设置目标属性
        /// </summary>
        /// <param name="source"></param>
        /// <param name="element"></param>
        internal static void SetTargetAttribute(this LinkBehave source, IHtmlElement element)
        {
            var target = source.GetTarget();
            if (target == null) return;
            element.Attribute(HtmlAttribute.Target, target);
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
        /// <param name="e"></param>
        /// <param name="script"></param>
        public static void AddEvent(this IHtmlOutput source, AppEvent e, string script)
        {
            if (string.IsNullOrWhiteSpace(script)) return;
            InitEventContent(source);
            if (!source.Items.ContainsKey(e))
            {
                source.Items.Add(e, new List<string>());
            }
            var list = source.Items[e] as List<string>;
            if (script.Last() == ';')
            {
                list.Add(script);
            }
            else
            {
                list.Add(string.Format("{0};", script));
            }
        }

        /// <summary>
        /// 添加事件脚本
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <param name="script"></param>
        public static void AddEvent(this IHtmlOutput source, AppEvent e, Element.Script script)
        {
            if (script == null) return;
            if (!script.HasContent()) return;
            InitEventContent(source);
            if (!source.Items.ContainsKey(e))
            {
                source.Items.Add(e, new List<string>());
            }
            var list = source.Items[e] as List<string>;
            script.SetListFromContent(list);
        }

        /// <summary>
        /// 设置快速搜索栏
        /// </summary>
        /// <param name="source">输出组件</param>
        /// <param name="location">位置</param>
        /// <param name="key">参数键</param>
        /// <param name="placeholder">占位符</param>
        public static void SetQuickSearch(this IHtmlOutput source, ILocation location, string key = "key", string placeholder = null)
        {
            var script = string.Format(
                "oldmansoft.webman.search.on({{ action: '{0}', target: '{1}', name: '{2}', placeholder: '{3}' }})",
                location.Path.JavaScriptEncode(),
                location.Behave.GetTarget(),
                key.JavaScriptEncode(),
                string.IsNullOrEmpty(placeholder) ? location.Display.JavaScriptEncode() : placeholder.JavaScriptEncode()
            );
            source.AddEvent(AppEvent.Active, script);
        }

        private static void InitEventContent(IHtmlOutput source)
        {
            if (source.Items.ContainsKey(AppEvent.Load)) return;
            if (source.Items.ContainsKey(AppEvent.Active)) return;
            if (source.Items.ContainsKey(AppEvent.Inactive)) return;
            if (source.Items.ContainsKey(AppEvent.Unload)) return;

            source.OnCompleted += (outer) =>
            {
                outer.Append("<script>");
                outer.Append("$app.event()");
                AddAppEventContent(outer, AppEvent.Load);
                AddAppEventContent(outer, AppEvent.Active);
                AddAppEventContent(outer, AppEvent.Inactive);
                AddAppEventContent(outer, AppEvent.Unload);
                outer.Append(";</script>");
            };
        }

        private static void AddAppEventContent(IHtmlOutput outer, AppEvent e)
        {
            if (!outer.Items.ContainsKey(e)) return;

            outer.Append(".on");
            outer.Append(e.ToString());
            outer.Append("(function (view) {");
            outer.Append("\r\n");
            foreach (var item in outer.Items[e] as List<string>)
            {
                outer.Append(item);
                outer.Append("\r\n");
            }
            outer.Append("})");
        }

        /// <summary>
        /// 转换为空间容易内容
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToSpaceVolumeString(this uint source)
        {
            if (source < 1024)
            {
                return string.Format("{0} 字节", source);
            }
            if (source < 1024 * 1024)
            {
                return string.Format("{0} KB", source / 1024);
            }

            return string.Format("{0} MB", source / 1024 / 1024);
        }
    }
}