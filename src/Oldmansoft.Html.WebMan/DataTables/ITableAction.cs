using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格动作
    /// </summary>
    public interface ITableAction : IItemAction
    {
        /// <summary>
        /// 提交选择参数
        /// </summary>
        /// <returns></returns>
        ITableAction Post();
    }
}
