using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class TreeListItem : HtmlElement
    {
        /// <summary>
        /// 位置
        /// </summary>
        public ILocation Location { get; private set; }

        private IHtmlElement Leafs { get; set; }

        /// <summary>
        /// 创建菜单项
        /// </summary>
        /// <param name="location"></param>
        public TreeListItem(ILocation location)
            : base(HtmlTag.Li)
        {
            if (location == null) throw new ArgumentNullException();
            Location = location;
            Leafs = new HtmlElement(HtmlTag.Ul);
        }

        /// <summary>
        /// 创建菜单项
        /// </summary>
        /// <param name="display"></param>
        /// <param name="icon"></param>
        public TreeListItem(string display, FontAwesome icon)
            : base(HtmlTag.Li)
        {
            Location = WebMan.Location.Create(display, null, icon, LinkBehave.Link);
            Leafs = new HtmlElement(HtmlTag.Ul);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item">项</param>
        /// <returns></returns>
        public TreeListItem Add(TreeListItem item)
        {
            if (item == null) throw new ArgumentNullException();
            Leafs.Append(item);
            return this;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            Append(Location.CreateElement());
            if (Leafs.HasChild())
            {
                Append(Leafs);
            }
            base.Format(outer);
        }
    }
}
