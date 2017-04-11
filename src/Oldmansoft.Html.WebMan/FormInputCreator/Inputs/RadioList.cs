using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class RadioList : FormInput
    {
        protected string Value { get; set; }

        protected IList<ListDataItem> Options { get; set; }

        public RadioList(string name, string value, IList<ListDataItem> options)
            : base(name)
        {
            Value = value;
            Options = options;
        }

        public override void SetInputMode()
        {
            Tag = HtmlTag.Ul;
            AddClass("radio");
            foreach (var option in Options)
            {
                var li = new HtmlElement(HtmlTag.Li);
                Append(li);
                var label = new HtmlElement(HtmlTag.Label);
                li.Append(label);
                var input = new HtmlElement(HtmlTag.Input);
                label.Append(input);
                input.Attribute(HtmlAttribute.Type, "radio");
                input.Attribute(HtmlAttribute.Name, Name);
                input.Attribute(HtmlAttribute.Value, option.Value);
                if (option.Value == Value)
                {
                    input.Attribute(HtmlAttribute.Checked, "checked");
                }
                SetAttribute(input);
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
