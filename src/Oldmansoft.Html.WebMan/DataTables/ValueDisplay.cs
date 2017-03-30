﻿using System;
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

        private Dictionary<Type, Func<Type, object, string>> GenericDealers { get; set; }

        private ValueDisplay()
        {
            SimpleDealers = new Dictionary<Type, IValueDisplay>();
            SimpleDealers.Add(typeof(Guid), new ValueGuidDisplay());
            SimpleDealers.Add(typeof(bool), new ValueBoolDisplay());
            SimpleDealers.Add(typeof(DateTime), new ValueDateTimeDisplay());

            GenericDealers = new Dictionary<Type, Func<Type, object, string>>();
            GenericDealers.Add(typeof(Nullable<>), NullableDeal);
            GenericDealers.Add(typeof(List<>), ListDeal);
        }

        private string NullableDeal(Type type, object value)
        {
            return ConvertSimpleType(Nullable.GetUnderlyingType(type), value);
        }

        private string ListDeal(Type type, object value)
        {
            var result = new StringBuilder();

            var source = value as System.Collections.IEnumerable;
            var itemType = type.GetGenericArguments()[0];
            foreach (var item in source)
            {
                if (item == null) continue;
                result.Append("<i>");
                result.Append(Convert(itemType, item));
                result.Append("</i>");
            }
            return result.ToString();
        }

        public string Convert(Type type, object value)
        {
            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();
                if (GenericDealers.ContainsKey(genericType))
                {
                    return GenericDealers[genericType](type, value);
                }
            }
            return ConvertSimpleType(type, value);
        }

        private string ConvertSimpleType(Type type, object value)
        {
            if (SimpleDealers.ContainsKey(type))
            {
                return SimpleDealers[type].Convert(value);
            }

            if (!type.IsEnum)
            {
                return value.ToString().HtmlEncode();
            }

            var name = Enum.GetName(type, value);
            var attribute = type.GetMember(name)[0].GetCustomAttribute(typeof(System.ComponentModel.DescriptionAttribute), false) as System.ComponentModel.DescriptionAttribute;
            if (attribute == null)
            {
                return name.HtmlEncode();
            }
            else
            {
                return attribute.Description.HtmlEncode();
            }
        }
    }
}
