using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Element;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// Markdown 资源
    /// </summary>
    public class MarkdownResource : Document.IEnabledLinkScriptResource
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public Link Link { get; set; }

        /// <summary>
        /// 脚本
        /// </summary>
        public ScriptResource Script { get; set; }

        /// <summary>
        /// 附加预览脚本
        /// </summary>
        public ScriptResource Markdown { get; set; }

        /// <summary>
        /// 附加预览脚本
        /// </summary>
        public ScriptResource ToMarkdown { get; set; }
    }
}
