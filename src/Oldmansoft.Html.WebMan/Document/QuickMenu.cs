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

        private IList<ILocation> Items { get; set; }
        
        /// <summary>
        /// 创建快速菜单
        /// </summary>
        public QuickMenu()
        {
            Items = new List<ILocation>();
        }
    }
}
