using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class DateTime : FormInput
    {
        private System.DateTime? Value { get; set; }

        public DateTime(string name, System.DateTime? value)
            : base(name)
        {
            Value = value;
        }

        private string GetValue()
        {
            if (!Value.HasValue) return string.Empty;
            if (Value.Value == System.DateTime.MinValue) return string.Empty;
            var result = Value.Value;
            if (result.Kind == System.DateTimeKind.Utc)
            {
                result = result.ToLocalTime();
            }
            return result.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            AddClass("input-group");

            var span = new HtmlElement(HtmlTag.Span);
            Append(span);
            span.AddClass("input-group-addon");
            span.Append(new HtmlElement(HtmlTag.I).AddClass("fa fa-calendar-o"));

            var input = new HtmlElement(HtmlTag.Input);
            Append(input);
            input.Attribute(HtmlAttribute.Type, "datetime-local");
            input.Attribute(HtmlAttribute.Name, Name);
            input.Attribute(HtmlAttribute.Value, GetValue());
            SetAttribute(input);
            input.AddClass("form-control");
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
