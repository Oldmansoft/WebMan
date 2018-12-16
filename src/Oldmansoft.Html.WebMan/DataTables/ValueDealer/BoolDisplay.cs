using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class BoolDisplay : IValueDisplay
    {
        public HtmlNode Convert(object value, ModelPropertyContent propertyContent)
        {
            return new HtmlText((bool)value ? "是" : "否");
        }
    }
}
