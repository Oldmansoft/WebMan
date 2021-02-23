using System;
using System.Reflection;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 空位置
    /// </summary>
    class EmptyLocation : ILocation
    {
        /// <summary>
        /// 显示
        /// </summary>
        public string Display
        {
            get { return string.Empty; }
            set { }
        }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get { return string.Empty; }
            set { }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public FontAwesome Icon
        {
            get { return FontAwesome.Address_Book; }
            set { }
        }

        /// <summary>
        /// 行为
        /// </summary>
        public LinkBehave Behave
        {
            get { return LinkBehave.Link; }
            set { }
        }

        /// <summary>
        /// 路径所指向的方法
        /// </summary>
        public MethodInfo Method
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// 路径所指向对象的类型
        /// </summary>
        public Type TargetType
        {
            get { return null; }
            set { }
        }
    }
}
