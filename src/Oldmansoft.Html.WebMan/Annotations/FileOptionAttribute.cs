using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 文件属性
    /// </summary>
    public class FileOptionAttribute : Attribute
    {
        /// <summary>
        /// 数量
        /// </summary>
        public uint Count { get; set; }
        
        /// <summary>
        /// 允许上传的文件扩展名
        /// </summary>
        public string[] Extensions { get; set; }

        /// <summary>
        /// 文件数量
        /// </summary>
        public FileOptionAttribute()
        {
            Count = 0;
        }
    }
}
