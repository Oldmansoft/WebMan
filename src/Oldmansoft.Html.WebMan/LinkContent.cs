using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 链接内容
    /// </summary>
    public class LinkContent
    {
        /// <summary>
        /// 文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public ILocation Location { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public FontAwesome Icon { get; set; }

        /// <summary>
        /// 创建链接内容
        /// </summary>
        /// <param name="icon">图标</param>
        public LinkContent(FontAwesome icon)
        {
            Icon = icon;
        }

        /// <summary>
        /// 创建链接内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="icon"></param>
        public LinkContent(string text, FontAwesome icon)
        {
            Text = text;
            Icon = icon;
        }

        /// <summary>
        /// 创建链接内容
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="location">路径</param>
        /// <param name="icon">图标</param>
        public LinkContent(string text, ILocation location, FontAwesome icon)
        {
            Text = text;
            Location = location;
            Icon = icon;
        }
    }
}
