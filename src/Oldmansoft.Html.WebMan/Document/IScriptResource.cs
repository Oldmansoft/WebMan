using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Document
{
    /// <summary>
    /// 脚本资源
    /// </summary>
    public interface IScriptResource
    {
        /// <summary>
        /// 脚本
        /// </summary>
        Element.ScriptResource Script { get; set; }
    }
}
