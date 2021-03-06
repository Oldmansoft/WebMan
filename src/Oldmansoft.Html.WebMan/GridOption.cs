﻿namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 栅格项
    /// </summary>
    public class GridOption : HtmlElement
    {
        /// <summary>
        /// 创建栅格项
        /// </summary>
        /// <param name="column"></param>
        public GridOption(Column column = Column.Sm12)
            : base(HtmlTag.Div)
        {
            this.AddClass(column);
        }
    }
}
