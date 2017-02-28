using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 面板
    /// </summary>
    public class Panel : HtmlElement
    {
        private IHtmlElement HeaderCaption { get; set; }

        private IHtmlElement Header { get; set; }

        private IHtmlElement Body { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public FontAwesome Icon { get; set; }

        /// <summary>
        /// 创建面板
        /// </summary>
        public Panel()
            : base(HtmlTag.Div)
        {
            Header = new HtmlElement(HtmlTag.Header);
            base.Append(Header);

            HeaderCaption = new HtmlElement(HtmlTag.H2);
            Header.Append(HeaderCaption);

            var tools = new HtmlElement(HtmlTag.Div).AddClass("webman-panel-tools");
            Header.Append(tools);
            tools.Append(FontAwesome.Times.CreateElement());

            Body = new HtmlElement(HtmlTag.Div);
            base.Append(Body);
            Body.AddClass("webman-body");
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            Header.Prepend(Icon.CreateElement());
            HeaderCaption.Text(Caption);
            AddClass("webman-panel");
            base.Format(outer);
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override IHtmlElement Append(IHtmlNode node)
        {
            return Body.Append(node);
        }

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override IHtmlElement Prepend(IHtmlNode node)
        {
            return Body.Prepend(node);
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override IHtmlElement Text(string text)
        {
            return Body.Text(text);
        }
    }
}
