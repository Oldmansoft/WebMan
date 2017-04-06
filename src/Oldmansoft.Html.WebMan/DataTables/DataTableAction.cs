using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class DataTableAction
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
        /// 是否需要选中项
        /// </summary>
        public bool NeedSelectedItem { get; set; }

        public DataTableAction(string text, string location, LinkBehave behave)
        {
            Text = text;
            Location = location;
            Behave = behave;
        }
    }
}
