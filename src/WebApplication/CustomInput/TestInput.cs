using Oldmansoft.Html.WebMan.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oldmansoft.Html.WebMan;
using Oldmansoft.Html;

namespace WebApplication.CustomInput
{
    public class TestInput : Oldmansoft.Html.WebMan.FormInputCreator.FormInput, ICustomInput
    {
        private string Value { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        protected override void InitValue(object value)
        {
            if (value != null) Value = value.ToString();
        }

        void ICustomInput.Set(object[] parameter)
        {
        }

        public override void SetInputMode()
        {
            Tag = HtmlTag.Div;
            var hidden = new HtmlElement(HtmlTag.Input);
            hidden.Attribute(HtmlAttribute.Type, "hidden");
            hidden.Attribute(HtmlAttribute.Name, ModelItem.Name);
            hidden.Attribute(HtmlAttribute.Value, Value);
            hidden.AppendTo(this);

            var input = new HtmlElement(HtmlTag.Input);
            input.AddClass("form-control");
            input.OnClient(HtmlEvent.Change, "$(this).prev().val($(this).val());");
            input.AppendTo(this);
        }

        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            Text(Value);
        }
    }
}