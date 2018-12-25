using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 项动作
    /// </summary>
    public interface IDynamicTableAction
    {
        /// <summary>
        /// 先确认
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        IDynamicTableAction Confirm(string content);

        /// <summary>
        /// 客户端动作执行条件
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="condition">JS 数据条件，提供 data 数据项。获取数据使用 data.属性名称</param>
        /// <returns></returns>
        IDynamicTableAction OnClientCondition(ItemActionClient action, string condition);
    }
}
