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
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 属性内容
        /// </summary>
        public ModelPropertyContent PropertyContent { get; private set; }

        /// <summary>
        /// 列表源
        /// </summary>
        public IList<ListDataItem> Options { get; private set; }
        
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
        protected void SetAttributeDisabledReadOnlyPlaceHolder(IHtmlElement input, bool disabled)
        {
            if (disabled)
            {
                input.Attribute(HtmlAttribute.Disabled, "disabled");
            }
            if (PropertyContent.ReadOnly)
            {
                input.Attribute(HtmlAttribute.ReadOnly, "readonly");
            }
            if (!string.IsNullOrEmpty(PropertyContent.Description))
            {
                input.Attribute(HtmlAttribute.PlaceHolder, PropertyContent.Description);
            }
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="input"></param>
        /// <param name="disabled"></param>
        protected void SetAttributeDisabledReadOnly(IHtmlElement input, bool disabled)
        {
            if (disabled)
            {
                input.Attribute(HtmlAttribute.Disabled, "disabled");
            }
            if (PropertyContent.ReadOnly)
            {
                input.Attribute(HtmlAttribute.ReadOnly, "readonly");
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="propertyContent">属性内容</param>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        public void Init(ModelPropertyContent propertyContent, string name, object value, IList<ListDataItem> options)
        {
            Name = name;
            PropertyContent = propertyContent;
            Options = options;
            InitValue(value);
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value"></param>
        protected abstract void InitValue(object value);

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public abstract void SetInputMode();

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public abstract void SetViewMode();
    }
}
