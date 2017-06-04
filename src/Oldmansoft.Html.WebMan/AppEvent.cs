using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 应用事件
    /// </summary>
    public enum AppEvent
    {
        /// <summary>
        /// 加载
        /// </summary>
        Load,

        /// <summary>
        /// 激活
        /// </summary>
        Active,

        /// <summary>
        /// 静眠
        /// </summary>
        Inactive,

        /// <summary>
        /// 卸载
        /// </summary>
        Unload
    }
}
