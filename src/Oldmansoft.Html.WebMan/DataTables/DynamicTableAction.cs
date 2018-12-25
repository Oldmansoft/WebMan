using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class DynamicTableAction : TableAction, IDynamicTableAction
    {   
        /// <summary>
        /// 隐藏条件
        /// </summary>
        public string HideCondition { get; set; }

        /// <summary>
        /// 禁用条件
        /// </summary>
        public string DisableCondition { get; set; }

        public DynamicTableAction(string text, string location, LinkBehave behave)
            : base(text, location, behave)
        {
        }

        public IDynamicTableAction OnClientCondition(ItemActionClient action, string condition)
        {
            if (string.IsNullOrWhiteSpace(condition)) return this;
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

        IDynamicTableAction IDynamicTableAction.Confirm(string content)
        {
            ConfirmContent = content;
            return this;
        }
    }
}
