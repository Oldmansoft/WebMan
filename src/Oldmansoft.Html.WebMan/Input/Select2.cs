﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.FormValidate;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// Select2
    /// </summary>
    public class Select2 : FormInputCreator.FormInput, ICustomInput
    {
        private string Name { get; set; }

        private string Value { get; set; }

        private IList<string> Values { get; set; }
        
        /// <summary>
        /// 选项列表
        /// </summary>
        private IList<ListDataItem> Options { get; set; }

        void ICustomInput.Set(object[] parameter)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">值类型</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        public override void Init(string name, Type type, object value, IList<ListDataItem> options)
        {
            Name = name;
            if (type.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)))
            {
                Values = value.GetListString();
            }
            else
            {
                Value = value.GetString();
            }
            Options = options;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readOnly"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readOnly, string hint)
        {
            Tag = HtmlTag.Select;
            Attribute(HtmlAttribute.Name, Name);
            AddClass("form-control");
            Css("width", "100%");
            AddClass("select2");
            if (Values != null)
            {
                Attribute(HtmlAttribute.Multiple, "multiple");
            }
            else if (Value == null)
            {
                Append(new HtmlElement(HtmlTag.Option).Append(new HtmlRaw("&nbsp;")));
            }
            foreach (var option in Options)
            {
                var item = new HtmlElement(HtmlTag.Option);
                Append(item);
                item.Attribute(HtmlAttribute.Value, option.Value);
                if (Values != null)
                {
                    if (Values.Contains(option.Value))
                    {
                        item.Attribute(HtmlAttribute.Selected, "selected");
                    }
                }
                else
                {
                    if (option.Value == Value)
                    {
                        item.Attribute(HtmlAttribute.Selected, "selected");
                    }
                }
                item.Text(option.Text);
            }
            if (disabled || readOnly) Attribute(HtmlAttribute.Disabled, "disabled");

            ScriptRegister.Register("Select2Edit", "view.node.find('select.select2').select2();");
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            if (Values == null)
            {
                Tag = HtmlTag.Div;
                AddClass("control-value");

                var item = Options.FirstOrDefault(o => o.Value == Value.ToString());
                if (item == null) return;
                Text(item.Text);
            }
            else
            {
                Tag = HtmlTag.Ul;
                AddClass("control-value");
                foreach (var option in Options)
                {
                    if (!Values.Contains(option.Value)) continue;
                    var li = new HtmlElement(HtmlTag.Li);
                    Append(li);
                    li.Text(option.Text);
                }
            }
        }
    }
}
