using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 多行文本组件
    /// </summary>
    public class Textarea : FormInput
    {
        private string Name { get; set; }

        private string Value { get; set; }
        
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
            Value = value.GetString();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Tag = HtmlTag.Textarea;
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Rows, "5");
            SetAttribute(this, disabled, readony, hint);
            Text(Value);
            AddClass("form-control");
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value textarea");
            Append(new HtmlRaw(Value.HtmlEncode().Replace("\r\n", "<br/>").Replace("\n", "<br/>")));
        }
    }
}
