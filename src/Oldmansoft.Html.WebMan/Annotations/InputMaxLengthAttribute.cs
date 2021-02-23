using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 输入限制长度
    /// </summary>
    public class InputMaxLengthAttribute : ValidationAttribute
    {
        /// <summary>
        /// 长度
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="length"></param>
        public InputMaxLengthAttribute(uint length)
        {
            Length = length;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            if (value is IList<string>)
            {
                foreach (var item in (value as IList<string>))
                {
                    if (item == null) continue;
                    if (item.Length > Length) return false;

                }
            }
            else if (value is string)
            {
                if ((value as string).Length > Length) return false;
            }
            return true;
        }
    }
}
