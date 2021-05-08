using System.Collections.Generic;

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
        /// Select2
        /// </summary>
        public Input.Select2Resource Select2 { get; private set; }
        
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

            FontAwesome = new Document.Resource
            {
                Link = new Element.Link("//cdn.jsdelivr.net/npm/font-awesome@4.7.0/css/font-awesome.min.css")
            };

            JQuery = new Document.Resource
            {
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/gh/jquery/jquery@1.12.4/dist/jquery.min.js")
                {
                    Integrity = "sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=",
                    CrossOrigin = "anonymous"
                }
            };

            Bootstrap = new Document.Resource
            {
                Link = new Element.Link("//cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css")
                {
                    Integrity = "sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u",
                    CrossOrigin = "anonymous"
                },
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/js/bootstrap.min.js")
                {
                    Integrity = "sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa",
                    CrossOrigin = "anonymous"
                }
            };

            Sha256 = new Document.Resource
            {
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/gh/emn178/js-sha256@0.5.0/build/sha256.min.js")
                {
                    Integrity = "sha256-DzhbsixtDukR6TQaqxgDEwtXBct17h3gpz8vgzpDWbA=",
                    CrossOrigin = "anonymous"
                }
            };

            WebApp = new Document.Resource
            {
                Link = new Element.Link("//cdn.jsdelivr.net/gh/Oldmansoft/webapp/dist/oldmansoft-webapp.min.css"),
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/gh/Oldmansoft/webapp/dist/oldmansoft-webapp.min.js")
            };

            DataTables = new Document.Resource
            {
                Link = new Element.Link("//cdn.jsdelivr.net/npm/datatables@1.10.13/media/css/jquery.dataTables.min.css"),
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/npm/datatables@1.10.13/media/js/jquery.dataTables.min.js")
            };

            JQueryForm = new Document.Resource
            {
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/npm/jquery-form@4.2.1/dist/jquery.form.min.js")
                {
                    Integrity = "sha384-tIwI8+qJdZBtYYCKwRkjxBGQVZS3gGozr3CtI+5JF/oL1JmPEHzCEnIKbDbLTCer",
                    CrossOrigin = "anonymous"
                }
            };

            BootstrapValidator = new Document.Resource
            {
                Link = new Element.Link("//cdn.jsdelivr.net/npm/bootstrapvalidator@0.5.4/dist/css/bootstrapValidator.min.css"),
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/npm/bootstrapvalidator@0.5.4/dist/js/bootstrapValidator.min.js")
            };

            PluginFix = new Document.Resource
            {
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/gh/Oldmansoft/WebMan/src/WebApplicationCore/wwwroot/js/oldmansoft-plugin-fix.js")
            };

            WebMan = new Document.Resource
            {
                Link = new Element.Link("//cdn.jsdelivr.net/gh/Oldmansoft/WebMan/src/WebApplicationCore/wwwroot/css/oldmansoft-webman.css"),
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/gh/Oldmansoft/WebMan/src/WebApplicationCore/wwwroot/js/oldmansoft-webman.js")
            };

            Markdown = new Input.MarkdownResource
            {
                Link = new Element.Link("//cdn.jsdelivr.net/npm/bootstrap-markdown@2.10.0/css/bootstrap-markdown.min.css"),
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/npm/bootstrap-markdown@2.10.0/js/bootstrap-markdown.min.js"),
                Preview = new Element.ScriptResource("//cdn.jsdelivr.net/npm/markdown@0.5.0/lib/markdown.min.js")
            };

            Select2 = new Input.Select2Resource
            {
                Link = new Element.Link("//cdn.jsdelivr.net/npm/select2@4.0.3/dist/css/select2.min.css"),
                Script = new Element.ScriptResource("//cdn.jsdelivr.net/npm/select2@4.0.3/dist/js/select2.min.js")
            };
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
