using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    class HandlerParameter
    {
        /// <summary>
        /// 实体信息
        /// </summary>
        public ModelItemInfo ModelItem { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public ListDataSource Source { get; set; }

        /// <summary>
        /// 脚本
        /// </summary>
        public Input.ScriptRegister Script { get; set; }
    }
}
