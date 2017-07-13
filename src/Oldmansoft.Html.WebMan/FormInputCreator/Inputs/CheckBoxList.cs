using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 多选框列表组件
    /// </summary>
    public class CheckBoxList : FormInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        protected IEnumerable<string> Values { get; set; }

        /// <summary>
        /// 选项列表
        /// </summary>
        protected IList<ListDataItem> Options { get; set; }

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
            Values = value.GetListString();
            Options = options;
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
            foreach(var option in Options)
            {
                var label = new HtmlElement(HtmlTag.Label);
                Append(label);
                label.AddClass("checkbox-inline");

                var input = new HtmlElement(HtmlTag.Input);
                label.Append(input);
                input.Attribute(HtmlAttribute.Type, "checkbox");
                input.Attribute(HtmlAttribute.Name, Name);
                input.Attribute(HtmlAttribute.Value, option.Value);
                if (Values.Contains(option.Value))
                {
                    input.Attribute(HtmlAttribute.Checked, "checked");
                }
                SetAttribute(input, disabled, readony, hint);
                label.Append(new HtmlRaw(option.Text.HtmlEncode()));
            }
        }

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public override void SetViewMode()
        {
            Tag = HtmlTag.Ul;
            AddClass("control-value");
            foreach(var option in Options)
            {
                if (!Values.Contains(option.Value)) continue;
                var li = new HtmlElement(HtmlTag.Li);
                Append(li);
                li.Text(option.Text);
            }
        }
    }
}
