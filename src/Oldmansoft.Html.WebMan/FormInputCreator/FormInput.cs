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
        /// 脚本注册器
        /// </summary>
        public Input.ScriptRegister ScriptRegister { get; set; }

        /// <summary>
        /// 表单验证器
        /// </summary>
        public FormValidate.FormValidator FormValidator { get; set; }

        /// <summary>
        /// Data 属性值
        /// </summary>
        public Annotations.HtmlDataAttribute HtmlData { get; set; }

        /// <summary>
        /// 创建表单输入组件
        /// </summary>
        public FormInput()
            : base(HtmlTag.Input)
        {
            HtmlData = Annotations.HtmlDataAttribute.Empty;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="input"></param>
        /// <param name="disabled"></param>
        /// <param name="readOnly"></param>
        /// <param name="hint"></param>
        protected void SetAttribute(IHtmlElement input, bool disabled, bool readOnly, string hint)
        {
            if (disabled)
            {
                input.Attribute(HtmlAttribute.Disabled, "disabled");
            }
            if (readOnly)
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
        /// <param name="info">实体项信息</param>
        /// <param name="type">值类型</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        public abstract void Init(ModelItemInfo info, Type type, object value, IList<ListDataItem> options);

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readOnly"></param>
        /// <param name="hint"></param>
        public abstract void SetInputMode(bool disabled, bool readOnly, string hint);

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public abstract void SetViewMode();
    }
}
