﻿using System;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class DateTimeDisplay : IValueDisplay
    {
        public Type DealType => typeof(DateTime);

        public HtmlNode Convert(object value, ModelPropertyContent propertyContent)
        {
            var time = (DateTime)value;
            if (time.Kind == DateTimeKind.Utc)
            {
                time = time.ToLocalTime();
            }

            if (propertyContent.DataType == System.ComponentModel.DataAnnotations.DataType.Date)
            {
                return new HtmlText(time.ToString("yyyy-MM-dd"));
            }

            if (propertyContent.DataType == System.ComponentModel.DataAnnotations.DataType.Time)
            {
                return new HtmlText(time.ToString("HH:mm:ss"));
            }

            return new HtmlText(time.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
