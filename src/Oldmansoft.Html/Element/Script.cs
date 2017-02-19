using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Element
{
    /// <summary>
    /// 网页脚本
    /// </summary>
    public class Script : HtmlElement
    {
        /// <summary>
        /// 创建网页脚本
        /// </summary>
        /// <param name="script"></param>
        public Script(string script)
            : base(HtmlTag.Script)
        {
            if (string.IsNullOrEmpty(script)) return;
            Append(new HtmlRaw(script));
        }
    }
}
