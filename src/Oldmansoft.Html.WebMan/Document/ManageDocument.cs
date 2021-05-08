using System;
using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 管理页面文档
    /// </summary>
    public class ManageDocument : HtmlDocument
    {
        private string DefaultLink { get; set; }

        /// <summary>
        /// 搜索栏地址
        /// </summary>
        private ILocation SearchAction { get; set; }

        /// <summary>
        /// 搜索输入名称
        /// </summary>
        private string SearchInputName { get; set; }

        /// <summary>
        /// 商标名称
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public TreeList Menu { get; private set; }

        /// <summary>
        /// 任务栏
        /// </summary>
        public List<ILocation> Taskbar { get; private set; }

        /// <summary>
        /// 帐号资料快速菜单
        /// </summary>
        public QuickMenu Quick { get; private set; }

        /// <summary>
        /// 创建文档
        /// </summary>
        /// <param name="defaultLink">默认链接</param>
        public ManageDocument(ILocation defaultLink)
        {
            DefaultLink = defaultLink.Path;
            SearchAction = Location.Empty;
            Menu = new TreeList();
            Taskbar = new List<ILocation>();
            Quick = new QuickMenu();
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
            foreach (var item in InitAfterScripts)
            {
                Body.Append(item);
            }
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
            SetHeader(header);

            var nav = new HtmlElement(HtmlTag.Nav).AddClass("row");
            panel.Append(nav);

            var breadcrumb = new HtmlElement(HtmlTag.Div).AddClass("webman-breadcrumb");
            nav.Append(breadcrumb);
            breadcrumb.Append(new HtmlElement(HtmlTag.Span).Text("位置: "));
            breadcrumb.Append(new HtmlElement(HtmlTag.Ul).AddClass("breadcrumb"));

            var main = new HtmlElement(HtmlTag.Div).AddClass("webman-content row");
            panel.Append(main);
        }

        private void SetHeader(IHtmlElement header)
        {
            var bar = new HtmlElement(HtmlTag.Div).AddClass("webman-bar");
            header.Append(bar);
            bar.Append(Location.Create(null, null, FontAwesome.Bars, LinkBehave.Link).CreateElement());
            SetSearchAction(header);

            var nav = new HtmlElement(HtmlTag.Ul);
            header.Append(nav);
            foreach (var item in Taskbar)
            {
                item.Display = null;
                var a = item.CreateElement();
                a.AddClass("badge-container badge-bar");
                nav.Append(new HtmlElement(HtmlTag.Li).Append(a));
            }

            SetQuickMenu(nav);
        }

        private void SetSearchAction(IHtmlElement header)
        {
            var form = new HtmlElement(HtmlTag.Form);
            header.Append(form);
            form.Attribute(HtmlAttribute.Method, "post").Attribute(HtmlAttribute.Action, SearchAction.Path);
            SearchAction.Behave.SetTargetAttribute(form);

            var input = new HtmlElement(HtmlTag.Input)
                .Attribute(HtmlAttribute.Type, "text")
                .Attribute(HtmlAttribute.Name, SearchInputName)
                .AddClass("form-control");
            if (!string.IsNullOrEmpty(SearchAction.Display))
            {
                input.Attribute(HtmlAttribute.PlaceHolder, SearchAction.Display);
            }
            
            var button = new HtmlElement(HtmlTag.I).AddClass("fa fa-search");
            form.Append(input).Append(button);
            if (SearchAction == Location.Empty) form.AddClass("hidden");
        }

        private void SetQuickMenu(HtmlElement nav)
        {
            if (Quick.Avatar.Photo == null && Quick.Avatar.Display == null) return;

            var account = new HtmlElement(HtmlTag.A).AddClass("webman-account");
            if (string.IsNullOrEmpty(Quick.Avatar.Photo))
            {
                account.Append(FontAwesome.User.CreateElement());
            }
            else
            {
                account.Append(new HtmlElement(HtmlTag.Img).Attribute(HtmlAttribute.Src, Quick.Avatar.Photo));
            }
            account.Append(new HtmlElement(HtmlTag.Span).Text(Quick.Avatar.Display));
            account.AddClass("dropdown-toggle");
            account.Data("toggle", "dropdown");

            var quick = new HtmlElement(HtmlTag.Li).Append(account);
            nav.Append(quick);
            quick.AddClass("dropdown");

            if (Quick.Items.Count == 0) return;
            var quickItems = new HtmlElement(HtmlTag.Ul);
            quick.Append(quickItems);
            quickItems.AddClass("dropdown-menu");
            quickItems.AddClass("pull-right");
            foreach (var item in Quick.Items)
            {
                var a = item.CreateElement();
                a.AddClass("badge-container");
                quickItems.Append(new HtmlElement(HtmlTag.Li).Append(a));
            }
        }

        /// <summary>
        /// 设置快速搜索栏
        /// </summary>
        /// <param name="location"></param>
        /// <param name="name"></param>
        public void SetQuickSearch(ILocation location, string name = "key")
        {
            SearchAction = location ?? throw new ArgumentNullException();
            SearchInputName = name;
        }
    }
}
