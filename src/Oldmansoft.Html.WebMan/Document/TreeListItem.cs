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
        private ILocation Location { get; set; }

        private IHtmlElement Leafs { get; set; }

        /// <summary>
        /// 创建菜单项
        /// </summary>
        /// <param name="location"></param>
        public TreeListItem(ILocation location)
            : base(HtmlTag.Li)
        {
            Location = location ?? throw new ArgumentNullException();
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
            Append(CreateElement());
            if (Leafs.HasChild())
            {
                Append(Leafs);
            }
            base.Format(outer);
        }

        private IHtmlElement CreateElement()
        {
            var result = new HtmlElement(HtmlTag.A);
            if (Location.Path != null) result.Attribute(HtmlAttribute.Href, Location.Path);
            result.Append(Location.Icon.CreateElement());
            if (!string.IsNullOrEmpty(Location.Display))
            {
                var span = new HtmlElement(HtmlTag.Span).Text(Location.Display);
                result.Append(span);
            }
            return result;
        }
    }
}
