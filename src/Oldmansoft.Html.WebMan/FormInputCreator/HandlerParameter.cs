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
        public ModelItemInfo ModelItem { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public ListDataSource Source { get; private set; }

        /// <summary>
        /// 脚本注册
        /// </summary>
        public Input.ScriptRegister ScriptRegister { get; private set; }

        /// <summary>
        /// 表单验证器
        /// </summary>
        public FormValidate.FormValidator FormValidator { get; private set; }

        /// <summary>
        /// 数据属性
        /// </summary>
        public Annotations.HtmlDataAttribute HtmlData { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="modelItem"></param>
        /// <param name="model"></param>
        /// <param name="source"></param>
        /// <param name="script"></param>
        /// <param name="validator"></param>
        /// <param name="htmlData"></param>
        public HandlerParameter(ModelItemInfo modelItem, object model, ListDataSource source, Input.ScriptRegister script, FormValidate.FormValidator validator, Annotations.HtmlDataAttribute htmlData)
        {
            ModelItem = modelItem;
            Value = modelItem.Property.GetValue(model);
            Source = source;
            ScriptRegister = script;
            FormValidator = validator;
            HtmlData = htmlData;
        }

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
