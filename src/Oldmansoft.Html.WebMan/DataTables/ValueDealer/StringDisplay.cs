using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Util;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class StringDisplay : IValueDisplay
    {
        public HtmlNode Convert(object value, ModelItemInfo modelItem)
        {
            if (modelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Password)
            {
                return new HtmlText("***");
            }

            return new HtmlText(((string)value).HtmlEncode());
        }
    }
}
