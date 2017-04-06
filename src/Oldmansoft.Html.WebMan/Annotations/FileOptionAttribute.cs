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
        /// 能删除
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        /// 文件数量
        /// </summary>
        public FileOptionAttribute()
        {
            Count = 0;
            CanDelete = false;
        }
    }
}
