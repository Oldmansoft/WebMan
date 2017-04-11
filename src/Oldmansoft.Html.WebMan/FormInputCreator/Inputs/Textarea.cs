using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class Textarea : FormInput
    {
        protected string Value { get; set; }

        public Textarea(string name, string value)
            : base(name)
        {
            Value = value;
        }

        public override void SetInputMode()
        {
            Tag = HtmlTag.Textarea;
            Attribute(HtmlAttribute.Type, "text");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value);
            Attribute(HtmlAttribute.Rows, "5");
            SetAttribute(this);
            AddClass("form-control");
        }

        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value textarea");
            Append(new HtmlRaw(Value.HtmlEncode().Replace("\r\n", "<br/>").Replace("\n", "<br/>")));
        }
    }
}
