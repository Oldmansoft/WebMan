﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
