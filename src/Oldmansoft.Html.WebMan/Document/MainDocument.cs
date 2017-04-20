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
        /// 搜索栏地址
        /// </summary>
        private string SearchAction { get; set; }

        /// <summary>
        /// 任务栏
        /// </summary>
        public List<LinkContent> Taskbar { get; private set; }

        /// <summary>
        /// 帐号资料
        /// </summary>
        public QuickMenu Account { get; set; }

        /// <summary>
        /// 创建主页文档
        /// </summary>
        /// <param name="link"></param>
        public MainDocument(string link)
        {
            DefaultLink = link;
            Menu = new TreeList();
            Taskbar = new List<LinkContent>();
        }

        /// <summary>
        /// 格式化之前
        /// </summary>
        protected override void BeforeFormat()
        {
            base.BeforeFormat();

            var section = new HtmlElement(HtmlTag.Section).AddClass("container-fluid");
            Body.Append(section);

            var leftPanel = new HtmlElement(HtmlTag.Section).AddClass("webman-left-panel");
            section.Append(leftPanel);
            SetLeftPanelContent(leftPanel);

            var mainPanel = new HtmlElement(HtmlTag.Section).AddClass("webman-main-panel");
            section.Append(mainPanel);
            SetMainPanelContent(mainPanel);

            var rightPanel = new HtmlElement(HtmlTag.Div).AddClass("webman-right-panel");
            section.Append(rightPanel);

            var script = new Element.Script(string.Format("$man.init('.webman-main-panel>.webman-content', '{0}');", DefaultLink));
            Body.Append(script);
        }

        private void SetLeftPanelContent(IHtmlElement panel)
        {
            var header = new HtmlElement(HtmlTag.Header).AddClass("row").Text(Logo ?? Title);
            panel.Append(header);
            
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

            var breadcrumb = new HtmlElement(HtmlTag.Div).AddClass("webman-breadcrumb");
            nav.Append(breadcrumb);
            breadcrumb.Append(new HtmlElement(HtmlTag.Span).Text("位置: "));
            breadcrumb.Append(new HtmlElement(HtmlTag.Ul));

            var main = new HtmlElement(HtmlTag.Div).AddClass("webman-content row");
            panel.Append(main);
        }

        private void SestHeader(IHtmlElement header)
        {
            var bar = new HtmlElement(HtmlTag.Div).AddClass("webman-bar");
            header.Append(bar);
            bar.Append(new LinkContent(FontAwesome.Bars).CreateElement());

            if (!string.IsNullOrEmpty(SearchAction))
            {
                var form = new HtmlElement(HtmlTag.Form);
                header.Append(form);
                form.Attribute(HtmlAttribute.Method, "post").Attribute(HtmlAttribute.Action, SearchAction);

                var input = new HtmlElement(HtmlTag.Input)
                    .Attribute(HtmlAttribute.Type, "text")
                    .Attribute(HtmlAttribute.Name, "keyword")
                    .Attribute(HtmlAttribute.PlaceHolder, "Search here...")
                    .AddClass("form-control");
                var button = new HtmlElement(HtmlTag.I).AddClass("fa fa-search");
                form.Append(input).Append(button);
            }

            var nav = new HtmlElement(HtmlTag.Ul);
            header.Append(nav);
            foreach (var item in Taskbar)
            {
                nav.Append(new HtmlElement(HtmlTag.Li).Append(item.CreateElement()));
            }

            if (Account != null)
            {
                var account = new HtmlElement(HtmlTag.A).AddClass("webman-account");
                if (!string.IsNullOrEmpty(Account.Image))
                {
                    account.Append(new HtmlElement(HtmlTag.Img).Attribute(HtmlAttribute.Src, Account.Image));
                }
                account.Append(new HtmlText(Account.Text));
                nav.Append(new HtmlElement(HtmlTag.Li).Append(account));
            }
        }

        private void SetRightPanelContent(IHtmlElement panel)
        {
            var nav = new HtmlElement(HtmlTag.Nav);
            panel.Append(nav);

            var main = new HtmlElement(HtmlTag.Div).AddClass("webman-content");
            panel.Append(main);
        }

        /// <summary>
        /// 设置搜索栏路径
        /// </summary>
        /// <param name="action"></param>
        public void SetSearchAction(string action)
        {
            SearchAction = action;
        }
    }
}
