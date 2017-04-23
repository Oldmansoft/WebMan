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
    }
}
