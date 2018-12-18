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
        private HashSet<string> Value { get; set; }
        
        private bool WrongValueFormat { get; set; }

        private Annotations.InputMaxLengthAttribute InputMaxLength { get; set; }

        void ICustomInput.Set(object[] parameter)
        {
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            WrongValueFormat = value != null && !(value is IEnumerable<string>);
            InputMaxLength = PropertyContent.InputMaxLength;
            Value = new HashSet<string>();
            foreach (var item in value.GetListString())
            {
                Value.Add(item);
            }
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            AddClass("tagsinput");
            AddClass("form-control");
            if (PropertyContent.ReadOnly) Attribute(HtmlAttribute.ReadOnly, "readonly");
            if (PropertyContent.Disabled) Attribute(HtmlAttribute.ReadOnly, "disabled");
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

                if (!PropertyContent.Disabled && !PropertyContent.ReadOnly) FontAwesome.Times.CreateElement().AddClass("container-parent-remove").AppendTo(span);
            }

            var input = new HtmlElement(HtmlTag.Input);
            if (!PropertyContent.Disabled && !PropertyContent.ReadOnly) input.AppendTo(this);
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
