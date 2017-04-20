using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 菜单分支
    /// </summary>
    public class TreeListBranch : HtmlElement
    {
        private LinkContent Link { get; set; }

        private IHtmlElement Leafs { get; set; }

        /// <summary>
        /// 创建菜单分支
        /// </summary>
        /// <param name="link"></param>
        public TreeListBranch(LinkContent link)
            : base(HtmlTag.Li)
        {
            Link = link;
            Leafs = new HtmlElement(HtmlTag.Ul);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="branch">分支</param>
        /// <returns></returns>
        public TreeListBranch Add(TreeListBranch branch)
        {
            Leafs.Append(branch);
            return this;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="leaf">叶子</param>
        /// <returns></returns>
        public TreeListBranch Add(TreeListLeaf leaf)
        {
            Leafs.Append(leaf);
            return this;
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
