﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class DataTableAction : ITableAction
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        public LinkBehave Behave { get; set; }

        /// <summary>
        /// 是否需要提交参数
        /// </summary>
        public bool NeedPost { get; set; }

        /// <summary>
        /// 确认内容
        /// </summary>
        public string ConfirmContent { get; set; }

        public DataTableAction(string text, string location, LinkBehave behave)
        {
            Text = text;
            Location = location;
            Behave = behave;
        }

        public ITableAction Confirm(string content)
        {
            ConfirmContent = content;
            return this;
        }

        public ITableAction PostSelected()
        {
            NeedPost = true;
            return this;
        }
    }
}
