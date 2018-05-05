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
        private string Name { get; set; }

        private System.DateTime? Value { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">值类型</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        public override void Init(string name, System.Type type, object value, IList<ListDataItem> options)
        {
            Name = name;
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
        /// <param name="disabled"></param>
        /// <param name="readOnly"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readOnly, string hint)
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
            SetAttribute(input, disabled, readOnly, hint);
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
