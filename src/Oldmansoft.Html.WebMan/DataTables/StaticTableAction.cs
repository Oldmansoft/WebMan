using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class StaticTableAction<TModel> : TableAction, IStaticTableItemAction<TModel>
    {   
        /// <summary>
        /// 隐藏条件
        /// </summary>
        public Func<TModel, bool> HideCondition { get; set; }

        /// <summary>
        /// 禁用条件
        /// </summary>
        public Func<TModel, bool> DisableCondition { get; set; }

        public StaticTableAction(string text, string location, LinkBehave behave)
            : base(text, location, behave)
        {
        }

        public IStaticTableItemAction<TModel> OnClientCondition(ItemActionClient action, Func<TModel, bool> condition)
        {
            if (condition == null) return this;
            if (action == ItemActionClient.Hide)
            {
                HideCondition = condition;
            }
            else
            {
                DisableCondition = condition;
            }
            return this;
        }

        IStaticTableItemAction<TModel> IStaticTableItemAction<TModel>.Confirm(string content)
        {
            ConfirmContent = content;
            return this;
        }
    }
}
