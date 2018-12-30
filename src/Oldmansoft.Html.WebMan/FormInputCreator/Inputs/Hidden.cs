using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 隐藏组件
    /// </summary>
    public class Hidden : FormInput
    {
        private string Value { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            Value = value.GetNotNullString();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Attribute(HtmlAttribute.Type, "hidden");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value);
            SetAttributeDisabledReadOnlyPlaceHolder(this, PropertyContent.Disabled);
            HtmlData.SetContext(this);
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            SetInputMode();
        }
    }
}
