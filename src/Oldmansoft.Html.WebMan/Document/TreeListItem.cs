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
        internal bool IsRoot { get; set; }

        private string Display { get; set; }

        private ILocation Location { get; set; }

        private FontAwesome Icon { get; set; }

        private IHtmlElement Leafs { get; set; }

        /// <summary>
        /// 创建菜单项
        /// </summary>
        /// <param name="text"></param>
        /// <param name="location"></param>
        /// <param name="icon"></param>
        public TreeListItem(string text, ILocation location, FontAwesome icon)
            : base(HtmlTag.Li)
        {
            Display = text;
            Location = location;
            Icon = icon;
            Leafs = new HtmlElement(HtmlTag.Ul);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item">项</param>
        /// <returns></returns>
        public TreeListItem Add(TreeListItem item)
        {
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
            if (Location != null) result.Attribute(HtmlAttribute.Href, Location.Path);
            if (IsRoot)
            {
                result.Append(Icon.CreateElement());
            }
            if (!string.IsNullOrEmpty(Display))
            {
                var span = new HtmlElement(HtmlTag.Span).Text(Display);
                result.Append(span);
            }
            if (Leafs.HasChild())
            {
                result.Append(FontAwesome.Plus_Circle.CreateElement().AddClass("arrow"));
            }
            return result;
        }
    }
}
