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
        /// 字体图标库
        /// </summary>
        public Document.ILinkResource FontAwesome { get; private set; }

        /// <summary>
        /// JQuery
        /// </summary>
        public Document.IScriptResource JQuery { get; private set; }
        
        /// <summary>
        /// Bootstrap
        /// </summary>
        public Document.ILinkScriptResource Bootstrap { get; private set; }
        
        /// <summary>
        /// SHA256 脚本
        /// </summary>
        public Document.IScriptResource Sha256 { get; private set; }
        
        /// <summary>
        /// WebApp
        /// </summary>
        public Document.ILinkScriptResource WebApp { get; private set; }
        
        /// <summary>
        /// DataTables
        /// </summary>
        public Document.ILinkScriptResource DataTables { get; private set; }
        
        /// <summary>
        /// jQuery Form Plugin
        /// </summary>
        public Document.IScriptResource JQueryForm { get; private set; }
        
        /// <summary>
        /// bootstrapValidator
        /// </summary>
        public Document.ILinkScriptResource BootstrapValidator { get; private set; }

        /// <summary>
        /// 插件修复
        /// </summary>
        public Document.IScriptResource PluginFix { get; private set; }
        
        /// <summary>
        /// WebMan
        /// </summary>
        public Document.ILinkScriptResource WebMan { get; private set; }

        /// <summary>
        /// Markdown
        /// </summary>
        public Input.MarkdownResource Markdown { get; private set; }
        
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

            FontAwesome = new Document.Resource();
            FontAwesome.Link = new Element.Link("https://cdn.staticfile.org/font-awesome/4.7.0/css/font-awesome.min.css");

            JQuery = new Document.Resource();
            JQuery.Script = new Element.ScriptResource("//code.jquery.com/jquery-1.12.4.min.js");
            JQuery.Script.Integrity = "sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=";
            JQuery.Script.CrossOrigin = "anonymous";

            Bootstrap = new Document.Resource();
            Bootstrap.Link = new Element.Link("//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css");
            Bootstrap.Link.Integrity = "sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u";
            Bootstrap.Link.CrossOrigin = "anonymous";
            Bootstrap.Script = new Element.ScriptResource("//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js");
            Bootstrap.Script.Integrity = "sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa";
            Bootstrap.Script.CrossOrigin = "anonymous";

            Sha256 = new Document.Resource();
            Sha256.Script = new Element.ScriptResource("//cdnjs.cloudflare.com/ajax/libs/js-sha256/0.5.0/sha256.min.js");
            Sha256.Script.Integrity = "sha256-QBdQQL9wDJVDk0eibUblj8jCArYQD+XaeFU47LnWboY=";
            Sha256.Script.CrossOrigin = "anonymous";

            WebApp = new Document.Resource();
            WebApp.Link = new Element.Link("~/Content/oldmansoft-webapp.css".ResolveUrl());
            WebApp.Script = new Element.ScriptResource("~/Scripts/oldmansoft-webapp.js".ResolveUrl());

            DataTables = new Document.Resource();
            DataTables.Link = new Element.Link("//cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css");
            DataTables.Script = new Element.ScriptResource("//cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js");

            JQueryForm = new Document.Resource();
            JQueryForm.Script = new Element.ScriptResource("//cdn.jsdelivr.net/jquery.form/4.2.1/jquery.form.min.js");
            JQueryForm.Script.Integrity = "sha384-tIwI8+qJdZBtYYCKwRkjxBGQVZS3gGozr3CtI+5JF/oL1JmPEHzCEnIKbDbLTCer";
            JQueryForm.Script.CrossOrigin = "anonymous";

            BootstrapValidator = new Document.Resource();
            BootstrapValidator.Link = new Element.Link("//cdn.bootcss.com/bootstrap-validator/0.5.3/css/bootstrapValidator.min.css");
            BootstrapValidator.Script = new Element.ScriptResource("//cdn.bootcss.com/bootstrap-validator/0.5.3/js/bootstrapValidator.min.js");

            PluginFix = new Document.Resource();
            PluginFix.Script = new Element.ScriptResource("~/Scripts/oldmansoft-plugin-fix.js".ResolveUrl());

            WebMan = new Document.Resource();
            WebMan.Link = new Element.Link("~/Content/oldmansoft-webman.css".ResolveUrl());
            WebMan.Script = new Element.ScriptResource("~/Scripts/oldmansoft-webman.js".ResolveUrl());

            Markdown = new Input.MarkdownResource();
            Markdown.Link = new Element.Link("//cdn.bootcss.com/bootstrap-markdown/2.10.0/css/bootstrap-markdown.min.css");
            Markdown.Script = new Element.ScriptResource("//cdn.bootcss.com/bootstrap-markdown/2.10.0/js/bootstrap-markdown.min.js");
            Markdown.Markdown = new Element.ScriptResource("//cdn.bootcss.com/bootstrap-markdown/1.1.4/js/markdown.min.js");
            Markdown.ToMarkdown = new Element.ScriptResource("//cdn.bootcss.com/bootstrap-markdown/1.1.4/js/to-markdown.min.js");
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
