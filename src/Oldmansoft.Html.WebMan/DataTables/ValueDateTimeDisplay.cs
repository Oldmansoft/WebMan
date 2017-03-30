using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class ValueDateTimeDisplay : IValueDisplay
    {
        public string Convert(object value)
        {
            var time = (DateTime)value;
            if (time.Kind == DateTimeKind.Utc)
            {
                time = time.ToLocalTime();
            }
            return time.ToString();
        }
    }
}
