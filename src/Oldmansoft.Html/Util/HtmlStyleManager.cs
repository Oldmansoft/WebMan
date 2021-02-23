using System;
using System.Collections.Generic;
using System.Reflection;

namespace Oldmansoft.Html.Util
{
    class HtmlStyleManager
    {
        private readonly Dictionary<string, string> Store = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        public void Css(object properties)
        {
            if (properties == null) return;
            foreach(var propertyInfo in properties.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Store[propertyInfo.Name] = propertyInfo.GetValue(properties).ToString();
            }
        }

        public string Css(string name)
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(name)) return result;
            Store.TryGetValue(name.Trim(), out result);
            return result;
        }

        public void Css(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            Store[name.Trim()] = value;
        }

        public void Format(IHtmlOutput outer)
        {
            if (Store.Count == 0) return;
            outer.Append(HtmlChar.Spaces);
            outer.Append(HtmlAttribute.Style);
            outer.Append(HtmlChar.Equals);
            outer.Append(HtmlChar.DoubleQuotes);
            foreach(var item in Store)
            {
                outer.Append(item.Key);
                outer.Append(HtmlChar.Colons);
                outer.Append(item.Value.HtmlEncode());
                outer.Append(HtmlChar.Semicolons);
            }
            outer.Append(HtmlChar.DoubleQuotes);
        }
    }
}
