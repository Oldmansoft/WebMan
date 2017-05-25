using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class DataTableAction : ITableAction, IItemAction
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        public LinkBehave Behave { get; set; }

        /// <summary>
        /// 是否提交参数
        /// </summary>
        public bool IsSupportParameter { get; set; }

        /// <summary>
        /// 是否需要选择
        /// </summary>
        public bool IsNeedSelected { get; set; }

        /// <summary>
        /// 确认内容
        /// </summary>
        public string ConfirmContent { get; set; }
        
        /// <summary>
        /// 隐藏条件
        /// </summary>
        public string HideCondition { get; set; }

        /// <summary>
        /// 禁用条件
        /// </summary>
        public string DisableCondition { get; set; }

        public DataTableAction(string text, string location, LinkBehave behave)
        {
            Text = text;
            Location = location;
            Behave = behave;
        }

        ITableAction ITableAction.Confirm(string content)
        {
            ConfirmContent = content;
            return this;
        }

        public ITableAction SupportParameter()
        {
            IsSupportParameter = true;
            return this;
        }

        public ITableAction NeedSelected()
        {
            IsNeedSelected = true;
            return this;
        }

        public IItemAction OnClientCondition(ItemActionClient action, string condition)
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

        IItemAction IItemAction.Confirm(string content)
        {
            ConfirmContent = content;
            return this;
        }
    }
}
