using System.Collections.Generic;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 元素接口
    /// </summary>
    public interface IHtmlElementEnumerable : IHtmlElement, IEnumerable<IHtmlElement>
    {
    }
}
