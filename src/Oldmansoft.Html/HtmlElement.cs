using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 元素
    /// </summary>
    public class HtmlElement : HtmlNode, IHtmlElement
    {
        private static HashSet<HtmlTag> SelfClosedTags;

        private HtmlAttributeManager Attributes = new HtmlAttributeManager();

        private HtmlClassManager Classes = new HtmlClassManager();

        private HtmlStyleManager Csses = new HtmlStyleManager();

        private HtmlScriptManager Scripts = new HtmlScriptManager();

        /// <summary>
        /// 标签
        /// </summary>
        public HtmlTag Tag { get; protected set; }

        /// <summary>
        /// 创建元素
        /// </summary>
        /// <param name="tag"></param>
        public HtmlElement(HtmlTag tag)
        {
            Tag = tag;
        }

        private static bool IsSelfClosed(HtmlTag tag)
        {
            if (SelfClosedTags == null)
            {
                var tags = new HashSet<HtmlTag>();
                tags.Add(HtmlTag.Col);
                tags.Add(HtmlTag.Img);
                tags.Add(HtmlTag.Area);
                tags.Add(HtmlTag.Base);
                tags.Add(HtmlTag.Link);
                tags.Add(HtmlTag.Meta);
                tags.Add(HtmlTag.Frame);
                tags.Add(HtmlTag.Input);
                tags.Add(HtmlTag.Param);
                SelfClosedTags = tags;
            }
            return SelfClosedTags.Contains(tag);
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Attribute(HtmlAttribute name)
        {
            return Attributes.Attribute(name.ToString().ToLower());
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Attribute(string name)
        {
            return Attributes.Attribute(name);
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHtmlElement Attribute(HtmlAttribute name, string value)
        {
            Attributes.Attribute(name.ToString().ToLower(), value);
            return this;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHtmlElement Attribute(string name, string value)
        {
            Attributes.Attribute(name, value);
            return this;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IHtmlElement Attribute(object properties)
        {
            Attributes.Attribute(properties);
            return this;
        }

        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IHtmlElement RemoveAttribute(string name)
        {
            Attributes.RemoveAttribute(name);
            return this;
        }

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IHtmlElement AddClass(string name)
        {
            Classes.AddClass(name);
            return this;
        }

        /// <summary>
        /// 移除样式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IHtmlElement RemoveClass(string name)
        {
            Classes.RemoveClass(name);
            return this;
        }

        /// <summary>
        /// 获取样式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Css(string name)
        {
            return Csses.Css(name);
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHtmlElement Css(string name, string value)
        {
            Csses.Css(name, value);
            return this;
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IHtmlElement Css(object properties)
        {
            Csses.Css(properties);
            return this;
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual IHtmlElement Append(IHtmlNode node)
        {
            NodesAppend(node);
            return this;
        }

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual IHtmlElement Prepend(IHtmlNode node)
        {
            NodesPrepend(node);
            return this;
        }

        /// <summary>
        /// 元素后贴
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual IHtmlElement After(IHtmlNode node)
        {
            NodeAfter(node);
            return this;
        }

        /// <summary>
        /// 元素前贴
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual IHtmlElement Before(IHtmlNode node)
        {
            NodeBefore(node);
            return this;
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public virtual IHtmlElement Text(string text)
        {
            NodesClear();
            return Append(new HtmlText(text));
        }

        /// <summary>
        /// 客户端事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        public IHtmlElement OnClient(HtmlEvent e, string script)
        {
            Scripts.SetScript(string.Format("on{0}", e.ToString().ToLower()), script);
            return this;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            outer.Append(HtmlChar.SingleLeftAngleQuotation);
            outer.Append(Tag);
            Attributes.Format(outer);
            Classes.Format(outer);
            Csses.Format(outer);
            Scripts.Format(outer);
            if (NodesIsEmpty() && IsSelfClosed(Tag))
            {
                outer.Append(HtmlChar.Slashes);
                outer.Append(HtmlChar.SingleRightAngleQuotation);
                return;
            }

            outer.Append(HtmlChar.SingleRightAngleQuotation);
            foreach (var node in Children())
            {
                node.Format(outer);
            }
            if (HtmlOutput.DebugFormat && Children().Count() > 0)
            {
                outer.Append("\r\n");
            }
            outer.Append(HtmlChar.SingleLeftAngleQuotation);
            outer.Append(HtmlChar.Slashes);
            outer.Append(Tag);
            outer.Append(HtmlChar.SingleRightAngleQuotation);
        }
    }
}