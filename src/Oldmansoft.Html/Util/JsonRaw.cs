using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// JSON 原文
    /// </summary>
    public class JsonRaw : JsonBuilder
    {
        private string Content { get; set; }

        /// <summary>
        /// 创建 JSON 原文
        /// </summary>
        /// <param name="content"></param>
        public JsonRaw(string content)
        {
            Content = content;
        }

        internal override void Create(StringBuilder result)
        {
            result.Append(Content);
        }
    }
}
