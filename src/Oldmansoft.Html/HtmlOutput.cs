using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html
{
    /// <summary>
    /// HTML 输出
    /// </summary>
    public class HtmlOutput : IHtmlOutput
    {
        /// <summary>
        /// 格式化输出
        /// </summary>
        public static bool DebugFormat { get; set; }

        private bool IsCompleted = false;

        private StringBuilder Outer { get; set; }

        /// <summary>
        /// 序号生成器
        /// </summary>
        public IdGenerator Generator { get; private set; }

        private List<IHtmlNode> Nodes { get; set; }

        /// <summary>
        /// 创建 HTML 输出
        /// </summary>
        /// <param name="node"></param>
        public HtmlOutput(params IHtmlNode[] node)
        {
            Outer = new StringBuilder();
            Generator = new IdGenerator();
            Nodes = new List<IHtmlNode>(node);
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node"></param>
        public void Add(params IHtmlNode[] node)
        {
            if (node == null) return;
            Nodes.AddRange(node);
        }
        
        void IHtmlOutput.Append(string value)
        {
            Outer.Append(value);
        }
        
        void IHtmlOutput.Append(HtmlAttribute attribute)
        {
            Outer.Append(attribute.ToString().ToLower().Replace("_", "-"));
        }

        void IHtmlOutput.Append(HtmlTag tag)
        {
            Outer.Append(tag.ToString().ToLower());
        }

        /// <summary>
        /// 添加字符
        /// </summary>
        void IHtmlOutput.Append(HtmlChar c)
        {
            Outer.Append(c.Value);
        }

        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <returns></returns>
        public string Complete()
        {
            if (IsCompleted) throw new InvalidOperationException("不同再次执行");
            IsCompleted = true;

            foreach (var node in Nodes)
            {
                node.Format(this);
            }
            return Outer.ToString();
        }
    }
}
