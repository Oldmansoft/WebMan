using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<IHtmlNode> Nodes = new List<IHtmlNode>();
        
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
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
        protected void NodesAdd(IHtmlNode node)
        {
            Nodes.Add(node);
        }

        /// <summary>
        /// 节点插入
        /// </summary>
        /// <param name="node"></param>
        protected void NodesInsert(IHtmlNode node)
        {
            Nodes.Insert(0, node);
        }

        /// <summary>
        /// 节点清除
        /// </summary>
        protected void NodesClear()
        {
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
        public IEnumerable<IHtmlNode> Children()
        {
            return Nodes;
        }
    }
}
