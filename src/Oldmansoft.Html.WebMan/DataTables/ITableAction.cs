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
        /// 提供参数
        /// 客户端选择的复选框值将用变量名为 SelectedId 的数组进行传递。
        /// </summary>
        /// <returns></returns>
        ITableAction SupportParameter();

        /// <summary>
        /// 需要选择
        /// 客户端不选择复选框时，会提示用户。
        /// </summary>
        /// <returns></returns>
        ITableAction NeedSelected();
    }
}
