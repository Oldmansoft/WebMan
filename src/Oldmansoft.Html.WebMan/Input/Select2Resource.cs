using Oldmansoft.Html.Element;

namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// Select2 资源
    /// </summary>
    public class Select2Resource : Document.IEnabledLinkScriptResource
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
    }
}
