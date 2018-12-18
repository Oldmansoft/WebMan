﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    /// <summary>
    /// 多选下拉列表组件
    /// </summary>
    public class MultiSelect : CheckBoxList
    {
        /// <summary>
        /// 设置输入模式
        /// </summary>
        public override void SetInputMode()
        {
            Tag = HtmlTag.Select;
            Attribute(HtmlAttribute.Name, Name);
            Attribute(HtmlAttribute.Multiple, "multiple");
            AddClass("form-control");
            foreach (var option in Options)
            {
                var item = new HtmlElement(HtmlTag.Option);
                Append(item);
                item.Attribute(HtmlAttribute.Value, option.Value);
                if (Values.Contains(option.Value))
                {
                    item.Attribute(HtmlAttribute.Selected, "selected");
                }
                item.Text(option.Text);
            }
            if (PropertyContent.ReadOnly) PropertyContent.Disabled = true;
            SetAttributeDisabledReadOnlyPlaceHolder(this);
            HtmlData.SetContext(this);
        }
    }
}
