using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Util
{
    class HtmlAttributeManager
    {
        private Dictionary<string, string> Store = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        public string Attribute(string name)
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(name)) return result;
            Store.TryGetValue(name.Trim(), out result);
            return result;
        }

        public void Attribute(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            if (name == HtmlAttribute.Class.ToString().ToLower())
            {
                throw new ArgumentException("请使用 AddClass 方法来控制此属性", "name");
            }
            if (name == HtmlAttribute.Style.ToString().ToLower())
            {
                throw new ArgumentException("请使用 Css 方法来控制此属性", "name");
            }
            Store[name.Trim()] = value;
        }

        public void RemoveAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            Store.Remove(name.Trim());
        }

        public void Format(IHtmlOutput outer)
        {
            foreach (var item in Store)
            {
                outer.Append(HtmlChar.Spaces);
                outer.Append(item.Key.HtmlEncode());
                if (item.Value != null)
                {
                    outer.Append(HtmlChar.Equals);
                    outer.Append(HtmlChar.DoubleQuotes);
                    outer.Append(item.Value.HtmlEncode());
                    outer.Append(HtmlChar.DoubleQuotes);
                }
            }
        }
    }
}
