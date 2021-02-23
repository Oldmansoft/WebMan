namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    /// <summary>
    /// 表单输入创建处理参数
    /// </summary>
    public class HandlerParameter
    {
        /// <summary>
        /// 属性内容
        /// </summary>
        public ModelPropertyContent PropertyContent { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

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
        /// <param name="propertyContent"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="source"></param>
        /// <param name="script"></param>
        /// <param name="validator"></param>
        /// <param name="htmlData"></param>
        public HandlerParameter(ModelPropertyContent propertyContent, string name, object value, ListDataSource source, Input.ScriptRegister script, FormValidate.FormValidator validator, Annotations.HtmlDataAttribute htmlData)
        {
            PropertyContent = propertyContent;
            Name = name;
            Value = value;
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
