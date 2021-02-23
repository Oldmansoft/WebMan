using System;
using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan.Util
{
    /// <summary>
    /// 内容类型映射
    /// </summary>
    public class ContentTypeMapping
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static readonly ContentTypeMapping Instance = new ContentTypeMapping();

        private Dictionary<string, FontAwesome> Store { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        private ContentTypeMapping()
        {
            Store = new Dictionary<string, FontAwesome>(StringComparer.OrdinalIgnoreCase);
            Store.Add("video", FontAwesome.Film);
            Store.Add("audio", FontAwesome.Music);
            Store.Add("text", FontAwesome.Book);
            Store.Add("application", FontAwesome.Cogs);
            Store.Add("image", FontAwesome.Picture_O);
        }

        /// <summary>
        /// 转换成图标
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FontAwesome ToIcon(string contentType, string fileName)
        {
            var key = contentType;
            if (!string.IsNullOrEmpty(key))
            {
                key = key.Split('/')[0];
            }
            var extensionName = fileName;
            if (!string.IsNullOrEmpty(extensionName))
            {
                extensionName = System.IO.Path.GetExtension(extensionName).ToLower();
            }
            if (key == "application" && extensionName != ".exe")
            {
                return FontAwesome.Warning;
            }

            if (key != null && Store.ContainsKey(key))
            {
                return Store[key];
            }

            return FontAwesome.Question_Circle;
        }
    }
}
