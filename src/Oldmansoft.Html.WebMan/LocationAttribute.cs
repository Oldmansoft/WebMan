using System;

namespace Oldmansoft.Html.WebMan
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
        public FontAwesome Icon { get; set; }

        /// <summary>
        /// 方式
        /// </summary>
        public LinkBehave Behave { get; set; }

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
