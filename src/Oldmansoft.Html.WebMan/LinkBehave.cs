using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 链接行为
    /// </summary>
    public enum LinkBehave
    {
        /// <summary>
        /// 内部联接
        /// </summary>
        Link,

        /// <summary>
        /// 内部弹窗
        /// </summary>
        Open,

        /// <summary>
        /// 调用
        /// </summary>
        Call,

        /// <summary>
        /// 自身
        /// </summary>
        Self,

        /// <summary>
        /// 新窗口
        /// </summary>
        Blank,

        /// <summary>
        /// 脚本
        /// </summary>
        Script
    }
}
