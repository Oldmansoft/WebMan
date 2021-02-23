using System;
using System.Collections.Generic;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 空节点
    /// </summary>
    sealed class EmptyNode : IHtmlNode
    {
        private List<IHtmlNode> Nodes { get; set; }

        public EmptyNode()
        {
            Nodes = new List<IHtmlNode>();
        }

        IHtmlNode IHtmlNode.Parent
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<IHtmlNode> GetNodes()
        {
            return Nodes;
        }

        public void Format(IHtmlOutput outer)
        {
            foreach (var item in Nodes)
            {
                item.Format(outer);
            }
        }

        public IEnumerable<IHtmlNode> Children()
        {
            return Nodes;
        }
    }
}
