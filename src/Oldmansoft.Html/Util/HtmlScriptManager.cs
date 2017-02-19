using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Util
{
    class HtmlScriptManager
    {
        private Dictionary<string, string> Store = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
        
        public void SetScript(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            Store[name.Trim()] = value;
        }

        public void Format(IHtmlOutput outer)
        {
            foreach (var item in Store)
            {
                outer.Append(HtmlChar.Spaces);
                outer.Append(item.Key.HtmlEncode());
                outer.Append(HtmlChar.Equals);
                outer.Append(HtmlChar.DoubleQuotes);
                outer.Append(item.Value.HtmlEncode());
                outer.Append(HtmlChar.DoubleQuotes);
            }
        }
    }
}
