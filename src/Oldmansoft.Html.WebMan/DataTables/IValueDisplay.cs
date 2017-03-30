using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 值显示
    /// </summary>
    internal interface IValueDisplay
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Convert(object value);
    }
}
