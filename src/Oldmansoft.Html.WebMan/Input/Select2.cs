using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// Select2
    /// </summary>
    public class Select2 : FormInputCreator.FormInput, ICustomInput
    {
        private string Value { get; set; }

        private IList<string> Values { get; set; }
        
        void ICustomInput.Set(object[] parameter)
        {
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            if (PropertyContent.Property.PropertyType.GetInterfaces().Contains(typeof(System.Collections.IList)))
            {
                Values = value.GetListString();
            }
            else
            {
                Value = value.GetString();
            }
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
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
            else if ((PropertyContent.Property.PropertyType.IsGenericType && PropertyContent.Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) || PropertyContent.Property.PropertyType == typeof(string))
            {
                if (Value == null || PropertyContent.Required == null)
                {
                    Append(new HtmlElement(HtmlTag.Option).Append(new HtmlRaw("&nbsp;")));
                }
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
            if (PropertyContent.Disabled || PropertyContent.ReadOnly) Attribute(HtmlAttribute.Disabled, "disabled");

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
