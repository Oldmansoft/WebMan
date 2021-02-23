namespace Oldmansoft.Html
{
    /// <summary>
    /// Html 标签字符
    /// </summary>
    public class HtmlChar
    {
        /// <summary>
        /// 值
        /// </summary>
        public char Value { get; private set; }

        private HtmlChar(char value)
        {
            Value = value;
        }

        /// <summary>
        /// 空格
        /// </summary>
        public static readonly HtmlChar Spaces = new HtmlChar(' ');

        /// <summary>
        /// 等号
        /// </summary>
        public static readonly new HtmlChar Equals = new HtmlChar('=');

        /// <summary>
        /// 冒号
        /// </summary>
        public static readonly HtmlChar Colons = new HtmlChar(':');

        /// <summary>
        /// 分号
        /// </summary>
        public static readonly HtmlChar Semicolons = new HtmlChar(';');

        /// <summary>
        /// 双引号
        /// </summary>
        public static readonly HtmlChar DoubleQuotes = new HtmlChar('"');

        /// <summary>
        /// 尖括号开始
        /// </summary>
        public static readonly HtmlChar SingleLeftAngleQuotation = new HtmlChar('<');

        /// <summary>
        /// 尖括号结束
        /// </summary>
        public static readonly HtmlChar SingleRightAngleQuotation = new HtmlChar('>');

        /// <summary>
        /// 斜杠
        /// </summary>
        public static readonly HtmlChar Slashes = new HtmlChar('/');
    }
}
