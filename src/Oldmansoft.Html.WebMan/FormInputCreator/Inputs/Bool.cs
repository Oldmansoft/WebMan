using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 布尔值组件
    /// </summary>
    public class Bool : FormInput
    {
        private bool? Value { get; set; }

        private Dictionary<bool, string> Source { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            Source = new Dictionary<bool, string>();
            Source.Add(true, "是");
            Source.Add(false, "否");
            Value = (bool?)value;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            foreach (var option in Source)
            {
                var label = new HtmlElement(HtmlTag.Label);
                Append(label);
                label.AddClass("radio-inline");

                var input = new HtmlElement(HtmlTag.Input);
                label.Append(input);
                input.Attribute(HtmlAttribute.Type, "radio");
                input.Attribute(HtmlAttribute.Name, PropertyContent.Name);
                input.Attribute(HtmlAttribute.Value, option.Key.ToString().ToLower());
                if (Value.HasValue && option.Key == Value.Value)
                {
                    input.Attribute(HtmlAttribute.Checked, "checked");
                }
                if (PropertyContent.Disabled || PropertyContent.ReadOnly) input.Attribute(HtmlAttribute.Disabled, "disabled");
                HtmlData.SetContext(input);
                label.Append(new HtmlRaw(option.Value.HtmlEncode()));
            }
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            if (!Value.HasValue) return;
            Text(Source[Value.Value]);
        }
    }
}
