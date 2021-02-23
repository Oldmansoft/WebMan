namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 文本组件
    /// </summary>
    public class Text : FormInput
    {
        private string Value { get; set; }

        private Annotations.InputMaxLengthAttribute InputMaxLength { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            InputMaxLength = PropertyContent.Attributes.Get<Annotations.InputMaxLengthAttribute>();
            Value = value.GetNotNullString();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Attribute(HtmlAttribute.Type, "text");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value);
            if (InputMaxLength != null) Attribute(HtmlAttribute.MaxLength, InputMaxLength.Length.ToString());
            SetAttributeDisabledReadOnlyPlaceHolder(this, PropertyContent.Disabled);
            AddClass("form-control");
            HtmlData.SetContext(this);
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            Text(Value);
        }
    }
}
