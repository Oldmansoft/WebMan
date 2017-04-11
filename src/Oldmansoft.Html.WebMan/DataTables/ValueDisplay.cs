using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 值显示
    /// </summary>
    class ValueDisplay
    {
        public static readonly ValueDisplay Instance = new ValueDisplay();

        private Dictionary<Type, IValueDisplay> SimpleDealers { get; set; }

        private Dictionary<Type, Func<Type, object, ModelItemInfo, string>> GenericDealers { get; set; }

        private ValueDisplay()
        {
            SimpleDealers = new Dictionary<Type, IValueDisplay>();
            SimpleDealers.Add(typeof(Guid), new ValueGuidDisplay());
            SimpleDealers.Add(typeof(bool), new ValueBoolDisplay());
            SimpleDealers.Add(typeof(DateTime), new ValueDateTimeDisplay());

            GenericDealers = new Dictionary<Type, Func<Type, object, ModelItemInfo, string>>();
            GenericDealers.Add(typeof(Nullable<>), NullableDeal);
            GenericDealers.Add(typeof(List<>), ListDeal);
        }

        private string NullableDeal(Type type, object value, ModelItemInfo modelItem)
        {
            return ConvertSimpleType(Nullable.GetUnderlyingType(type), value, modelItem);
        }

        private string ListDeal(Type type, object value, ModelItemInfo modelItem)
        {
            IHtmlElement ul = new HtmlElement(HtmlTag.Ul);

            var source = value as System.Collections.IEnumerable;
            var itemType = type.GetGenericArguments()[0];
            foreach (var item in source)
            {
                if (item == null) continue;
                var li = new HtmlElement(HtmlTag.Li);
                ul.Append(li);
                li.Append(new HtmlRaw(Convert(itemType, item, modelItem)));
            }
            var result = new HtmlOutput();
            ul.Format(result);
            return result.Complete();
        }

        public string Convert(Type type, object value, ModelItemInfo modelItem)
        {
            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();
                if (GenericDealers.ContainsKey(genericType))
                {
                    return GenericDealers[genericType](type, value, modelItem);
                }
            }
            return ConvertSimpleType(type, value, modelItem);
        }

        private string ConvertSimpleType(Type type, object value, ModelItemInfo modelItem)
        {
            if (SimpleDealers.ContainsKey(type))
            {
                return SimpleDealers[type].Convert(value, modelItem);
            }
            
            if (type.IsEnum)
            {
                return EnumProvider.Instance.GetDescription(type, value).HtmlEncode();
            }

            return value.ToString().HtmlEncode();
        }
    }
}
