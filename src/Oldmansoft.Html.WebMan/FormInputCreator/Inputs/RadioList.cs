using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 单选框列表组件
    /// </summary>
    public class RadioList : FormInput
    {
        /// <summary>
        /// 值
        /// </summary>
        protected string Value { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            Value = value.GetString();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            foreach (var option in Options)
            {
                var label = new HtmlElement(HtmlTag.Label);
                Append(label);
                label.AddClass("radio-inline");

                var input = new HtmlElement(HtmlTag.Input);
                label.Append(input);
                input.Attribute(HtmlAttribute.Type, "radio");
                input.Attribute(HtmlAttribute.Name, PropertyContent.Name);
                input.Attribute(HtmlAttribute.Value, option.Value);
                if (option.Value == Value)
                {
                    input.Attribute(HtmlAttribute.Checked, "checked");
                }
                if (PropertyContent.Disabled || PropertyContent.ReadOnly) input.Attribute(HtmlAttribute.Disabled, "disabled");
                HtmlData.SetContext(input);
                label.Append(new HtmlRaw(option.Text.HtmlEncode()));
            }
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");

            var item = Options.FirstOrDefault(o => o.Value == Value);
            if (item == null) return;
            Text(item.Text);
        }
    }
}
