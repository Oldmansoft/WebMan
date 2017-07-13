﻿using System;
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

        private ScriptRegister Scripts { get; set; }

        private bool WrongValueFormat { get; set; }

        void ICustomInput.Set(object[] parameter)
        {
        }

        /// <summary>
        /// 设值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="scripts"></param>
        /// <param name="formValidator"></param>
        public override void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts, FormValidate.FormValidator formValidator)
        {
            WrongValueFormat = value != null && !(value is IEnumerable<string>);
            Name = name;
            Scripts = scripts;

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
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Tag = HtmlTag.Div;
            AddClass("tagsinput");
            AddClass("form-control");
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

                FontAwesome.Times.CreateElement().AddClass("container-parent-remove").AppendTo(span);
            }

            var input = new HtmlElement(HtmlTag.Input);
            input.AppendTo(this);
            input.AddClass("input");
            input.Attribute(HtmlAttribute.PlaceHolder, "add tags");
            input.Data("name", Name);

            Scripts.Register("TagsInputEdit", "oldmansoft.webman.setTagsInput(view, 'div.tagsinput');");
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("tagsinput");
            AddClass("form-control");
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