using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 文档资源
    /// </summary>
    public class DocumentResource
    {
        /// <summary>
        /// 文字样式
        /// </summary>
        public Element.Link LinkFontAwesome { get; set; }

        /// <summary>
        /// JQuery 脚本
        /// </summary>
        public Element.ScriptResource ScriptJQuery { get; set; }

        /// <summary>
        /// Bootstrap 样式
        /// </summary>
        public Element.Link LinkBootstrap { get; set; }

        /// <summary>
        /// Bootstrap 脚本
        /// </summary>
        public Element.ScriptResource ScriptBootstrap { get; set; }
        
        /// <summary>
        /// SHA256 脚本
        /// </summary>
        public Element.ScriptResource ScriptSha256 { get; set; }
        
        /// <summary>
        /// Webapp 样式
        /// </summary>
        public Element.Link LinkWebapp { get; set; }

        /// <summary>
        /// Webapp 脚本
        /// </summary>
        public Element.ScriptResource ScriptWebapp { get; set; }

        /// <summary>
        /// DataTables 样式
        /// </summary>
        public Element.Link LinkDataTables { get; set; }

        /// <summary>
        /// DataTables 脚本
        /// </summary>
        public Element.ScriptResource ScriptDataTables { get; set; }

        /// <summary>
        /// jQuery Form Plugin 脚本
        /// </summary>
        public Element.ScriptResource ScriptJQueryForm { get; set; }

        /// <summary>
        /// 插件修复脚本
        /// </summary>
        public Element.ScriptResource ScriptPluginFix { get; set; }

        /// <summary>
        /// Webman 样式
        /// </summary>
        public Element.Link LinkWebman { get; set; }

        /// <summary>
        /// Webman 脚本
        /// </summary>
        public Element.ScriptResource ScriptWebman { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        internal IList<IHtmlElement> Nodes { get; private set; }

        /// <summary>
        /// 创建文档资源
        /// </summary>
        public DocumentResource()
        {
            Nodes = new List<IHtmlElement>();
        }

        /// <summary>
        /// 添加样式链接
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public DocumentResource AddLink(Element.Link link)
        {
            Nodes.Add(link);
            return this;
        }

        /// <summary>
        /// 添加脚本
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public DocumentResource AddScript(Element.Script script)
        {
            Nodes.Add(script);
            return this;
        }

        /// <summary>
        /// 添加脚本链接
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public DocumentResource AddScript(Element.ScriptResource script)
        {
            Nodes.Add(script);
            return this;
        }
    }
}
