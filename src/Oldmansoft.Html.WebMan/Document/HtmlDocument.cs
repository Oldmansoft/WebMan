using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 网页文档
    /// </summary>
    public abstract class HtmlDocument : Element.Document
    {
        /// <summary>
        /// 初始化后运行的脚本
        /// </summary>
        protected IList<Element.Script> InitAfterScripts { get; private set; }
        
        /// <summary>
        /// 文档资源
        /// </summary>
        public DocumentResource Resources { get; private set; }

        /// <summary>
        /// 创建网页文档
        /// </summary>
        /// <param name="webRootPath"></param>
        public HtmlDocument(string webRootPath)
        {
            InitAfterScripts = new List<Element.Script>();
            Resources = new DocumentResource(webRootPath);

            Head.Append(new HtmlElement(HtmlTag.Meta).Attribute(HtmlAttribute.Http_Equiv, "Content-Type").Attribute(HtmlAttribute.Content, "text/html; charset=utf-8"));
            Head.Append(new HtmlElement(HtmlTag.Meta).Attribute(HtmlAttribute.Name, "viewport").Attribute(HtmlAttribute.Content, "width=device-width, initial-scale=1, user-scalable=no"));
            Head.Append(new HtmlRaw("<!-[if IE 11]>"));
            Head.Append(new HtmlElement(HtmlTag.Meta).Attribute(HtmlAttribute.Http_Equiv, "x-ua-compatible").Attribute(HtmlAttribute.Content, "ie=11"));
            Head.Append(new HtmlRaw("<![endif]->"));
            Head.Append(new HtmlRaw("<!-[if IE 10]>"));
            Head.Append(new HtmlElement(HtmlTag.Meta).Attribute(HtmlAttribute.Http_Equiv, "x-ua-compatible").Attribute(HtmlAttribute.Content, "ie=10"));
            Head.Append(new HtmlRaw("<![endif]->"));
            Head.Append(new HtmlRaw("<!-[if IE 9]>"));
            Head.Append(new HtmlElement(HtmlTag.Meta).Attribute(HtmlAttribute.Http_Equiv, "x-ua-compatible").Attribute(HtmlAttribute.Content, "ie=9"));
            Head.Append(new HtmlRaw("<![endif]->"));
            Head.Append(new HtmlRaw("<!--[if lt IE 9]>"));
            Head.Append(new Element.Script("if(confirm('not support! please download new internel explorer.')) document.location = 'http://windows.microsoft.com/zh-cn/internet-explorer/download-ie';"));
            Head.Append(new HtmlRaw("<![endif]-->"));
        }

        /// <summary>
        /// 格式化之前
        /// </summary>
        protected override void BeforeFormat()
        {
            Head.Append(Resources.FontAwesome.Link);
            Head.Append(Resources.JQuery.Script);
            Head.Append(Resources.Bootstrap.Link);
            Head.Append(Resources.Bootstrap.Script);
            Head.Append(Resources.Sha256.Script);
            Head.Append(Resources.WebApp.Link);
            Head.Append(Resources.WebApp.Script);
            Head.Append(Resources.DataTables.Link);
            Head.Append(Resources.DataTables.Script);
            Head.Append(Resources.JQueryForm.Script);
            Head.Append(Resources.BootstrapValidator.Link);
            Head.Append(Resources.BootstrapValidator.Script);
            Head.Append(Resources.PluginFix.Script);
            Head.Append(Resources.WebMan.Link);
            Head.Append(Resources.WebMan.Script);

            if (Resources.Markdown.Enabled)
            {
                Head.Append(Resources.Markdown.Link);
                Head.Append(Resources.Markdown.Script);
                Head.Append(Resources.Markdown.Preview);
            }
            if (Resources.Select2.Enabled)
            {
                Head.Append(Resources.Select2.Link);
                Head.Append(Resources.Select2.Script);
            }

            foreach(var item in Resources.Nodes)
            {
                Head.Append(item);
            }
        }

        /// <summary>
        /// 添加初始化完后运行的脚本
        /// </summary>
        /// <param name="script"></param>
        public void AddScript(Element.Script script)
        {
            InitAfterScripts.Add(script);
        }
    }
}
