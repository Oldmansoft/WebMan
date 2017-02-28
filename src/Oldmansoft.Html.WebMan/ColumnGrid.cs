using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 栅格列
    /// </summary>
    public class ColumnGrid : HtmlElement
    {
        /// <summary>
        /// 创建栅格列
        /// </summary>
        /// <param name="node"></param>
        /// <param name="column"></param>
        public ColumnGrid(IHtmlElement node, Column column = Column.Sm12)
            : base(HtmlTag.Div)
        {
            this.AddClass(column);
            Append(node);
        }
    }
}
