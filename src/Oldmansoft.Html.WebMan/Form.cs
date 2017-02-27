using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表单
    /// </summary>
    public class Form : HtmlElement
    {
        /// <summary>
        /// 创建表单
        /// </summary>
        public Form()
            :base(HtmlTag.Form)
        {
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="text"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public Form Add(string text, IHtmlElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            var group = new HtmlElement(HtmlTag.Div);
            Append(group);
            group.AddClass("form-group");

            var label = new HtmlElement(HtmlTag.Label);
            group.Append(label);
            label.AddClass(Col.Sm2);
            label.AddClass("control-label");
            label.Text(text);

            var content = new HtmlElement(HtmlTag.Div);
            group.Append(content);
            content.AddClass(Col.Sm10);
            content.Append(element);
            return this;
        }
    }
}
