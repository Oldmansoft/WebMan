using System.Collections.Generic;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 节点容器
    /// </summary>
    public class HtmlNodeContainer : HtmlNode
    {
        private readonly List<IHtmlNode> Nodes;

        /// <summary>
        /// 创建列表
        /// </summary>
        /// <param name="node"></param>
        public HtmlNodeContainer(params IHtmlNode[] node)
        {
            Nodes = new List<IHtmlNode>();
            if (node != null || node.Length > 0) Nodes.AddRange(node);
        }
        
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            foreach (var node in Nodes)
            {
                node.Format(outer);
            }
        }
    }
}
