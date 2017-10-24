using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 全局设定
    /// </summary>
    public static class GlobalOption
    {
        /// <summary>
        /// 表格动作参数名称
        /// </summary>
        public static string TableSelectedParameterName { get; set; }

        static GlobalOption()
        {
            TableSelectedParameterName = "selectedId";
        }
    }
}
