using Oldmansoft.Html.WebMan.DataTables.ValueDealer;
using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 值显示
    /// </summary>
    class ValueDisplay
    {
        public static readonly ValueDisplay Instance = new ValueDisplay();

        private readonly Dictionary<Type, IValueDisplay> SimpleDealers;

        private readonly Dictionary<Type, Func<Type, object, ModelPropertyContent, HtmlNode>> GenericDealers;

        private ValueDisplay()
        {
            SimpleDealers = new Dictionary<Type, IValueDisplay>();
            Add(new GuidDisplay());
            Add(new BoolDisplay());
            Add(new DateTimeDisplay());
            Add(new StringDisplay());

            GenericDealers = new Dictionary<Type, Func<Type, object, ModelPropertyContent, HtmlNode>>
            {
                { typeof(Nullable<>), NullableDeal },
                { typeof(List<>), ListDeal }
            };
        }

        public void Add(IValueDisplay valueDisplay)
        {
            SimpleDealers.Add(valueDisplay.DealType, valueDisplay);
        }

        private HtmlNode NullableDeal(Type type, object value, ModelPropertyContent propertyContent)
        {
            return ConvertSimpleType(Nullable.GetUnderlyingType(type), value, propertyContent);
        }

        private HtmlNode ListDeal(Type type, object value, ModelPropertyContent propertyContent)
        {
            var ul = new HtmlElement(HtmlTag.Ul);

            var source = value as System.Collections.IEnumerable;
            var itemType = type.GetGenericArguments()[0];
            foreach (var item in source)
            {
                if (item == null) continue;
                var li = new HtmlElement(HtmlTag.Li);
                ul.Append(li);
                li.Append(Convert(itemType, item, propertyContent));
            }
            return ul;
        }

        public HtmlNode Convert(Type type, object value, ModelPropertyContent propertyContent)
        {
            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();
                if (GenericDealers.ContainsKey(genericType))
                {
                    return GenericDealers[genericType](type, value, propertyContent);
                }
            }
            return ConvertSimpleType(type, value, propertyContent);
        }

        private HtmlNode ConvertSimpleType(Type type, object value, ModelPropertyContent propertyContent)
        {
            if (!string.IsNullOrEmpty(propertyContent.Format))
            {
                return new HtmlText(string.Format(propertyContent.Format, value));
            }
            if (SimpleDealers.ContainsKey(type))
            {
                return SimpleDealers[type].Convert(value, propertyContent);
            }
            if (type.IsEnum)
            {
                return new HtmlText(EnumProvider.Instance.GetDescription(type, value));
            }
            return new HtmlText(value.ToString());
        }
    }
}
