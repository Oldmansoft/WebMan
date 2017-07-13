using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// 表单输入组件
    /// </summary>
    public interface IFormInput : IHtmlElement
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">值类型</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        /// <param name="scripts">脚本</param>
        /// <param name="formValidator">验证器</param>
        void Init(string name, Type type, object value, IList<ListDataItem> options, ScriptRegister scripts, FormValidate.FormValidator formValidator);

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled">是否可用</param>
        /// <param name="readony">只读</param>
        /// <param name="hint">可描述输入字段预期值的提示信息</param>
        void SetInputMode(bool disabled, bool readony, string hint);

        /// <summary>
        /// 设置查看模式
        /// </summary>
        void SetViewMode();
    }
}
