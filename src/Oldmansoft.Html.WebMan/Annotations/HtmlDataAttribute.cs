using System;
using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 设置 Html data 属性
    /// </summary>
    public class HtmlDataAttribute : Attribute
    {
        /// <summary>
        /// 空内容
        /// </summary>
        public static readonly HtmlDataAttribute Empty = new HtmlDataAttribute(null);

        /// <summary>
        /// Html 属性
        /// </summary>
        private Dictionary<string, string> Datas { get; set; }

        /// <summary>
        /// 创建设置 Html data 属性
        /// </summary>
        /// <param name="properties"></param>
        public HtmlDataAttribute(params string[] properties)
        {
            Datas = new Dictionary<string, string>();
            if (properties == null) return;
            for (var i = 0; i < properties.Length; i += 2)
            {
                string value = null;
                if (properties.Length > i + 1) value = properties[i + 1];
                Datas[properties[i]] = value;
            }
        }

        /// <summary>
        /// 设置元素上下文
        /// </summary>
        /// <param name="element"></param>
        public void SetContext(IHtmlElement element)
        {
            foreach (var item in Datas)
            {
                element.Data(item.Key, item.Value);
            }
        }
    }
}
