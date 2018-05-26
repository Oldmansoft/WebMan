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
        /// 实体项
        /// </summary>
        public ModelItemInfo ModelItem { get; private set; }

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
        protected void SetAttributeDisabledReadOnlyPlaceHolder(IHtmlElement input)
        {
            if (ModelItem.Disabled)
            {
                input.Attribute(HtmlAttribute.Disabled, "disabled");
            }
            if (ModelItem.ReadOnly)
            {
                input.Attribute(HtmlAttribute.ReadOnly, "readonly");
            }
            if (!string.IsNullOrEmpty(ModelItem.Description))
            {
                input.Attribute(HtmlAttribute.PlaceHolder, ModelItem.Description);
            }
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="input"></param>
        protected void SetAttributeDisabledReadOnly(IHtmlElement input)
        {
            if (ModelItem.Disabled)
            {
                input.Attribute(HtmlAttribute.Disabled, "disabled");
            }
            if (ModelItem.ReadOnly)
            {
                input.Attribute(HtmlAttribute.ReadOnly, "readonly");
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info">实体项信息</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        public void Init(ModelItemInfo info, object value, IList<ListDataItem> options)
        {
            ModelItem = info;
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
