using System;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 内容格式化
    /// </summary>
    public class FormatAttribute : Attribute
    {
        /// <summary>
        /// 格式化内容
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="format"></param>
        public FormatAttribute(string format)
        {
            Format = format;
        }
    }
}
