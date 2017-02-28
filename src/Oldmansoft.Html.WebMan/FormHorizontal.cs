using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 横表单
    /// </summary>
    public class FormHorizontal : HtmlElement
    {
        /// <summary>
        /// 创建横表单
        /// </summary>
        public FormHorizontal()
            :base(HtmlTag.Form)
        {
            AddClass("form-horizontal");
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="text"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public FormHorizontal Add(string text, GridOption grid)
        {
            if (grid == null) throw new ArgumentNullException("grid");
            var group = new HtmlElement(HtmlTag.Div);
            Append(group);
            group.AddClass("form-group");

            var label = new HtmlElement(HtmlTag.Label);
            group.Append(label);
            label.AddClass(Column.Sm2);
            label.AddClass("control-label");
            label.Text(text);

            group.Append(grid);
            return this;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            var group = new HtmlElement(HtmlTag.Div);
            Append(group);
            group.AddClass("form-group");

            var buttons = new GridOption(Column.Sm10);
            group.Append(buttons);
            buttons.AddClass(ColumnOffset.Sm2);

            var reset = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "reset");
            buttons.Append(reset);
            reset.AddClass("btn btn-default");

            var submit = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "submit");
            buttons.Append(submit);
            submit.AddClass("btn btn-primary");
            base.Format(outer);
        }
    }
}
