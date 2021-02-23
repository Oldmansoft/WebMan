using System;

namespace Oldmansoft.Html.Element
{
    /// <summary>
    /// 外部脚本文件
    /// </summary>
    public class ScriptResource : HtmlElement
    {
        /// <summary>
        /// 设置和获取异步执行脚本
        /// </summary>
        public string Async
        {
            get
            {
                return Attribute(HtmlAttribute.Async);
            }
            set
            {
                Attribute(HtmlAttribute.Async, value);
            }
        }

        /// <summary>
        /// 设置和获取外部脚本文件中使用的字符编码
        /// </summary>
        public string Charset
        {
            get
            {
                return Attribute(HtmlAttribute.Charset);
            }
            set
            {
                Attribute(HtmlAttribute.Charset, value);
            }
        }

        /// <summary>
        /// 设置和获取外部脚本文件的 URL
        /// </summary>
        public string Src
        {
            get
            {
                return Attribute(HtmlAttribute.Src);
            }
            set
            {
                Attribute(HtmlAttribute.Src, value);
            }
        }

        /// <summary>
        /// 设置和获取脚本的 MIME 类型
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
        /// 创建外部脚本文件
        /// </summary>
        /// <param name="src"></param>
        public ScriptResource(string src)
            : base(HtmlTag.Script)
        {
            if (string.IsNullOrEmpty(src)) throw new ArgumentNullException("src");
            Src = src;
        }
    }
}
