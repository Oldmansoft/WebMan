﻿using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 树形列表
    /// </summary>
    public class TreeList : IHtmlElement
    {
        private HtmlElement Element { get; set; }

        /// <summary>
        /// 创建树形列表
        /// </summary>
        public TreeList()
        {
            Element = new HtmlElement(HtmlTag.Ul);
            Element.AddClass("side-menu");
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item">项</param>
        /// <returns></returns>
        public TreeList Add(TreeListItem item)
        {
            Element.Append(item);
            return this;
        }
        
        IHtmlElement IHtmlElement.AddClass(string name)
        {
            return Element.AddClass(name);
        }

        IHtmlElement IHtmlElement.Append(IHtmlNode node)
        {
            return Element.Append(node);
        }
        
        string IHtmlElement.Attribute(HtmlAttribute name)
        {
            return Element.Attribute(name);
        }
        
        IHtmlElement IHtmlElement.Attribute(HtmlAttribute name, string value)
        {
            return Element.Attribute(name, value);
        }

        string IHtmlElement.Data(string name)
        {
            return Element.Data(name);
        }

        IHtmlElement IHtmlElement.Data(string name, string value)
        {
            return Element.Data(name, value);
        }
        
        IHtmlElement IHtmlElement.RemoveData(string name)
        {
            return Element.RemoveData(name);
        }

        IList<IHtmlNode> IHtmlNode.Children()
        {
            return Element.Children();
        }

        IHtmlElement IHtmlElement.Css(object properties)
        {
            return Element.Css(properties);
        }

        string IHtmlElement.Css(string name)
        {
            return Element.Css(name);
        }

        IHtmlElement IHtmlElement.Css(string name, string value)
        {
            return Element.Css(name, value);
        }

        void IHtmlNode.Format(IHtmlOutput outer)
        {
            (Element as IHtmlNode).Format(outer);
        }

        IHtmlNode IHtmlNode.Parent
        {
            get
            {
                return (Element as IHtmlNode).Parent;
            }
            set
            {
                (Element as IHtmlNode).Parent = value;
            }
        }

        IHtmlElement IHtmlElement.OnClient(HtmlEvent e, string script)
        {
            return Element.OnClient(e, script);
        }

        IHtmlElement IHtmlElement.Prepend(IHtmlNode node)
        {
            return Element.Prepend(node);
        }

        IHtmlElement IHtmlElement.After(IHtmlNode node)
        {
            return Element.After(node);
        }

        IHtmlElement IHtmlElement.Before(IHtmlNode node)
        {
            return Element.Before(node);
        }

        IHtmlElement IHtmlElement.RemoveAttribute(HtmlAttribute name)
        {
            return Element.RemoveAttribute(name);
        }

        IHtmlElement IHtmlElement.RemoveClass(string name)
        {
            return Element.RemoveClass(name);
        }

        IHtmlElement IHtmlElement.Text(string text)
        {
            return Element.Text(text);
        }
    }
}
