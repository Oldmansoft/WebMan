using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Document
{
    /// <summary>
    /// 链接资源
    /// </summary>
    public interface ILinkResource
    {
        /// <summary>
        /// 样式
        /// </summary>
        Element.Link Link { get; set; }
    }
}
