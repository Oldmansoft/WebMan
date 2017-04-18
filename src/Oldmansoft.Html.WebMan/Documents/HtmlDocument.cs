using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 网页文档
    /// </summary>
    public abstract class HtmlDocument : Element.Document
    {
        /// <summary>
        /// 文档资源
        /// </summary>
        public DocumentResource Resources { get; private set; }

        /// <summary>
        /// 创建网页文档
        /// </summary>
        public HtmlDocument()
        {
            Resources = new DocumentResource();

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
            Head.Append(new Element.Script("alert('not support! please download new internel explorer.'); document.location = 'http://windows.microsoft.com/zh-cn/internet-explorer/download-ie';"));
            Head.Append(new HtmlRaw("<![endif]-->"));
        }

        /// <summary>
        /// 格式化之前
        /// </summary>
        protected override void BeforeFormat()
        {
            if (Resources.LinkFontAwesome == null)
            {
                Resources.LinkFontAwesome = new Element.Link("https://cdn.staticfile.org/font-awesome/4.7.0/css/font-awesome.min.css");
            }
            Head.Append(Resources.LinkFontAwesome);

            if (Resources.ScriptJQuery == null)
            {
                Resources.ScriptJQuery = new Element.ScriptResource("//code.jquery.com/jquery-1.12.4.min.js");
                Resources.ScriptJQuery.Integrity = "sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=";
                Resources.ScriptJQuery.CrossOrigin = "anonymous";
            }
            Head.Append(Resources.ScriptJQuery);

            if (Resources.LinkBootstrap == null)
            {
                Resources.LinkBootstrap = new Element.Link("//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css");
                Resources.LinkBootstrap.Integrity = "sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u";
                Resources.LinkBootstrap.CrossOrigin = "anonymous";
            }
            Head.Append(Resources.LinkBootstrap);

            if (Resources.ScriptBootstrap == null)
            {
                Resources.ScriptBootstrap = new Element.ScriptResource("//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js");
                Resources.ScriptBootstrap.Integrity = "sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa";
                Resources.ScriptBootstrap.CrossOrigin = "anonymous";
            }
            Head.Append(Resources.ScriptBootstrap);

            if (Resources.LinkWebapp == null)
            {
                Resources.LinkWebapp = new Element.Link("~/Content/oldmansoft-webapp.css".ResolveUrl());
            }
            Head.Append(Resources.LinkWebapp);

            if (Resources.ScriptWebapp == null)
            {
                Resources.ScriptWebapp = new Element.ScriptResource("~/Scripts/oldmansoft-webapp.js".ResolveUrl());
            }
            Head.Append(Resources.ScriptWebapp);

            if (Resources.ScriptSha256 == null)
            {
                Resources.ScriptSha256 = new Element.ScriptResource("//cdnjs.cloudflare.com/ajax/libs/js-sha256/0.5.0/sha256.min.js");
                Resources.ScriptSha256.Integrity = "sha256-QBdQQL9wDJVDk0eibUblj8jCArYQD+XaeFU47LnWboY=";
                Resources.ScriptSha256.CrossOrigin = "anonymous";
            }
            Head.Append(Resources.ScriptSha256);

            if (Resources.LinkDataTables == null)
            {
                Resources.LinkDataTables = new Element.Link("//cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css");
            }
            Head.Append(Resources.LinkDataTables);

            if (Resources.ScriptDataTables == null)
            {
                Resources.ScriptDataTables = new Element.ScriptResource("//cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js");
            }
            Head.Append(Resources.ScriptDataTables);

            if (Resources.ScriptJQueryForm == null)
            {
                Resources.ScriptJQueryForm = new Element.ScriptResource("//cdn.jsdelivr.net/jquery.form/4.2.1/jquery.form.min.js");
                Resources.ScriptJQueryForm.Integrity = "sha384-tIwI8+qJdZBtYYCKwRkjxBGQVZS3gGozr3CtI+5JF/oL1JmPEHzCEnIKbDbLTCer";
                Resources.ScriptJQueryForm.CrossOrigin = "anonymous";
            }
            Head.Append(Resources.ScriptJQueryForm);

            if (Resources.LinkBootstrapValidator == null)
            {
                Resources.LinkBootstrapValidator = new Element.Link("//cdn.bootcss.com/bootstrap-validator/0.5.3/css/bootstrapValidator.min.css");
            }
            Head.Append(Resources.LinkBootstrapValidator);

            if (Resources.ScriptBootstrapValidator == null)
            {
                Resources.ScriptBootstrapValidator = new Element.ScriptResource("//cdn.bootcss.com/bootstrap-validator/0.5.3/js/bootstrapValidator.min.js");
            }
            Head.Append(Resources.ScriptBootstrapValidator);

            if (Resources.ScriptPluginFix == null)
            {
                Resources.ScriptPluginFix = new Element.ScriptResource("~/Scripts/oldmansoft-plugin-fix.js".ResolveUrl());
            }
            Head.Append(Resources.ScriptPluginFix);

            if (Resources.LinkWebman == null)
            {
                Resources.LinkWebman = new Element.Link("~/Content/oldmansoft-webman.css".ResolveUrl());
            }
            Head.Append(Resources.LinkWebman);

            if (Resources.ScriptWebman == null)
            {
                Resources.ScriptWebman = new Element.ScriptResource("~/Scripts/oldmansoft-webman.js".ResolveUrl());
            }
            Head.Append(Resources.ScriptWebman);

            foreach(var item in Resources.Nodes)
            {
                Head.Append(item);
            }
        }
    }
}
