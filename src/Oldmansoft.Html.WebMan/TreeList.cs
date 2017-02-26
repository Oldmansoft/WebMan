using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="branch">分支</param>
        /// <returns></returns>
        public TreeList Add(TreeListBranch branch)
        {
            Element.Append(branch);
            return this;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="leaf">叶子</param>
        /// <returns></returns>
        public TreeList Add(TreeListLeaf leaf)
        {
            Element.Append(leaf);
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

        IHtmlElement IHtmlElement.Attribute(object properties)
        {
            return Element.Attribute(properties);
        }

        string IHtmlElement.Attribute(string name)
        {
            return Element.Attribute(name);
        }

        string IHtmlElement.Attribute(HtmlAttribute name)
        {
            return Element.Attribute(name);
        }

        IHtmlElement IHtmlElement.Attribute(string name, string value)
        {
            return Element.Attribute(name, value);
        }

        IHtmlElement IHtmlElement.Attribute(HtmlAttribute name, string value)
        {
            return Element.Attribute(name, value);
        }

        IEnumerable<IHtmlNode> IHtmlNode.Children()
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

        IHtmlElement IHtmlElement.OnClient(HtmlEvent e, string script)
        {
            return Element.OnClient(e, script);
        }

        IHtmlElement IHtmlElement.Prepend(IHtmlNode node)
        {
            return Element.Prepend(node);
        }

        IHtmlElement IHtmlElement.RemoveAttribute(string name)
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
