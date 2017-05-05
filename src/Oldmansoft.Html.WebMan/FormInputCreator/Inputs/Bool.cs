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
        private string Name { get; set; }

        private bool? Value { get; set; }

        private Dictionary<bool, string> Options { get; set; }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="scripts"></param>
        public override void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts)
        {
            Name = name;
            Options = new Dictionary<bool, string>();
            Options.Add(true, "是");
            Options.Add(false, "否");
            Value = (bool?)value;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readony, string hint)
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
                input.Attribute(HtmlAttribute.Name, Name);
                input.Attribute(HtmlAttribute.Value, option.Key.ToString().ToLower());
                if (Value.HasValue && option.Key == Value.Value)
                {
                    input.Attribute(HtmlAttribute.Checked, "checked");
                }
                SetAttribute(input, disabled, readony, hint);
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
            Text(Options[Value.Value]);
        }
    }
}
