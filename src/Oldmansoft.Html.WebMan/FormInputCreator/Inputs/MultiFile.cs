using Oldmansoft.Html.WebMan.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 多文件组件
    /// </summary>
    public class MultiFile : FormInput
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
        public List<HttpPostedFileCustom> Value { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">值类型</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        /// <param name="scripts">脚本</param>
        /// <param name="formValidator">验证器</param>
        public override void Init(string name, Type type, object value, IList<ListDataItem> options, ScriptRegister scripts, FormValidate.FormValidator formValidator)
        {
            Name = name;
            FormValidator = formValidator;
            if (FileOption == null) FileOption = new Annotations.FileOptionAttribute();
            Value = new List<HttpPostedFileCustom>();
            if (value == null) return;
            if (value is IEnumerable<HttpPostedFileBase>)
            {
                foreach (var item in (value as IEnumerable<HttpPostedFileBase>))
                {
                    if (item is HttpPostedFileCustom) Value.Add(item as HttpPostedFileCustom);
                }
            }
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
            AddClass("mulit-file-group");
            SetFileInput(disabled, readony, hint);
            foreach (var item in Value)
            {
                var group = new HtmlElement(HtmlTag.Div);
                group.AppendTo(this);
                group.AddClass("input-group control-line");

                var control = new HtmlElement(HtmlTag.Div);
                control.AppendTo(group);
                control.AddClass("form-control");

                var icon = Util.ContentTypeMapping.Instance.ToIcon(item.ContentType, item.FileName);
                icon.CreateElement().AppendTo(control);
                var a = new HtmlElement(HtmlTag.A);
                a.AppendTo(control);
                a.Text(item.FileName);
                a.AddClass("icon-fa-text");
                a.Attribute(HtmlAttribute.Href, item.Location);
                a.Attribute(HtmlAttribute.Target, "_blank");

                var delInput = new HtmlElement(HtmlTag.Input);
                delInput.AppendTo(group);
                delInput.Attribute(HtmlAttribute.Type, "hidden");
                delInput.Attribute(HtmlAttribute.Name, string.Format("{0}_DeleteMark", Name));
                delInput.AddClass("del-file-input");
                delInput.Attribute(HtmlAttribute.Value, "0");

                if (FileOption.SupportDelete)
                {
                    var span = new HtmlElement(HtmlTag.Span);
                    span.AppendTo(group);
                    span.AddClass("input-group-addon del-files");
                    span.Append(FontAwesome.Times.CreateElement());
                }
            }
            SetFormValidator();
        }

        private void SetFileInput(bool disabled, bool readony, string hint)
        {
            var group = new HtmlElement(HtmlTag.Div);
            group.AppendTo(this);
            group.AddClass("input-group control-line");

            var span = new HtmlElement(HtmlTag.Span);
            span.AppendTo(group);
            span.AddClass("input-group-addon");
            span.Append(new HtmlElement(HtmlTag.I).AddClass("fa fa-files-o"));

            var templateInput = new HtmlElement(HtmlTag.Input);
            templateInput.AppendTo(group);
            templateInput.Attribute(HtmlAttribute.Type, "file");
            templateInput.Attribute(HtmlAttribute.Name, Name);
            templateInput.Data("temporary", "temporary");
            templateInput.Data("temporary-for", Name);
            if (FileOption.Accept != Annotations.ContentType.None)
            {
                var list = new List<string>();
                foreach (var item in FileOption.Accept.ToArray())
                {
                    var contentType = item.ToString().ToLower().Replace('_', '-');
                    if (FileOption.Extensions.Length == 0)
                    {
                        list.Add(string.Format("{0}/*", contentType));
                    }
                    else
                    {
                        foreach (var extension in FileOption.Extensions)
                        {
                            list.Add(string.Format("{0}/{1}", contentType, extension.ToLower()));
                        }
                    }
                }
                templateInput.Attribute(HtmlAttribute.Accept, string.Join(",", list));
            }
            templateInput.AddClass("template-mulit-file-input");

            var virtualInput = new HtmlElement(HtmlTag.Div);
            virtualInput.AddClass("form-control virtual-mulit-file-input");
            virtualInput.Text(string.IsNullOrEmpty(hint) ? "选择多个文件" : hint);
            virtualInput.AppendTo(group);
            SetAttribute(virtualInput, disabled, readony, null);
        }

        private void SetFormValidator()
        {
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
            Tag = HtmlTag.Ul;
            AddClass("control-value");

            foreach (var item in Value)
            {
                var li = new HtmlElement(HtmlTag.Li);
                li.AppendTo(this);

                var icon = Util.ContentTypeMapping.Instance.ToIcon(item.ContentType, item.FileName);
                li.Append(icon.CreateElement());
                var a = new HtmlElement(HtmlTag.A);
                li.Append(a);
                a.Text(item.FileName);
                a.AddClass("icon-fa-text");
                a.Attribute(HtmlAttribute.Href, item.Location);
                a.Attribute(HtmlAttribute.Target, "_blank");
            }
        }
    }
}
