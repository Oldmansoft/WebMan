using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class Date : FormInput
    {
        private DateTime? Value { get; set; }

        public Date(string name, DateTime? value)
            : base(name)
        {
            Value = value;
        }

        private string GetValue()
        {
            if (!Value.HasValue) return string.Empty;
            if (Value.Value == DateTime.MinValue) return string.Empty;
            var result = Value.Value;
            if (result.Kind == DateTimeKind.Utc)
            {
                result = result.ToLocalTime();
            }
            return result.ToString("yyyy-MM-dd");
        }

        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            var input = new HtmlElement(HtmlTag.Input);
            Append(input);
            input.Attribute(HtmlAttribute.Type, "date");
            input.Attribute(HtmlAttribute.Name, Name);
            input.Attribute(HtmlAttribute.Value, GetValue());
            SetAttribute(input);

            var span = new HtmlElement(HtmlTag.Span);
            Append(span);
            span.AddClass("fa fa-calendar form-control-feedback");
            if (Disabled || ReadOnly) return;
        }

        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            Text(GetValue());

            var span = new HtmlElement(HtmlTag.Span);
            Append(span);
            span.AddClass("fa fa-calendar");
        }
    }
}
