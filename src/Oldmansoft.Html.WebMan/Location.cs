using System;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 路径提供者
    /// </summary>
    public class Location : ILocation
    {
        /// <summary>
        /// 空
        /// </summary>
        public static readonly ILocation Empty = new EmptyLocation();

        /// <summary>
        /// 显示
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// 图标
        /// </summary>
        public FontAwesome Icon { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        public LinkBehave Behave { get; set; }

        /// <summary>
        /// 路径所指向的方法
        /// </summary>
        public System.Reflection.MethodInfo Method { get; set; }

        /// <summary>
        /// 路径所指向对象的类型
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="path"></param>
        private Location(string path)
        {
            Path = path;
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ILocation Create(string path)
        {
            return new Location(path);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="display"></param>
        /// <param name="path"></param>
        /// <param name="icon"></param>
        /// <param name="behave"></param>
        /// <returns></returns>
        public static ILocation Create(string display, string path, FontAwesome icon, LinkBehave behave)
        {
            return new Location(path) { Display = display, Icon = icon, Behave = behave };
        }
    }
}
