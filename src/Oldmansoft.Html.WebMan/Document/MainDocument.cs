﻿using Oldmansoft.Html.Util;
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
        private ILocation SearchAction { get; set; }

        /// <summary>
        /// 任务栏
        /// </summary>
        public List<ILocation> Taskbar { get; private set; }

        /// <summary>
        /// 帐号资料快速菜单
        /// </summary>
        public QuickMenu Quick { get; private set; }

        /// <summary>
        /// 创建主页文档
        /// </summary>
        /// <param name="defaultLink">默认链接</param>
        public MainDocument(ILocation defaultLink)
        {
            DefaultLink = defaultLink.Path;
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
            breadcrumb.Append(new HtmlElement(HtmlTag.Ul).AddClass("breadcrumb"));

            var main = new HtmlElement(HtmlTag.Div).AddClass("webman-content row");
            panel.Append(main);
        }

        private void SestHeader(IHtmlElement header)
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
            if (SearchAction == null) return;

            var form = new HtmlElement(HtmlTag.Form);
            header.Append(form);
            form.Attribute(HtmlAttribute.Method, "post").Attribute(HtmlAttribute.Action, SearchAction.Path);
            SearchAction.Behave.SetTargetAttribute(form);

            var input = new HtmlElement(HtmlTag.Input)
                .Attribute(HtmlAttribute.Type, "text")
                .Attribute(HtmlAttribute.Name, "keyword")
                .AddClass("form-control");
            if (!string.IsNullOrEmpty(SearchAction.Display))
            {
                input.Attribute(HtmlAttribute.PlaceHolder, SearchAction.Display);
            }
            
            var button = new HtmlElement(HtmlTag.I).AddClass("fa fa-search");
            form.Append(input).Append(button);
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
        /// <param name="location"></param>
        public void SetSearchAction(ILocation location)
        {
            SearchAction = location;
        }
    }
}
