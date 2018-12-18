using Oldmansoft.Html.WebMan.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 时间日期组件
    /// </summary>
    public class DateTimeInput : FormInput
    {
        private System.DateTime? Value { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            Value = (System.DateTime?)value;
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

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            AddClass("input-group");

            var span = new HtmlElement(HtmlTag.Span);
            Append(span);
            span.AddClass("input-group-addon");
            span.Append(FontAwesome.Calendar_O.CreateElement());

            var input = new HtmlElement(HtmlTag.Input);
            Append(input);
            input.Attribute(HtmlAttribute.Type, "datetime-local");
            input.Attribute(HtmlAttribute.Name, Name);
            input.Attribute(HtmlAttribute.Value, GetValue().Replace(" ", "T"));
            if (PropertyContent.ReadOnly) PropertyContent.Disabled = true;
            SetAttributeDisabledReadOnlyPlaceHolder(input);
            input.AddClass("form-control");
            HtmlData.SetContext(input);
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");

            var i = new HtmlElement(HtmlTag.I);
            Append(i);
            i.AddClass("fa fa-calendar-o");

            Append(new HtmlText(GetValue()));
        }
    }
}
