using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.DataTables.ValueDealer;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 值显示
    /// </summary>
    class ValueDisplay
    {
        public static readonly ValueDisplay Instance = new ValueDisplay();

        private Dictionary<Type, IValueDisplay> SimpleDealers { get; set; }

        private Dictionary<Type, Func<Type, object, ModelItemInfo, HtmlNode>> GenericDealers { get; set; }

        private ValueDisplay()
        {
            SimpleDealers = new Dictionary<Type, IValueDisplay>();
            SimpleDealers.Add(typeof(Guid), new GuidDisplay());
            SimpleDealers.Add(typeof(bool), new BoolDisplay());
            SimpleDealers.Add(typeof(DateTime), new DateTimeDisplay());
            SimpleDealers.Add(typeof(string), new StringDisplay());
            SimpleDealers.Add(typeof(System.Web.HttpPostedFileBase), new HttpPostedFileDisplay());

            GenericDealers = new Dictionary<Type, Func<Type, object, ModelItemInfo, HtmlNode>>();
            GenericDealers.Add(typeof(Nullable<>), NullableDeal);
            GenericDealers.Add(typeof(List<>), ListDeal);
        }

        private HtmlNode NullableDeal(Type type, object value, ModelItemInfo modelItem)
        {
            return ConvertSimpleType(Nullable.GetUnderlyingType(type), value, modelItem);
        }

        private HtmlNode ListDeal(Type type, object value, ModelItemInfo modelItem)
        {
            var ul = new HtmlElement(HtmlTag.Ul);

            var source = value as System.Collections.IEnumerable;
            var itemType = type.GetGenericArguments()[0];
            foreach (var item in source)
            {
                if (item == null) continue;
                var li = new HtmlElement(HtmlTag.Li);
                ul.Append(li);
                li.Append(Convert(itemType, item, modelItem));
            }
            return ul;
        }

        public HtmlNode Convert(Type type, object value, ModelItemInfo modelItem)
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

        private HtmlNode ConvertSimpleType(Type type, object value, ModelItemInfo modelItem)
        {
            if (SimpleDealers.ContainsKey(type))
            {
                return SimpleDealers[type].Convert(value, modelItem);
            }
            
            if (type.IsEnum)
            {
                return new HtmlText(EnumProvider.Instance.GetDescription(type, value).HtmlEncode());
            }

            return new HtmlText(value.ToString().HtmlEncode());
        }
    }
}
