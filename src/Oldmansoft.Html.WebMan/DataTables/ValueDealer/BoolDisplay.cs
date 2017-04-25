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
        public string Convert(object value, ModelItemInfo modelItem)
        {
            return (bool)value ? "是" : "否";
        }
    }
}
