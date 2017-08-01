using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    class HandlerParameter
    {
        /// <summary>
        /// 实体信息
        /// </summary>
        public ModelItemInfo ModelItem { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public ListDataSource Source { get; set; }

        /// <summary>
        /// 脚本注册
        /// </summary>
        public Input.ScriptRegister ScriptRegister { get; set; }

        /// <summary>
        /// 表单验证器
        /// </summary>
        public FormValidate.FormValidator FormValidator { get; set; }

        public Annotations.HtmlDataAttribute HtmlData { get; set; }

        /// <summary>
        /// 设置输入组件属性
        /// </summary>
        /// <param name="input"></param>
        public void SetInputProperty(Input.IFormInput input)
        {
            input.ScriptRegister = ScriptRegister;
            input.FormValidator = FormValidator;
            input.HtmlData = HtmlData;
        }
    }
}
