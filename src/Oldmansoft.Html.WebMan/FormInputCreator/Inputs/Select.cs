﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 下拉选择组件
    /// </summary>
    public class Select : RadioList
    {
        /// <summary>
        /// 设置输入模式
        /// </summary>
        /// <param name="disabled"></param>
        /// <param name="readony"></param>
        /// <param name="hint"></param>
        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Tag = HtmlTag.Select;
            Attribute(HtmlAttribute.Name, Name);
            AddClass("form-control");
            foreach (var option in Options)
            {
                var item = new HtmlElement(HtmlTag.Option);
                Append(item);
                item.Attribute(HtmlAttribute.Value, option.Value);
                if (option.Value == Value)
                {
                    item.Attribute(HtmlAttribute.Selected, "selected");
                }
                item.Text(option.Text);
            }
            SetAttribute(this, disabled, readony, hint);
        }
    }
}
