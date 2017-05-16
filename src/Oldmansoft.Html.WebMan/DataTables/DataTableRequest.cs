using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public class DataTableRequest
    {
        /// <summary>
        /// 绘制计数器
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 第一条数据的起始位置，比如0代表第一条数据
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// 告诉服务器每页显示的条数
        /// </summary>
        public int length { get; set; }
        
        /// <summary>
        /// 获取页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (length == 0) return 1;
                return start / length + 1;
            }
        }

        /// <summary>
        /// 获取页大小
        /// </summary>
        public int PageSize
        {
            get { return length; }
        }
    }
}
