using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// 获取字符串键值对
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetKeyValues(this object source)
        {
            var result = new Dictionary<string, string>();
            if (source == null) return result;
            if (source is IDictionary<string, string>) return source as IDictionary<string, string>;

            foreach (var property in source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = property.GetValue(source);
                result.Add(property.Name, value == null ? string.Empty : value.ToString());
            }
            return result;
        }

        /// <summary>
        /// Html 编码字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string source)
        {
            if (source == null) return null;
            return HttpUtility.HtmlEncode(source);
        }

        /// <summary>
        /// JavaScript 编码字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string JavaScriptEncode(this string source)
        {
            if (source == null) return null;
            return HttpUtility.JavaScriptStringEncode(source);
        }

        /// <summary>
        /// 将 URL 转换为在请求客户端可用的 URL
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ResolveUrl(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            if (source.Length == 1) return source;
            if (source[0] != '~') return source;
            if (source[1] != '/') return source;

            if (source.Length < 3)
            {
                return HttpRuntime.AppDomainAppVirtualPath;
            }
            else
            {
                return HttpRuntime.AppDomainAppVirtualPath + source.Substring(2);
            }
        }

        /// <summary>
        /// 是否有子节点
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool HasChild(this IHtmlNode source)
        {
            if (source == null) return false;
            return source.GetNodes().Count > 0;
        }

        /// <summary>
        /// 添加到另一元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IHtmlElement AppendTo(this IHtmlElement source, IHtmlElement element)
        {
            if (element == null) return source;
            if (source == null) return source;
            element.Append(source);
            return source;
        }

        /// <summary>
        /// 插入到另一元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IHtmlElement PrependTo(this IHtmlElement source, IHtmlElement element)
        {
            if (element == null) return source;
            if (source == null) return source;
            element.Prepend(source);
            return source;
        }

        /// <summary>
        /// 元素后贴到另一元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IHtmlElement InsertAfter(this IHtmlElement source, IHtmlElement element)
        {
            if (element == null) return source;
            if (source == null) return source;
            element.After(source);
            return source;
        }

        /// <summary>
        /// 元素前贴到另一元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IHtmlElement InsertBefore(this IHtmlElement source, IHtmlElement element)
        {
            if (element == null) return source;
            if (source == null) return source;
            element.Before(source);
            return source;
        }

        /// <summary>
        /// 查找元素下所有符合标签的
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static IHtmlElementEnumerable Find(this IHtmlElement source, HtmlTag tag)
        {
            var result = new HtmlElementSeletor();
            if (source == null) return result;
            FindChildren(source, tag, result);
            return result;
        }

        private static void FindChildren(IHtmlElement source, HtmlTag tag, HtmlElementSeletor result)
        {
            foreach (var item in source.Children())
            {
                if (!(item is HtmlElement)) continue;
                var element = item as HtmlElement;
                if (element.Tag == tag) result.AddElement(element);
                FindChildren(element, tag, result);
            }
        }
    }
}
