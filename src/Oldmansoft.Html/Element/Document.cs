using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Element
{
    /// <summary>
    /// 网页文档
    /// </summary>
    public class Document : HtmlElement
    {
        private IHtmlElement TitleNode { get; set; }

        private string _Title { get; set; }

        /// <summary>
        /// 头
        /// </summary>
        public IHtmlElement Head { get; private set; }

        /// <summary>
        /// 身体
        /// </summary>
        public IHtmlElement Body { get; private set; }

        /// <summary>
        /// 创建网页文档
        /// </summary>
        public Document()
            : base(HtmlTag.Html)
        {
            Head = new HtmlElement(HtmlTag.Head);
            TitleNode = new HtmlElement(HtmlTag.Title);
            Head.Append(TitleNode);
            Body = new HtmlElement(HtmlTag.Body);
            Append(Head).Append(Body);
        }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                TitleNode.Text(_Title);
            }
        }
        
        /// <summary>
        /// 格式化之前
        /// </summary>
        protected virtual void BeforeFormat()
        {
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            BeforeFormat();
            outer.Append("<!DOCTYPE html>");
            base.Format(outer);
        }
    }
}
