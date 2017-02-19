using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Element
{
    /// <summary>
    /// 样式链接
    /// </summary>
    public class Link : HtmlElement
    {
        /// <summary>
        /// 设置和获取链接文档的位置
        /// </summary>
        public string Href
        {
            get
            {
                return Attribute(HtmlAttribute.Href);
            }
            set
            {
                Attribute(HtmlAttribute.Href, value);
            }
        }

        /// <summary>
        /// 设置和获取链接文档将被显示在什么设备上
        /// </summary>
        public string Media
        {
            get
            {
                return Attribute(HtmlAttribute.Media);
            }
            set
            {
                Attribute(HtmlAttribute.Media, value);
            }
        }

        /// <summary>
        /// 设置和获取当前文档与被链接文档之间的关系
        /// </summary>
        public string Rel
        {
            get
            {
                return Attribute(HtmlAttribute.Rel);
            }
            set
            {
                Attribute(HtmlAttribute.Rel, value);
            }
        }

        /// <summary>
        /// 设置和获取链接文档的 MIME 类型
        /// </summary>
        public string Type
        {
            get
            {
                return Attribute(HtmlAttribute.Type);
            }
            set
            {
                Attribute(HtmlAttribute.Type, value);
            }
        }

        /// <summary>
        /// 设置和获取完整性内容
        /// </summary>
        public string Integrity
        {
            get
            {
                return Attribute(HtmlAttribute.Integrity);
            }
            set
            {
                Attribute(HtmlAttribute.Integrity, value);
            }
        }

        /// <summary>
        /// 设置和获取同源资源
        /// </summary>
        public string CrossOrigin
        {
            get
            {
                return Attribute(HtmlAttribute.CrossOrigin);
            }
            set
            {
                Attribute(HtmlAttribute.CrossOrigin, value);
            }
        }

        /// <summary>
        /// 创建样式链接
        /// </summary>
        /// <param name="href">链接地址</param>
        public Link(string href)
            :base(HtmlTag.Link)
        {
            if (string.IsNullOrEmpty(href)) throw new ArgumentNullException("href");
            Attribute(HtmlAttribute.Rel, "stylesheet");
            Href = href;
        }
    }
}
