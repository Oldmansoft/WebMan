using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// HTML 结果
    /// </summary>
    public class HtmlResult : System.Web.Mvc.ContentResult, IHtmlOutput
    {
        private HtmlOutput Writer { get; set; }

        private IHtmlOutput Store { get; set; }

        /// <summary>
        /// 序号生成器
        /// </summary>
        public IGenerator<int> Generator
        {
            get
            {
                return Writer.Generator;
            }
        }

        /// <summary>
        /// 存储项
        /// </summary>
        public IList<string> Items
        {
            get
            {
                return Writer.Items;
            }
        }

        /// <summary>
        /// 当完成时
        /// </summary>
        public Action<IHtmlOutput> OnCompleted
        {
            get
            {
                return Writer.OnCompleted;
            }
            set
            {
                Writer.OnCompleted = value;
            }
        }

        /// <summary>
        /// 创建 Html 结果
        /// </summary>
        /// <param name="node"></param>
        public HtmlResult(params IHtmlNode[] node)
        {
            Writer = new HtmlOutput(node);
            Store = Writer;
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node"></param>
        public void Add(params IHtmlNode[] node)
        {
            Writer.Add(node);
        }

        void IHtmlOutput.Append(string value)
        {
            Store.Append(value);
        }

        void IHtmlOutput.Append(HtmlAttribute attribute)
        {
            Store.Append(attribute);
        }

        void IHtmlOutput.Append(HtmlTag tag)
        {
            Store.Append(tag);
        }

        /// <summary>
        /// 添加字符
        /// </summary>
        void IHtmlOutput.Append(HtmlChar c)
        {
            Store.Append(c);
        }
        
        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(System.Web.Mvc.ControllerContext context)
        {
            Content = Writer.Complete();
            base.ExecuteResult(context);
        }
    }
}
