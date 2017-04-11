using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 列表数据项
    /// </summary>
    public class ListDataItem
    {
        /// <summary>
        /// 文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 创建列表数据项
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public ListDataItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }
}
