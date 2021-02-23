using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// HTML 结果
    /// </summary>
    public class HtmlResult : ContentResult, IHtmlOutput
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
        public IDictionary<object, object> Items
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
        /// 重载
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ExecuteResultAsync(ActionContext context)
        {
            ContentType = "text/html; charset=utf-8";
            Content = Writer.Complete();
            return base.ExecuteResultAsync(context);
        }
    }
}
