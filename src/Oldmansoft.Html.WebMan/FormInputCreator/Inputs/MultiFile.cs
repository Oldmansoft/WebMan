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
        /// <summary>
        /// 文件选项
        /// </summary>
        private Annotations.FileOptionAttribute FileOption { get; set; }
        
        /// <summary>
        /// 值
        /// </summary>
        private List<HttpPostedFileCustom> Value { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            FileOption = PropertyContent.FileOption;
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
        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            AddClass("mulit-file-group");
            if (!PropertyContent.Disabled && !PropertyContent.ReadOnly) SetFileInput();
            foreach (var item in Value)
            {
                var group = new HtmlElement(HtmlTag.Div);
                group.AppendTo(this);
                group.AddClass("control-line");

                var control = new HtmlElement(HtmlTag.Div);
                control.AppendTo(group);
                control.AddClass("form-control");
                if (PropertyContent.ReadOnly) control.Attribute(HtmlAttribute.ReadOnly, "readonly");
                if (PropertyContent.Disabled) control.Attribute(HtmlAttribute.ReadOnly, "disabled");

                var icon = Util.ContentTypeMapping.Instance.ToIcon(item.ContentType, item.FileName);
                icon.CreateElement().AppendTo(control);
                var a = new HtmlElement(HtmlTag.A);
                a.Text(item.FileName);
                a.AddClass("icon-fa-text");
                a.Attribute(HtmlAttribute.Href, item.Location);
                a.Attribute(HtmlAttribute.Target, "_none");
                HtmlData.SetContext(a);
                a.AppendTo(control);

                var delInput = new HtmlElement(HtmlTag.Input);
                delInput.AppendTo(group);
                delInput.Attribute(HtmlAttribute.Type, "hidden");
                delInput.Attribute(HtmlAttribute.Name, string.Format("{0}_DeleteMark", PropertyContent.Name));
                delInput.AddClass("del-file-input");
                delInput.Attribute(HtmlAttribute.Value, "0");

                if (FileOption.SupportDelete && (!PropertyContent.Disabled && !PropertyContent.ReadOnly))
                {
                    group.AddClass("input-group");

                    var span = new HtmlElement(HtmlTag.Span);
                    span.AppendTo(group);
                    span.AddClass("input-group-addon del-files");
                    span.Append(FontAwesome.Times.CreateElement());
                }
            }
            SetFormValidator();
        }

        private void SetFileInput()
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
            templateInput.Attribute(HtmlAttribute.Name, PropertyContent.Name);
            templateInput.Data("temporary", "temporary");
            templateInput.Data("temporary-for", PropertyContent.Name);
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
            virtualInput.Text(string.IsNullOrEmpty(PropertyContent.Description) ? "选择多个文件" : PropertyContent.Description);
            virtualInput.AppendTo(group);
            SetAttributeDisabledReadOnly(virtualInput);
        }

        private void SetFormValidator()
        {
            var extendsionsMessage = "文件扩展名必须在 \"{0}\" 里面";
            if (FileOption.ErrorMessage != null) extendsionsMessage = FileOption.ErrorMessage;
            FormValidator[PropertyContent.Name].Set(Validator.Regexp(string.Format("\\.({0})$", string.Join("|", FileOption.Extensions))).Message(string.Format(extendsionsMessage, string.Join(" ", FileOption.Extensions))));
            
            if (FileOption.LimitContentLength > 0)
            {
                var limitContentLengthMessage = "文件大小限制为 {0}";
                if (FileOption.ErrorMessage != null) limitContentLengthMessage = FileOption.ErrorMessage;
                FormValidator[PropertyContent.Name].Set(Validator.FileLimitContentLength(FileOption.LimitContentLength).Message(string.Format(limitContentLengthMessage, FileOption.LimitContentLength.ToSpaceVolumeString())));
            }
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
                a.Text(item.FileName);
                a.AddClass("icon-fa-text");
                a.Attribute(HtmlAttribute.Href, item.Location);
                a.Attribute(HtmlAttribute.Target, "_none");
                HtmlData.SetContext(a);
                a.AppendTo(li);
            }
        }
    }
}
