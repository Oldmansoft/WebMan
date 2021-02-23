namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// Markdown
    /// </summary>
    public class Markdown : FormInputCreator.FormInput, ICustomInput
    {
        private string Value { get; set; }

        void ICustomInput.Set(object[] parameter)
        {
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            Value = value.GetNotNullString();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Tag = HtmlTag.Textarea;
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Rows, "5");
            Data("provide", "markdown");
            if (PropertyContent.Disabled || PropertyContent.ReadOnly) Attribute(HtmlAttribute.Disabled, "disabled");
            if (!string.IsNullOrEmpty(PropertyContent.Description)) Attribute(HtmlAttribute.PlaceHolder, PropertyContent.Description);
            Text(Value);
            AddClass("form-control");
            AddClass("markdown");

            ScriptRegister.Register("MarkdownEdit", "view.node.find('textarea.markdown').markdown({resize : 'vertical'});");
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            AddClass("markdown");
            Append(new HtmlRaw(Value.HtmlEncode().Replace("\r\n", "<br/>").Replace("\n", "<br/>")));

            ScriptRegister.Register("MarkdownView", "view.node.find('div.markdown').html(markdown.toHTML(view.node.find('div.markdown').text()));");
        }
    }
}
