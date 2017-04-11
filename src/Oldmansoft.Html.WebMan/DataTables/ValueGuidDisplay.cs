using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class ValueGuidDisplay : IValueDisplay
    {
        public string Convert(object value, ModelItemInfo modelItem)
        {
            return ((Guid)value).ToString("N");
        }
    }
}
