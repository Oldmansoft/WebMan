using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class CheckBoxList : FormInput
    {
        protected IEnumerable<string> Values { get; set; }

        protected IList<ListDataItem> Options { get; set; }

        public CheckBoxList(string name, IEnumerable<string> values, IList<ListDataItem> options)
            : base(name)
        {
            Values = values;
            if (Values == null)
            {
                Values = new string[] { };
            }
            Options = options;
        }

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
                SetAttribute(input);
                label.Append(new HtmlRaw(option.Text.HtmlEncode()));
            }
        }

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
