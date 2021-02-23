using System.Text;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// JSON 创建器
    /// </summary>
    public abstract class JsonBuilder
    {
        /// <summary>
        /// 添加值
        /// </summary>
        /// <param name="result"></param>
        /// <param name="value"></param>
        protected void Append(StringBuilder result, object value)
        {
            if (value == null)
            {
                result.Append("null");
            }
            else if (value is string stringValue)
            {
                result.Append(string.Format("\"{0}\"", stringValue.JavaScriptEncode()));
            }
            else if (value is bool boolValue)
            {
                result.Append(boolValue ? "true" : "false");
            }
            else if (value is JsonBuilder builder)
            {
                builder.Create(result);
            }
            else
            {
                result.Append(value.ToString());
            }
        }

        internal abstract void Create(StringBuilder result);

        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = new StringBuilder();
            Create(result);
            return result.ToString();
        }
    }
}
