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
    public interface IStaticTableItemAction<TModel>
    {
        /// <summary>
        /// 先确认
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        IStaticTableItemAction<TModel> Confirm(string content);

        /// <summary>
        /// 客户端动作执行条件
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="condition">数据条件</param>
        /// <returns></returns>
        IStaticTableItemAction<TModel> OnClientCondition(ItemActionClient action, Func<TModel, bool> condition);
    }
}
