using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// 将属性生成字符串键值对
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ToKeyValues(this object source)
        {
            var result = new Dictionary<string, string>();
            if (source == null) return result;
            if (source is IDictionary<string, string>) return source as IDictionary<string, string>;

            foreach (var property in source.GetType().GetProperties())
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
    }
}
