using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 整数组件
    /// </summary>
    public class Integer : FormInput
    {
        private object Value { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            Value = value;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Attribute(HtmlAttribute.Type, "number");
            Attribute(HtmlAttribute.Name, ModelItem.Name);
            Attribute(HtmlAttribute.Value, Value == null ? string.Empty : Value.ToString());
            SetAttributeDisabledReadOnlyPlaceHolder(this);
            AddClass("form-control");
            HtmlData.SetContext(this);
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
