using Oldmansoft.Html.Element;

namespace Oldmansoft.Html.WebMan.Document
{
    /// <summary>
    /// 简单文档
    /// </summary>
    public class SimpleDocument : HtmlDocument
    {
        private ILocation Location { get; set; }

        /// <summary>
        /// 创建文档
        /// </summary>
        /// <param name="defaultLink"></param>
        /// <param name="webRootPath"></param>
        public SimpleDocument(ILocation defaultLink, string webRootPath = "/")
            : base(webRootPath)
        {
            Location = defaultLink;
        }

        /// <summary>
        /// 格式化之前
        /// </summary>
        protected override void BeforeFormat()
        {
            base.BeforeFormat();

            var container = new HtmlElement(HtmlTag.Div);
            container.AddClass("container-fluid");
            Body.Append(container);

            var row = new HtmlElement(HtmlTag.Div);
            row.AddClass("row");
            container.Append(row);

            var col = new HtmlElement(HtmlTag.Div);
            col.AddClass("col-sm-6 col-sm-offset-3");
            col.AddClass("simple-main");
            row.Append(col);

            var script = new Script(string.Format("$man.init('.simple-main', '{0}');", Location.Path));
            Body.Append(script);
            foreach (var item in InitAfterScripts)
            {
                Body.Append(item);
            }
        }
    }
}
