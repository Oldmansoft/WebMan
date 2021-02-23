namespace Oldmansoft.Html
{
    /// <summary>
    /// 原始 Html
    /// </summary>
    public class HtmlRaw : HtmlNode
    {
        private readonly string Content;

        /// <summary>
        /// 创建原始 Html
        /// </summary>
        /// <param name="html"></param>
        public HtmlRaw(string html)
        {
            Content = html;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            outer.Append(Content);
        }
    }
}
