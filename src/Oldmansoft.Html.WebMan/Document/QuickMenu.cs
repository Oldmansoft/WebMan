using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 快速菜单
    /// </summary>
    public class QuickMenu
    {
        /// <summary>
        /// 头像
        /// </summary>
        public AvatarInfo Avatar { get; private set; }

        internal IList<ILocation> Items { get; private set; }
        
        /// <summary>
        /// 创建快速菜单
        /// </summary>
        public QuickMenu()
        {
            Avatar = new AvatarInfo();
            Items = new List<ILocation>();
        }

        /// <summary>
        /// 添加菜单项
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public QuickMenu Add(ILocation location)
        {
            if (location == null) return this;
            Items.Add(location);
            return this;
        }

        /// <summary>
        /// 头像信息
        /// </summary>
        public class AvatarInfo
        {
            /// <summary>
            /// 显示文字
            /// </summary>
            public string Display { get; set; }

            /// <summary>
            /// 图片
            /// </summary>
            public string Photo { get; set; }
        }
    }
}
