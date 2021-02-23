using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// 表单输入组件
    /// </summary>
    public interface IFormInput : IHtmlElement
    {
        /// <summary>
        /// 脚本注册器
        /// </summary>
        ScriptRegister ScriptRegister { get; set; }

        /// <summary>
        /// 表单验证器
        /// </summary>
        FormValidate.FormValidator FormValidator { get; set; }

        /// <summary>
        /// Data 属性值
        /// </summary>
        Annotations.HtmlDataAttribute HtmlData { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info">实体项</param>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        void Init(ModelPropertyContent info, string name, object value, IList<ListDataItem> options);

        /// <summary>
        /// 设置输入模式
        /// </summary>
        void SetInputMode();

        /// <summary>
        /// 设置查看模式
        /// </summary>
        void SetViewMode();
    }
}
