namespace Oldmansoft.Html.Element
{
    /// <summary>
    /// 网页文档
    /// </summary>
    public class Document : HtmlElement
    {
        private readonly IHtmlElement TitleNode;

        private string _Title;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                TitleNode.Text(_Title);
            }
        }

        /// <summary>
        /// 头
        /// </summary>
        public IHtmlElement Head { get; private set; }

        /// <summary>
        /// 身体
        /// </summary>
        public IHtmlElement Body { get; private set; }

        /// <summary>
        /// 创建网页文档
        /// </summary>
        public Document()
            : base(HtmlTag.Html)
        {
            Head = new HtmlElement(HtmlTag.Head);
            TitleNode = new HtmlElement(HtmlTag.Title);
            Head.Append(TitleNode);
            Body = new HtmlElement(HtmlTag.Body);
            Append(Head).Append(Body);
        }
        
        /// <summary>
        /// 格式化之前
        /// </summary>
        protected virtual void BeforeFormat()
        {
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            BeforeFormat();
            outer.Append("<!DOCTYPE html>");
            base.Format(outer);
        }
    }
}
