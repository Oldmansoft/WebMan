using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 主页文档
    /// </summary>
    public class MainDocument : HtmlDocument
    {
        private string DefaultLink { get; set; }

        /// <summary>
        /// 商标名称
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public TreeList Menu { get; private set; }

        /// <summary>
        /// 创建主页文档
        /// </summary>
        /// <param name="link"></param>
        public MainDocument(string link)
        {
            DefaultLink = link;
            Menu = new TreeList();
        }

        /// <summary>
        /// 格式化之前
        /// </summary>
        protected override void BeforeFormat()
        {
            base.BeforeFormat();

            var section = new HtmlElement(HtmlTag.Section);
            Body.Append(section);

            var leftPanel = new HtmlElement(HtmlTag.Article).AddClass("webman-left-panel col-sm-2");
            section.Append(leftPanel);
            SetLeftPanelContent(leftPanel);

            var mainPanel = new HtmlElement(HtmlTag.Article).AddClass("webman-main-panel col-sm-10");
            section.Append(mainPanel);
            SetMainPanelContent(mainPanel);

            var rightPanel = new HtmlElement(HtmlTag.Div).AddClass("webman-right-panel");
            section.Append(rightPanel);

            var script = new Element.Script(string.Format("$man.init('.webman-main-panel>.webman-content', '{0}');", DefaultLink));
            Body.Append(script);
        }

        private void SetLeftPanelContent(IHtmlElement panel)
        {
            var header = new HtmlElement(HtmlTag.Header).AddClass("row");
            panel.Append(header);

            header.Append(new HtmlText("["));
            var h1 = new HtmlElement(HtmlTag.H1).Text(Logo ?? Title);
            header.Append(h1);
            header.Append(new HtmlText("]"));

            var nav = new HtmlElement(HtmlTag.Nav).AddClass("row");
            panel.Append(nav);

            nav.Append(Menu);
        }

        private void SetMainPanelContent(IHtmlElement panel)
        {
            var header = new HtmlElement(HtmlTag.Header).AddClass("row");
            panel.Append(header);
            SestHeader(header);

            var nav = new HtmlElement(HtmlTag.Nav).AddClass("row");
            panel.Append(nav);

            var main = new HtmlElement(HtmlTag.Div).AddClass("webman-content row");
            panel.Append(main);
        }

        private static void SestHeader(IHtmlElement header)
        {
            var bar = new HtmlElement(HtmlTag.Div).AddClass("webman-bar");
            header.Append(bar);
            bar.Append(new LinkContent(FontAwesome.Bars).Create());

            var form = new HtmlElement(HtmlTag.Form);
            header.Append(form).Attribute(HtmlAttribute.Method, "post");
            var input = new HtmlElement(HtmlTag.Input)
                .Attribute(HtmlAttribute.Type, "text")
                .Attribute(HtmlAttribute.Name, "keyword")
                .Attribute(HtmlAttribute.PlaceHolder, "Search here...")
                .AddClass("form-control");
            form.Append(input);

            var quick = new HtmlElement(HtmlTag.Ul);
            header.Append(quick);
        }

        private void SetRightPanelContent(IHtmlElement panel)
        {
            var nav = new HtmlElement(HtmlTag.Nav);
            panel.Append(nav);

            var main = new HtmlElement(HtmlTag.Div).AddClass("webman-content");
            panel.Append(main);
        }
    }
}
