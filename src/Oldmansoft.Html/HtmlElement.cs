﻿using Oldmansoft.Html.Util;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 元素
    /// </summary>
    public class HtmlElement : HtmlNode, IHtmlElement
    {
        private static HashSet<HtmlTag> SelfClosedTags;

        private readonly HtmlAttributeManager Attributes = new HtmlAttributeManager();

        private readonly HtmlClassManager Classes = new HtmlClassManager();

        private readonly HtmlStyleManager Csses = new HtmlStyleManager();

        private readonly HtmlScriptManager Scripts = new HtmlScriptManager();

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
                var tags = new HashSet<HtmlTag>
                {
                    HtmlTag.Col,
                    HtmlTag.Img,
                    HtmlTag.Area,
                    HtmlTag.Base,
                    HtmlTag.Link,
                    HtmlTag.Meta,
                    HtmlTag.Frame,
                    HtmlTag.Input,
                    HtmlTag.Param
                };
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
        /// 移除属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IHtmlElement RemoveAttribute(HtmlAttribute name)
        {
            Attributes.RemoveAttribute(name.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 获取数据属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Data(string name)
        {
            return Attributes.Attribute(string.Format("data-{0}", name));
        }

        /// <summary>
        /// 设置数据属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHtmlElement Data(string name, string value)
        {
            Attributes.Attribute(string.Format("data-{0}", name), value);
            return this;
        }

        /// <summary>
        /// 移除数据属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IHtmlElement RemoveData(string name)
        {
            Attributes.RemoveAttribute(string.Format("data-{0}", name));
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
            if (node == null) return this;
            if (node == this) throw new ArgumentException("不能添加自己", "node");
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
            if (node == null) return this;
            if (node == this) throw new ArgumentException("不能添加自己", "node");
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
            if (node == null) return this;
            if (node == this) throw new ArgumentException("不能添加自己", "node");
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
            if (node == null) return this;
            if (node == this) throw new ArgumentException("不能添加自己", "node");
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
            if (string.IsNullOrEmpty(text)) return this;
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