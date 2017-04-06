using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class Hidden : FormInput
    {
        private string Value { get; set; }

        public Hidden(string name, object value)
            : base(name)
        {
            Value = value == null ? string.Empty : value.ToString();
        }

        public override void SetInputMode()
        {
            Attribute(HtmlAttribute.Type, "hidden");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value);
            SetAttribute(this);
        }

        public override void SetViewMode()
        {
            SetInputMode();
        }
    }
}
