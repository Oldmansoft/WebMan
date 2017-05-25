using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 数字组件
    /// </summary>
    public class Number : FormInput
    {
        private string Name { get; set; }

        private object Value { get; set; }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="scripts"></param>
        /// <param name="formValidator"></param>
        public override void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts, FormValidate.FormValidator formValidator)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Attribute(HtmlAttribute.Type, "number");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value == null ? string.Empty : Value.ToString());
            SetAttribute(this, disabled, readony, hint);
            AddClass("form-control");
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            Text(Value == null ? string.Empty : Value.ToString());
        }
    }
}
