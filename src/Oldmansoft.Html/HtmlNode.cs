using System;
using System.Collections.Generic;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 节点
    /// </summary>
    public abstract class HtmlNode : IHtmlNode
    {
        /// <summary>
        /// 子节点
        /// </summary>
        private readonly List<IHtmlNode> Nodes;

        private IHtmlNode _Parent;

        /// <summary>
        /// 创建节点
        /// </summary>
        public HtmlNode()
        {
            _Parent = new EmptyNode();
            Nodes = new List<IHtmlNode>();
            _Parent.Children().Add(this);
        }

        IHtmlNode IHtmlNode.Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        void IHtmlNode.Format(IHtmlOutput outer)
        {
            if (outer == null) throw new ArgumentNullException("outer");
            Format(outer);
        }
        
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected abstract void Format(IHtmlOutput outer);

        /// <summary>
        /// 节点添加
        /// </summary>
        /// <param name="node"></param>
        protected void NodesAppend(IHtmlNode node)
        {
            node.Parent.Children().Remove(node);
            node.Parent = this;
            Nodes.Add(node);
        }

        /// <summary>
        /// 节点插入
        /// </summary>
        /// <param name="node"></param>
        protected void NodesPrepend(IHtmlNode node)
        {
            node.Parent.Children().Remove(node);
            node.Parent = this;
            Nodes.Insert(0, node);
        }

        /// <summary>
        /// 元素后贴
        /// </summary>
        /// <param name="node"></param>
        protected void NodeAfter(IHtmlNode node)
        {
            node.Parent.Children().Remove(node);
            var nodes = _Parent.Children();
            var index = nodes.IndexOf(this);
            node.Parent = _Parent;
            nodes.Insert(index + 1, node);
        }

        /// <summary>
        /// 元素前贴
        /// </summary>
        /// <param name="node"></param>
        protected void NodeBefore(IHtmlNode node)
        {
            node.Parent.Children().Remove(node);
            var nodes = _Parent.Children();
            var index = nodes.IndexOf(this);
            node.Parent = _Parent;
            nodes.Insert(index, node);
        }

        /// <summary>
        /// 节点清除
        /// </summary>
        protected void NodesClear()
        {
            foreach(var item in Nodes)
            {
                item.Parent = new EmptyNode();
            }
            Nodes.Clear();
        }

        /// <summary>
        /// 节点是否为空
        /// </summary>
        /// <returns></returns>
        protected bool NodesIsEmpty()
        {
            return Nodes.Count == 0;
        }

        /// <summary>
        /// 子节点
        /// </summary>
        /// <returns></returns>
        public IList<IHtmlNode> Children()
        {
            return Nodes;
        }
    }
}
