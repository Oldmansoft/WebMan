﻿namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 数字组件
    /// </summary>
    public class Numeric : FormInput
    {
        private object Value { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            Value = value;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Data("bv-integer", "false");
            Attribute(HtmlAttribute.Type, "number");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value == null ? string.Empty : Value.ToString());
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
            Text(Value == null ? string.Empty : Value.ToString());
        }
    }
}
