using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// Markdown
    /// </summary>
    public class Markdown : FormInputCreator.FormInput, ICustomInput
    {
        private string Name { get; set; }

        private string Value { get; set; }

        void ICustomInput.Set(object[] parameter)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">值类型</param>
        /// <param name="value">值</param>
        /// <param name="options">列表项</param>
        public override void Init(string name, Type type, object value, IList<ListDataItem> options)
        {
            Name = name;
            Value = value.GetNotNullString();
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Tag = HtmlTag.Textarea;
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Rows, "5");
            Data("provide", "markdown");
            SetAttribute(this, disabled, readony, hint);
            Text(Value);
            AddClass("form-control");
            AddClass("markdown");

            ScriptRegister.Register("MarkdownEdit", "view.node.find('textarea.markdown').markdown({resize : 'vertical'});");
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            AddClass("markdown");
            Append(new HtmlRaw(Value.HtmlEncode().Replace("\r\n", "<br/>").Replace("\n", "<br/>")));

            ScriptRegister.Register("MarkdownView", "view.node.find('div.markdown').html(markdown.toHTML(view.node.find('div.markdown').text()));");
        }
    }
}
