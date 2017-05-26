using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 文件组件
    /// </summary>
    public class File : FormInput
    {
        private string Name { get; set; }

        /// <summary>
        /// 验证器
        /// </summary>
        protected FormValidate.FormValidator FormValidator { get; set; }

        /// <summary>
        /// 文件选项
        /// </summary>
        public Annotations.FileOptionAttribute FileOption { get; set; }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="scripts"></param>
        /// <param name="formValidator"></param>
        public override void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts, FormValidate.FormValidator formValidator)
        {
            Name = name;
            FormValidator = formValidator;
            if (FileOption == null) FileOption = new Annotations.FileOptionAttribute();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Attribute(HtmlAttribute.Type, "file");
            Attribute(HtmlAttribute.Name, Name);
            SetAttribute(this, disabled, readony, hint);
            AddClass("form-control");

            var message = "文件扩展名必须在 \"{0}\" 里面";
            if (FileOption.ErrorMessage != null)
            {
                message = FileOption.ErrorMessage;
            }
            FormValidator[Name].Set(Validator.Regexp(string.Format("\\.({0})$", string.Join("|", FileOption.Extensions))).Message(string.Format(message, string.Join(" ", FileOption.Extensions))));
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
        }
    }
}
