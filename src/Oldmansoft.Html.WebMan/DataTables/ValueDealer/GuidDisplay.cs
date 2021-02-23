using System;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class GuidDisplay : IValueDisplay
    {
        public Type DealType => typeof(Guid);

        public HtmlNode Convert(object value, ModelPropertyContent propertyContent)
        {
            return new HtmlText(((Guid)value).ToString("N"));
        }
    }
}
