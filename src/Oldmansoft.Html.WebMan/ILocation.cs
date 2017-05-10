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
        string Path { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        FontAwesome Icon { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        LinkBehave Behave { get; set; }

        /// <summary>
        /// 路径所指向的方法
        /// </summary>
        System.Reflection.MethodInfo Method { get; set; }

        /// <summary>
        /// 路径所指向对象的类型
        /// </summary>
        Type TargetType { get; set; }
    }
}
