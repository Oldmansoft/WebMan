using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// 脚本注册
    /// </summary>
    public class ScriptRegister
    {
        private Dictionary<string, string> Events { get; set; }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="script"></param>
        public void Register(string name, string script)
        {
            if (Events.ContainsKey(name)) return;
            Events.Add(name, script);
        }
    }
}
