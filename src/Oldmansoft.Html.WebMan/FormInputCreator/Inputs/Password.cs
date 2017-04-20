using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class Password : FormInput
    {
        private string Name { get; set; }

        private string Value { get; set; }
        
        public override void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts)
        {
            Name = name;
            Value = value.GetString();
        }

        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Attribute(HtmlAttribute.Type, "password");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value);
            SetAttribute(this, disabled, readony, hint);
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
