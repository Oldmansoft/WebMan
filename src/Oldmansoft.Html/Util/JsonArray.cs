using System.Collections.Generic;
using System.Text;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// JSON 数组
    /// </summary>
    public class JsonArray : JsonBuilder
    {
        /// <summary>
        /// 子项
        /// </summary>
        private readonly IList<object> Items;

        /// <summary>
        /// 创建 JSON 数组
        /// </summary>
        public JsonArray()
        {
            Items = new List<object>();
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="value"></param>
        public void Add(object value)
        {
            Items.Add(value);
        }

        internal override void Create(StringBuilder result)
        {
            var isStart = false;
            result.Append("[");
            foreach (var item in Items)
            {
                if (isStart) result.Append(",");
                isStart = true;
                Append(result, item);
            }
            result.Append("]");
        }
    }
}
