using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    abstract class FormInput : HtmlElement
    {
        public string Name { get; set; }

        public bool Disabled { get; set; }

        public bool ReadOnly { get; set; }

        public FormInput(string name)
            : base(HtmlTag.Input)
        {
            Name = name;
        }

        protected void SetAttribute(IHtmlElement element)
        {
            if (Disabled)
            {
                element.Attribute(HtmlAttribute.Disabled, "disabled");
            }
            if (ReadOnly)
            {
                element.Attribute(HtmlAttribute.ReadOnly, "readOnly");
            }
        }

        /// <summary>
        /// 设置输入模式
        /// </summary>
        public abstract void SetInputMode();

        /// <summary>
        /// 设置查看模式
        /// </summary>
        public abstract void SetViewMode();
    }
}
