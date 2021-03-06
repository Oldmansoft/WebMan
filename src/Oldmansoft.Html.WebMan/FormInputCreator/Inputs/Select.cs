﻿namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 下拉选择组件
    /// </summary>
    public class Select : RadioList
    {
        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Tag = HtmlTag.Select;
            Attribute(HtmlAttribute.Name, Name);
            AddClass("form-control");
            if (Value == null)
            {
                Append(new HtmlElement(HtmlTag.Option).Append(new HtmlRaw("&nbsp;")));
            }
            foreach (var option in Options)
            {
                var item = new HtmlElement(HtmlTag.Option);
                Append(item);
                item.Attribute(HtmlAttribute.Value, option.Value);
                if (option.Value == Value)
                {
                    item.Attribute(HtmlAttribute.Selected, "selected");
                }
                item.Text(option.Text);
            }
            SetAttributeDisabledReadOnlyPlaceHolder(this, PropertyContent.ReadOnly || PropertyContent.Disabled);
            HtmlData.SetContext(this);
        }
    }
}
