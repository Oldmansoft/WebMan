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
    public class Location : ILocation
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

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
    }
}
