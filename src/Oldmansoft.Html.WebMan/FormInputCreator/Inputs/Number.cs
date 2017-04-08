using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class Number : FormInput
    {
        private object Value { get; set; }

        public Number(string name, object value)
            : base(name)
        {
            Value = value;
        }

        public override void SetInputMode()
        {
            Attribute(HtmlAttribute.Type, "number");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value == null ? string.Empty : Value.ToString());
            SetAttribute(this);
            AddClass("form-control");
        }

        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            Text(Value == null ? string.Empty : Value.ToString());
        }
    }
}
