﻿using System;
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
            AddClass("form-horizontal");
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="text"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public Form Add(string text, ColumnGrid grid)
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
    }
}
