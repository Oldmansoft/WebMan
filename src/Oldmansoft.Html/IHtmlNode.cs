using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 结点
    /// </summary>
    public interface IHtmlNode
    {
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        void Format(IHtmlOutput outer);

        /// <summary>
        /// 子元素
        /// </summary>
        /// <returns></returns>
        IEnumerable<IHtmlNode> Children();
    }
}
