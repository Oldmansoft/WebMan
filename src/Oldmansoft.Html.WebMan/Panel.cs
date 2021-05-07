namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 面板
    /// </summary>
    public class Panel : HtmlElement
    {
        private IHtmlElement HeaderCaption { get; set; }

        private IHtmlElement HeaderSearchForm { get; set; }

        /// <summary>
        /// 头部
        /// </summary>
        public IHtmlElement Header { get; private set; }

        /// <summary>
        /// 身体
        /// </summary>
        public IHtmlElement Body { get; private set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

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
            tools.Append(new HtmlElement(HtmlTag.A).Append(FontAwesome.Times.CreateElement()).AddClass("webapp-close"));

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
            if (HeaderSearchForm != null)
            {
                Header.Append(HeaderSearchForm);
            }
            if (!string.IsNullOrWhiteSpace(Description))
            {
                var span = new HtmlElement(HtmlTag.Span);
                span.Text(Description);
                HeaderCaption.After(span);
            }
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

        /// <summary>
        /// 设置查找框
        /// </summary>
        /// <param name="location"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="placeholder"></param>
        public void SetSearch(ILocation location, string key = "key", string value = null, string placeholder = null)
        {
            HeaderSearchForm = new HtmlElement(HtmlTag.Form);
            HeaderSearchForm.Attribute(HtmlAttribute.Action, location.Path);
            HeaderSearchForm.Attribute(HtmlAttribute.Target, LinkBehave.Self.GetTarget());
            var group = new HtmlElement(HtmlTag.Div);
            group.AddClass("input-group");
            group.AppendTo(HeaderSearchForm);
            var input = new HtmlElement(HtmlTag.Input);
            input.AddClass("form-control");
            input.Attribute(HtmlAttribute.Name, key);
            if (!string.IsNullOrWhiteSpace(value)) input.Attribute(HtmlAttribute.Value, value);
            if (!string.IsNullOrWhiteSpace(placeholder)) input.Attribute(HtmlAttribute.PlaceHolder, placeholder);
            input.AppendTo(group);
            var addon = new HtmlElement(HtmlTag.Span);
            addon.AddClass("input-group-addon");
            addon.Append(FontAwesome.Search.CreateElement());
            addon.AppendTo(group);
            addon.OnClient(HtmlEvent.Click, "$(this).parentsUntil('form').submit();");
        }
    }
}
