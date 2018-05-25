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
        private string Name { get; set; }

        private string Value { get; set; }

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
            if (value != null) Value = value.ToString();
        }

        void ICustomInput.Set(object[] parameter)
        {
        }

        public override void SetInputMode(bool disabled, bool readOnly, string hint)
        {
            Tag = HtmlTag.Div;
            var hidden = new HtmlElement(HtmlTag.Input);
            hidden.Attribute(HtmlAttribute.Type, "hidden");
            hidden.Attribute(HtmlAttribute.Name, Name);
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