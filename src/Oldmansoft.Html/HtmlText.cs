namespace Oldmansoft.Html
{
    /// <summary>
    /// 文本结点
    /// </summary>
    public class HtmlText : HtmlNode
    {
        private readonly string Content;

        /// <summary>
        /// 创建文本结点
        /// </summary>
        /// <param name="text"></param>
        public HtmlText(string text)
        {
            Content = text;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            outer.Append(Content.HtmlEncode());
        }
    }
}
