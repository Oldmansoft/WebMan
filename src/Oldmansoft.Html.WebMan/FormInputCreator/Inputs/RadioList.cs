using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class RadioList : FormInput
    {
        protected string Name { get; set; }

        protected string Value { get; set; }

        protected IList<ListDataItem> Options { get; set; }
        
        public override void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts)
        {
            Name = name;
            Value = value.GetString();
            Options = options;
        }

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
                input.Attribute(HtmlAttribute.Value, option.Value);
                if (option.Value == Value)
                {
                    input.Attribute(HtmlAttribute.Checked, "checked");
                }
                SetAttribute(input, disabled, readony, hint);
                label.Append(new HtmlRaw(option.Text.HtmlEncode()));
            }
        }

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
