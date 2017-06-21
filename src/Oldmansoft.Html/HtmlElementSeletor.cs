using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 网页元素选择器
    /// </summary>
    class HtmlElementSeletor : IHtmlElement
    {
        private IList<IHtmlElement> Elements { get; set; }

        private IHtmlNode _Parent;

        public HtmlElementSeletor()
        {
            Elements = new List<IHtmlElement>();
            _Parent = new EmptyNode();
        }

        internal void AddElement(IHtmlElement element)
        {
            Elements.Add(element);
        }

        IHtmlNode IHtmlNode.Parent
        {
            get
            {
                return Elements.Count == 0 ? _Parent : Elements[0].Parent;
            }
            set
            {
                if (Elements.Count > 0) Elements[0].Parent = value;
            }
        }

        IHtmlElement IHtmlElement.AddClass(string name)
        {
            foreach(var item in Elements)
            {
                item.AddClass(name);
            }
            return this;
        }

        IHtmlElement IHtmlElement.After(IHtmlNode node)
        {
            if (Elements.Count > 0) Elements[0].After(node);
            return this;
        }

        IHtmlElement IHtmlElement.Append(IHtmlNode node)
        {
            if (Elements.Count > 0) Elements[0].Append(node);
            return this;
        }

        string IHtmlElement.Attribute(HtmlAttribute name)
        {
            return Elements.Count == 0 ? string.Empty : Elements[0].Attribute(name);
        }

        IHtmlElement IHtmlElement.Attribute(HtmlAttribute name, string value)
        {
            if (Elements.Count > 0) Elements[0].Attribute(name, value);
            return this;
        }

        IHtmlElement IHtmlElement.Before(IHtmlNode node)
        {
            if (Elements.Count > 0) Elements[0].Before(node);
            return this;
        }

        IEnumerable<IHtmlNode> IHtmlNode.Children()
        {
            return Elements.Count == 0 ? new List<IHtmlNode>() : Elements[0].Children();
        }

        IHtmlElement IHtmlElement.Css(object properties)
        {
            foreach(var item in Elements)
            {
                item.Css(properties);
            }
            return this;
        }

        string IHtmlElement.Css(string name)
        {
            return Elements.Count == 0 ? string.Empty : Elements[0].Css(name);
        }

        IHtmlElement IHtmlElement.Css(string name, string value)
        {
            foreach(var item in Elements)
            {
                item.Css(name, value);
            }
            return this;
        }

        string IHtmlElement.Data(string name)
        {
            return Elements.Count == 0 ? string.Empty : Elements[0].Data(name);
        }

        IHtmlElement IHtmlElement.Data(string name, string value)
        {
            foreach(var item in Elements)
            {
                item.Data(name, value);
            }
            return this;
        }

        void IHtmlNode.Format(IHtmlOutput outer)
        {
            foreach(var item in Elements)
            {
                item.Format(outer);
            }
        }

        List<IHtmlNode> IHtmlNode.GetNodes()
        {
            return Elements.Count == 0 ? new List<IHtmlNode>() : Elements[0].GetNodes();
        }

        IHtmlElement IHtmlElement.OnClient(HtmlEvent e, string script)
        {
            foreach(var item in Elements)
            {
                item.OnClient(e, script);
            }
            return this;
        }

        IHtmlElement IHtmlElement.Prepend(IHtmlNode node)
        {
            if (Elements.Count > 0) Elements[0].Prepend(node);
            return this;
        }

        IHtmlElement IHtmlElement.RemoveAttribute(HtmlAttribute name)
        {
            foreach (var item in Elements)
            {
                item.RemoveAttribute(name);
            }
            return this;
        }

        IHtmlElement IHtmlElement.RemoveClass(string name)
        {
            foreach (var item in Elements)
            {
                item.RemoveClass(name);
            }
            return this;
        }

        IHtmlElement IHtmlElement.RemoveData(string name)
        {
            foreach (var item in Elements)
            {
                item.RemoveData(name);
            }
            return this;
        }

        IHtmlElement IHtmlElement.Text(string text)
        {
            if (Elements.Count > 0) Elements[0].Text(text);
            return this;
        }
    }
}
