using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    /// <summary>
    /// 表单输入组件
    /// </summary>
    public abstract class FormInput : HtmlElement, Input.IFormInput
    {
        /// <summary>
        /// 创建表单输入组件
        /// </summary>
        public FormInput()
            : base(HtmlTag.Input)
        {
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="input"></param>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        protected void SetAttribute(IHtmlElement input, bool disabled, bool readony, string hint)
        {
            if (disabled)
            {
                input.Attribute(HtmlAttribute.Disabled, "disabled");
            }
            if (readony)
            {
                input.Attribute(HtmlAttribute.ReadOnly, "readonly");
            }
            if (!string.IsNullOrEmpty(hint))
            {
                input.Attribute(HtmlAttribute.PlaceHolder, hint);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="scripts"></param>
        public abstract void Init(string name, object value, IList<ListDataItem> options, Input.ScriptRegister scripts);

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public abstract void SetInputMode(bool disabled, bool readony, string hint);

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public abstract void SetViewMode();
    }
}
