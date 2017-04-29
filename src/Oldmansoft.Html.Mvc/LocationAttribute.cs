using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Mvc
{
    /// <summary>
    /// 配置
    /// </summary>
    public class LocationAttribute : Attribute
    {
        /// <summary>
        /// 显示
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public WebMan.FontAwesome Icon { get; set; }

        /// <summary>
        /// 方式
        /// </summary>
        public WebMan.LinkBehave Behave { get; set; }

        /// <summary>
        /// 创建配置
        /// </summary>
        /// <param name="display">显示</param>
        public LocationAttribute(string display)
        {
            Display = display;
        }
    }
}
