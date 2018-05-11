using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 固定数量
    /// </summary>
    public class FixedCountAttribute : ValidationAttribute
    {
        /// <summary>
        /// 数量
        /// </summary>
        public uint Value { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="value"></param>
        public FixedCountAttribute(uint value)
        {
            if (value == 0) throw new ArgumentException("值不能为零");
            Value = value;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
