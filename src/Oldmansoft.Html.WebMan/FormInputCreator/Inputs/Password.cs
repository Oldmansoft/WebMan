using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class Password : FormInput
    {
        private string Value { get; set; }

        public Password(string name, string value)
            : base(name)
        {
            Value = value;
        }

        public override void SetInputMode()
        {
            Attribute(HtmlAttribute.Type, "password");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value);
            SetAttribute(this);
            AddClass("form-control");
        }

        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            Text("******");
        }
    }
}
