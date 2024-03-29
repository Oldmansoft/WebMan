﻿using System.Collections.Generic;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 结点
    /// </summary>
    public interface IHtmlNode
    {
        /// <summary>
        /// 父节点
        /// </summary>
        IHtmlNode Parent { get; set; }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        void Format(IHtmlOutput outer);

        /// <summary>
        /// 子元素
        /// </summary>
        /// <returns></returns>
        IList<IHtmlNode> Children();
    }
}
