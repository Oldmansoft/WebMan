using System;

namespace Oldmansoft.Html.WebMan.DataTables.ValueDealer
{
    class StringDisplay : IValueDisplay
    {
        public Type DealType => typeof(string);

        public HtmlNode Convert(object value, ModelPropertyContent propertyContent)
        {
            if (propertyContent.DataType == System.ComponentModel.DataAnnotations.DataType.Password)
            {
                return new HtmlText("***");
            }

            return new HtmlText(((string)value));
        }
    }
}
