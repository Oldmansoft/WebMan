using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 元素接口
    /// </summary>
    public interface IHtmlElementEnumerable : IHtmlElement, IEnumerable<IHtmlElement>
    {
    }
}
