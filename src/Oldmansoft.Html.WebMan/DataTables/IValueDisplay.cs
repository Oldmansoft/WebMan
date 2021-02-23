using System;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 值显示
    /// </summary>
    public interface IValueDisplay
    {
        /// <summary>
        /// 处理类型
        /// </summary>
        Type DealType { get; }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyContent"></param>
        /// <returns></returns>
        HtmlNode Convert(object value, ModelPropertyContent propertyContent);
    }
}
