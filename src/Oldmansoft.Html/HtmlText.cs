using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 文本结点
    /// </summary>
    public class HtmlText : HtmlNode
    {
        private string Content = string.Empty;

        /// <summary>
        /// 创建文本结点
        /// </summary>
        /// <param name="text"></param>
        public HtmlText(string text)
        {
            Content = text;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            outer.Append(Content.HtmlEncode());
        }
    }
}
