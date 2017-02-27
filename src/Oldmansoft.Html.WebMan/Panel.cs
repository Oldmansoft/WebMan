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
            var header = new HtmlElement(HtmlTag.Header);
            base.Append(header);

            header.Append(Icon.CreateElement());
            HeaderCaption = new HtmlElement(HtmlTag.H2);
            header.Append(HeaderCaption);

            var tools = new HtmlElement(HtmlTag.Div).AddClass("webman-panel-tools");
            header.Append(tools);
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
            HeaderCaption.Text(Caption);
            AddClass("webman-panel");
            base.Format(outer);
        }

        /// <summary>
        /// 创建布局元素
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public IHtmlElement CreateLayout(Col col = Col.Xs12)
        {
            var result = new HtmlElement(HtmlTag.Div);
            result.AddClass(col);
            result.Append(this);
            return result;
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
