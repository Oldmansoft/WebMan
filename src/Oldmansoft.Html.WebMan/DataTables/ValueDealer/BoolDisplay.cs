using System;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class BoolDisplay : IValueDisplay
    {
        public Type DealType => typeof(bool);

        public HtmlNode Convert(object value, ModelPropertyContent propertyContent)
        {
            return new HtmlText((bool)value ? "是" : "否");
        }
    }
}
