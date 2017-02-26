using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 快速菜单
    /// </summary>
    public class QuickMenu
    {
        /// <summary>
        /// 文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 图像地址
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 菜单项
        /// </summary>
        public List<LinkContent> Items { get; private set; }

        /// <summary>
        /// 创建快速菜单
        /// </summary>
        public QuickMenu()
        {
            Items = new List<LinkContent>();
        }
    }
}
