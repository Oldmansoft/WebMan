using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 路径提供者
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// 显示
        /// </summary>
        string Display { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        string Path { get; }

        /// <summary>
        /// 图标
        /// </summary>
        FontAwesome Icon { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        LinkBehave Behave { get; set; }
    }
}
