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
        /// 文件选项
        /// </summary>
        private Annotations.FileOptionAttribute FileOption { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        private HttpPostedFileCustom Value { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info">实体项信息</param>
        /// <param name="type">值类型</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        public override void Init(ModelItemInfo info, Type type, object value, IList<ListDataItem> options)
        {
            Name = info.Name;
            FileOption = info.FileOption;
            if (FileOption == null) FileOption = new Annotations.FileOptionAttribute();
            Value = value as HttpPostedFileCustom;
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readOnly"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readOnly, string hint)
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
                var a = new HtmlElement(HtmlTag.A).Text(Value.FileName).AddClass("icon-fa-text").Attribute(HtmlAttribute.Href, Value.Location).Attribute(HtmlAttribute.Target, "_none");
                HtmlData.SetContext(a);
                a.AppendTo(span);
            }
            else
            {
                span.Append(new HtmlElement(HtmlTag.I).AddClass("fa fa-file"));
            }

            var input = new HtmlElement(HtmlTag.Input);
            Append(input);
            if (!readOnly && !disabled)
            {
                input.Attribute(HtmlAttribute.Type, "file");
            }
            input.Attribute(HtmlAttribute.Name, Name);
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
                input.Attribute(HtmlAttribute.Accept, string.Join(",", list));
            }
            input.AddClass("single-file-input");

            var virtualInput = new HtmlElement(HtmlTag.Div);
            virtualInput.AddClass("form-control virtual-file-input");
            virtualInput.Text(string.IsNullOrEmpty(hint) ? "选择文件" : hint);
            virtualInput.AppendTo(this);
            SetAttribute(virtualInput, disabled, readOnly, null);

            if (Value != null && !readOnly && !disabled)
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

            SetFormValidator();
        }

        private void SetFormValidator()
        {
            var extensionsMessage = "文件扩展名必须在 \"{0}\" 里面";
            if (FileOption.ErrorMessage != null)
            {
                extensionsMessage = FileOption.ErrorMessage;
            }
            FormValidator[Name].Set(Validator.Regexp(string.Format("\\.({0})$", string.Join("|", FileOption.Extensions))).Message(string.Format(extensionsMessage, string.Join(" ", FileOption.Extensions))));

            if (FileOption.LimitContentLength > 0)
            {
                var limitContentLengthMessage = "文件大小限制为 {0}";
                if (FileOption.ErrorMessage != null) limitContentLengthMessage = FileOption.ErrorMessage;
                FormValidator[Name].Set(Validator.FileLimitContentLength(FileOption.LimitContentLength).Message(string.Format(limitContentLengthMessage, FileOption.LimitContentLength.ToSpaceVolumeString())));
            }
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
            a.Text(Value.FileName);
            a.AddClass("icon-fa-text");
            a.Attribute(HtmlAttribute.Href, Value.Location);
            a.Attribute(HtmlAttribute.Target, "_none");
            HtmlData.SetContext(a);
            a.AppendTo(this);
        }
    }
}
