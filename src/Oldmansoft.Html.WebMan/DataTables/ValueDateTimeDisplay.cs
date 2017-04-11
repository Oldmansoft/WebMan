using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class ValueDateTimeDisplay : IValueDisplay
    {
        public string Convert(object value, ModelItemInfo modelItem)
        {
            var time = (DateTime)value;
            if (time.Kind == DateTimeKind.Utc)
            {
                time = time.ToLocalTime();
            }

            if (modelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Date)
            {
                return time.ToString("yyyy-MM-dd");
            }

            if (modelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Time)
            {
                return time.ToString("HH:mm:ss");
            }
            
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
