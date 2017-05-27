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
        /// 值
        /// </summary>
        public HttpPostedFileCustom Value { get; set; }
        
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
            Value = value as HttpPostedFileCustom;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Tag = HtmlTag.Div;
            AddClass("input-group");

            var span = new HtmlElement(HtmlTag.Span);
            Append(span);
            span.AddClass("input-group-addon");
            if (Value != null)
            {
                var icon = Util.ContentTypeMapping.Instance.ToIcon(Value.ContentType, Value.FileName);
                span.Append(icon.CreateElement());
                span.Append(new HtmlElement(HtmlTag.A).Text(Value.FileName).AddClass("text").Attribute(HtmlAttribute.Href, Value.Location).Attribute(HtmlAttribute.Target, "_blank"));
            }
            else
            {
                span.Append(new HtmlElement(HtmlTag.I).AddClass("fa fa-file"));
            }
            
            var input = new HtmlElement(HtmlTag.Input);
            Append(input);
            if (!readony && !disabled)
            {
                input.Attribute(HtmlAttribute.Type, "file");
            }
            input.Attribute(HtmlAttribute.Name, Name);
            SetAttribute(input, disabled, readony, hint);
            input.AddClass("form-control");
            
            if (Value != null && !readony && !disabled)
            {
                var delInput = new HtmlElement(HtmlTag.Input);
                Append(delInput);
                delInput.Attribute(HtmlAttribute.Type, "hidden");
                delInput.Attribute(HtmlAttribute.Name, string.Format("{0}_DeleteMark", Name));
                delInput.AddClass("del-file-input");
                delInput.Attribute(HtmlAttribute.Value, "0");

                if (FileOption.SupportDelete)
                {
                    span = new HtmlElement(HtmlTag.Span);
                    Append(span);
                    span.AddClass("input-group-addon del-file");
                    span.Append(FontAwesome.Times.CreateElement());
                }
            }

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
            if (Value == null) return;

            var icon = Util.ContentTypeMapping.Instance.ToIcon(Value.ContentType, Value.FileName);
            Append(icon.CreateElement());
            var a = new HtmlElement(HtmlTag.A);
            Append(a);
            a.Text(Value.FileName);
            a.AddClass("text");
            a.Attribute(HtmlAttribute.Href, Value.Location);
            a.Attribute(HtmlAttribute.Target, "_blank");
        }
    }
}
