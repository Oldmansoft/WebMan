using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oldmansoft.Html.Element
{
    /// <summary>
    /// 网页脚本
    /// </summary>
    public class Script : HtmlElement
    {
        private readonly string Content;

        private readonly List<Script> Scripts;

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
            if (Content.Last() != HtmlChar.Semicolons.Value) Content = string.Format("{0};", Content);
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

        /// <summary>
        /// 是否有内容
        /// </summary>
        /// <returns></returns>
        public virtual bool HasContent()
        {
            return Content != null || Scripts.Count > 0;
        }

        /// <summary>
        /// 设置列表
        /// </summary>
        /// <param name="list"></param>
        public virtual void SetListFromContent(IList<string> list)
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
