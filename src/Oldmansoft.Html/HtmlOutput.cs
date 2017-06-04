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
        private bool IsCompleted = false;

        private StringBuilder Outer { get; set; }

        private List<IHtmlNode> Nodes { get; set; }

        /// <summary>
        /// 格式化输出
        /// </summary>
        public static bool DebugFormat { get; set; }

        /// <summary>
        /// 序号生成器
        /// </summary>
        public IGenerator<int> Generator { get; private set; }

        /// <summary>
        /// 存储项
        /// </summary>
        public IDictionary<object, object> Items { get; private set; }

        /// <summary>
        /// 当完成时
        /// </summary>
        public Action<IHtmlOutput> OnCompleted { get; set; }

        /// <summary>
        /// 创建 HTML 输出
        /// </summary>
        /// <param name="node"></param>
        public HtmlOutput(params IHtmlNode[] node)
        {
            Outer = new StringBuilder();
            Generator = new IdGenerator();
            Nodes = new List<IHtmlNode>(node);
            Items = new Dictionary<object, object>();
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
            if (IsCompleted) throw new InvalidOperationException("不能多次执行");
            IsCompleted = true;

            foreach (var node in Nodes)
            {
                node.Format(this);
            }
            if (OnCompleted != null) OnCompleted(this);
            return Outer.ToString();
        }
    }
}
