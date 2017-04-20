using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 菜单叶子
    /// </summary>
    public class TreeListLeaf : HtmlElement
    {
        private LinkContent Link { get; set; }

        /// <summary>
        /// 创建菜单叶子
        /// </summary>
        /// <param name="link"></param>
        public TreeListLeaf(LinkContent link)
            :base(HtmlTag.Li)
        {
            Link = link;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            Append(Link.CreateElement());

            base.Format(outer);
        }
    }
}
