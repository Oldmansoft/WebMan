using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Element
{
    /// <summary>
    /// 网页脚本
    /// </summary>
    public class Script : HtmlElement
    {
        private string Content { get; set; }

        private List<Script> Scripts { get; set; }

        /// <summary>
        /// 创建网页脚本
        /// </summary>
        /// <param name="text"></param>
        public Script(string text)
            : base(HtmlTag.Script)
        {
            Scripts = new List<Script>();

            if (string.IsNullOrWhiteSpace(text)) return;
            Content = text.Trim();
            if (Content.Last() != ';') Content = string.Format("{0};", Content);
        }

        /// <summary>
        /// 创建网页脚本
        /// </summary>
        /// <param name="script"></param>
        public Script(params Script[] script)
            : base(HtmlTag.Script)
        {
            Scripts = new List<Script>();

            if (script == null) return;
            Scripts.AddRange(script);
        }

        internal virtual bool HasContent()
        {
            return Content != null || Scripts.Count > 0;
        }

        internal virtual void SetListFromContent(IList<string> list)
        {
            if (list == null) return;
            if (Content != null)
            {
                list.Add(Content);
            }
            foreach(var item in Scripts)
            {
                item.SetListFromContent(list);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="script">脚本</param>
        public void Add(params Script[] script)
        {
            if (script == null) return;
            Scripts.AddRange(script);
        }
        
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            if (!HasContent()) return;
            
            var list = new List<string>();
            SetListFromContent(list);

            var content = new StringBuilder();
            content.AppendLine();
            foreach (var item in list)
            {
                content.AppendLine(item);
            }
            Append(new HtmlRaw(content.ToString()));
            base.Format(outer);
        }
    }
}
