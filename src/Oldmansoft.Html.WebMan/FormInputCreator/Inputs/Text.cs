using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 文本组件
    /// </summary>
    public class Text : FormInput
    {
        private string Name { get; set; }

        private string Value { get; set; }

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
            Value = value.GetNotNullString();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readOnly"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readOnly, string hint)
        {
            Attribute(HtmlAttribute.Type, "text");
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Value, Value);
            SetAttribute(this, disabled, readOnly, hint);
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
            Text(Value);
        }
    }
}
