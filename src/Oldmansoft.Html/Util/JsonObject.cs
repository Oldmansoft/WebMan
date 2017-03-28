using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// JSON 对象
    /// </summary>
    public class JsonObject : JsonBuilder
    {
        /// <summary>
        /// 子项
        /// </summary>
        private IDictionary<string, object> Items { get; set; }

        /// <summary>
        /// 创建 JSON 对象
        /// </summary>
        public JsonObject()
        {
            Items = new Dictionary<string, object>();
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set(string name, object value)
        {
            Items.Add(name, value);
        }

        internal override void Create(StringBuilder result)
        {
            var isStart = false;
            result.Append("{");
            foreach (var item in Items)
            {
                if (isStart) result.Append(",");
                isStart = true;
                result.AppendFormat("\"{0}\":", item.Key.JavaScriptEncode());
                Append(result, item.Value);
            }
            result.Append("}");
        }
    }
}
