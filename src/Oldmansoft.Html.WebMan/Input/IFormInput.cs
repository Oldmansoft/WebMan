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
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="scripts"></param>
        void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts);

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
