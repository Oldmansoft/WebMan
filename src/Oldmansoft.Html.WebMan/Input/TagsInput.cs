using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// 多标签组件
    /// </summary>
    public class TagsInput : FormInputCreator.FormInput, ICustomInput
    {
        private string Name { get; set; }

        private HashSet<string> Value { get; set; }
        
        private bool WrongValueFormat { get; set; }

        private Annotations.InputMaxLengthAttribute InputMaxLength { get; set; }

        void ICustomInput.Set(object[] parameter)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info">实体项信息</param>
        /// <param name="type">值类型</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        public override void Init(ModelItemInfo info, Type type, object value, IList<ListDataItem> options)
        {
            WrongValueFormat = value != null && !(value is IEnumerable<string>);
            Name = info.Name;
            InputMaxLength = info.InputMaxLength;
            Value = new HashSet<string>();
            foreach (var item in value.GetListString())
            {
                Value.Add(item);
            }
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
            AddClass("tagsinput");
            AddClass("form-control");
            if (readOnly) Attribute(HtmlAttribute.ReadOnly, "readonly");
            if (disabled) Attribute(HtmlAttribute.ReadOnly, "disabled");
            if (WrongValueFormat)
            {
                Text("错误数据类型，请使用字符串列表");
                return;
            }

            foreach (var item in Value)
            {
                if (string.IsNullOrWhiteSpace(item)) continue;
                var div = new HtmlElement(HtmlTag.Div);
                div.AppendTo(this);

                var hidden = new HtmlElement(HtmlTag.Input);
                hidden.AppendTo(div);
                hidden.Attribute(HtmlAttribute.Type, "hidden");
                hidden.Attribute(HtmlAttribute.Name, Name);
                hidden.Attribute(HtmlAttribute.Value, item.Trim());

                var span = new HtmlElement(HtmlTag.Span);
                span.AppendTo(div);
                span.Text(item);

                if (!disabled && !readOnly) FontAwesome.Times.CreateElement().AddClass("container-parent-remove").AppendTo(span);
            }

            var input = new HtmlElement(HtmlTag.Input);
            if (!disabled && !readOnly) input.AppendTo(this);
            input.AddClass("input");
            input.Attribute(HtmlAttribute.PlaceHolder, "add tags");
            input.Attribute(HtmlAttribute.Name, Name);
            input.Data("temporary", "temporary");
            input.Data("temporary-for", Name);
            if (InputMaxLength != null) input.Attribute(HtmlAttribute.MaxLength, InputMaxLength.Length.ToString());

            ScriptRegister.Register("TagsInputEdit", "oldmansoft.webman.setTagsInput(view, 'div.tagsinput');");
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("tagsinput");
            if (WrongValueFormat)
            {
                Text("错误数据类型，请使用字符串列表");
                return;
            }

            foreach (var item in Value)
            {
                if (string.IsNullOrWhiteSpace(item)) continue;
                var div = new HtmlElement(HtmlTag.Div);
                div.AppendTo(this);

                var span = new HtmlElement(HtmlTag.Span);
                span.AppendTo(div);
                span.Text(item);
            }
        }
    }
}
