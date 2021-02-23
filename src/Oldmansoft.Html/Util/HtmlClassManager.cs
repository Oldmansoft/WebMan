using System;
using System.Collections.Generic;

namespace Oldmansoft.Html.Util
{
    class HtmlClassManager
    {
        private readonly SortedSet<string> Store = new SortedSet<string>(StringComparer.CurrentCultureIgnoreCase);

        public void AddClass(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            Store.Add(name.Trim());
        }

        public void RemoveClass(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            Store.Remove(name.Trim());
        }

        public void Format(IHtmlOutput outer)
        {
            if (Store.Count == 0) return;
            outer.Append(HtmlChar.Spaces);
            outer.Append(HtmlAttribute.Class);
            outer.Append(HtmlChar.Equals);
            outer.Append(HtmlChar.DoubleQuotes);
            var i = 0;
            foreach (var item in Store)
            {
                if (i > 0) outer.Append(HtmlChar.Spaces);
                outer.Append(item.HtmlEncode());
                i++;
            }
            outer.Append(HtmlChar.DoubleQuotes);
        }
    }
}
