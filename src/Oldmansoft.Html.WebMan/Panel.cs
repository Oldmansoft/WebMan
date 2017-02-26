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

        /// <summary>
        /// 宽度
        /// </summary>
        public Col Col { get; set; }

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
        public Panel(Col col = Col.Xs12)
            : base(HtmlTag.Div)
        {
            Col = col;

            var header = new HtmlElement(HtmlTag.Header);
            Append(header);

            header.Append(Icon.CreateElement());
            HeaderCaption = new HtmlElement(HtmlTag.H2);
            header.Append(HeaderCaption);

            var tools = new HtmlElement(HtmlTag.Div).AddClass("webman-panel-tools");
            header.Append(tools);
            tools.Append(FontAwesome.Times.CreateElement());
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            HeaderCaption.Text(Caption);
            AddClass("webman-panel");
            AddClass(Col.ToClassName());
            base.Format(outer);
        }
    }
}
