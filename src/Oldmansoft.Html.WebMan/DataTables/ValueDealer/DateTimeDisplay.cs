using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class DateTimeDisplay : IValueDisplay
    {
        public HtmlNode Convert(object value, ModelItemInfo modelItem)
        {
            var time = (DateTime)value;
            if (time.Kind == DateTimeKind.Utc)
            {
                time = time.ToLocalTime();
            }

            if (modelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Date)
            {
                return new HtmlText(time.ToString("yyyy-MM-dd"));
            }

            if (modelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Time)
            {
                return new HtmlText(time.ToString("HH:mm:ss"));
            }

            return new HtmlText(time.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
