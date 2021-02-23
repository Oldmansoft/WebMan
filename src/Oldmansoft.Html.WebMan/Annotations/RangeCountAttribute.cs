using System.ComponentModel.DataAnnotations;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 范围数量
    /// </summary>
    public class RangeCountAttribute : ValidationAttribute
    {
        /// <summary>
        /// 最少数量
        /// </summary>
        public uint MinCount { get; private set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public uint MaxCount { get; private set; }

        /// <summary>
        /// 服务器验证
        /// </summary>
        public bool ServerValidate { get; set; }

        /// <summary>
        /// 包含
        /// </summary>
        public bool Inclusive { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="maxCount"></param>
        public RangeCountAttribute(uint maxCount)
        {
            ServerValidate = true;
            Inclusive = true;
            MinCount = 0;
            MaxCount = maxCount;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="minCount"></param>
        /// <param name="maxCount"></param>
        public RangeCountAttribute(uint minCount, uint maxCount)
        {
            ServerValidate = true;
            Inclusive = true;
            MinCount = minCount;
            MaxCount = maxCount;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (!ServerValidate) return true;

            var count = 0;
            if (value is System.Collections.IList) count = (value as System.Collections.IList).Count;
            if (MinCount > 0)
            {
                if (Inclusive)
                {
                    if (count < MinCount) return false;
                }
                else
                {
                    if (count <= MinCount) return false;
                }
            }
            if (0 < MaxCount && MaxCount < uint.MaxValue)
            {
                if (Inclusive)
                {
                    if (count > MaxCount) return false;
                }
                else
                {
                    if (count >= MaxCount) return false;
                }
            }
            return true;
        }
    }
}
