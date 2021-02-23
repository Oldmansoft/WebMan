using System.Collections.Generic;
using System.Linq;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 多选框列表组件
    /// </summary>
    public class CheckBoxList : FormInput
    {
        /// <summary>
        /// 值
        /// </summary>
        protected IEnumerable<string> Values { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            Values = value.GetListString();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            foreach(var option in Options)
            {
                var label = new HtmlElement(HtmlTag.Label);
                Append(label);
                label.AddClass("checkbox-inline");

                var input = new HtmlElement(HtmlTag.Input);
                label.Append(input);
                input.Attribute(HtmlAttribute.Type, "checkbox");
                input.Attribute(HtmlAttribute.Name, Name);
                input.Attribute(HtmlAttribute.Value, option.Value);
                if (Values.Contains(option.Value))
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
            Tag = HtmlTag.Ul;
            AddClass("control-value");
            foreach(var option in Options)
            {
                if (!Values.Contains(option.Value)) continue;
                var li = new HtmlElement(HtmlTag.Li);
                Append(li);
                li.Text(option.Text);
            }
        }
    }
}
