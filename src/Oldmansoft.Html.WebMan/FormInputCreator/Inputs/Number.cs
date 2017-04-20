using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class Number : FormInput
    {
        private string Name { get; set; }

        private object Value { get; set; }
        
        public override void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts)
        {
            Name = name;
            Value = value;
        }

        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Attribute(HtmlAttribute.Type, "number");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value == null ? string.Empty : Value.ToString());
            SetAttribute(this, disabled, readony, hint);
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
