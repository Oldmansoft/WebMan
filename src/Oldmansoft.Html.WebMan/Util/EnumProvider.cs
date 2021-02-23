using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Oldmansoft.Html.WebMan.Util
{
    /// <summary>
    /// 枚举提供者
    /// </summary>
    public class EnumProvider
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static readonly EnumProvider Instance = new EnumProvider();

        private ConcurrentDictionary<object, string> Mapping { get; set; }

        private ConcurrentDictionary<Type, IList<ListDataItem>> ListSet { get; set; }

        private EnumProvider()
        {
            Mapping = new ConcurrentDictionary<object, string>();
            ListSet = new ConcurrentDictionary<Type, IList<ListDataItem>>();
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetDescription(Type type, object value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (type == null) throw new ArgumentNullException("type");
            if (!type.IsEnum) throw new ArgumentException("参数不是枚举");

            string result;
            if (Mapping.TryGetValue(value, out result))
            {
                return result;
            }

            var name = Enum.GetName(type, value);
            var attribute = type.GetMember(name)[0].GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
            if (attribute == null)
            {
                result = name;
            }
            else
            {
                result = attribute.Description;
            }
            Mapping.TryAdd(value, result);
            return result;
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetDescription(object value)
        {
            if (value == null) throw new ArgumentNullException("value");

            string result;
            if (Mapping.TryGetValue(value, out result))
            {
                return result;
            }

            return GetDescription(value.GetType(), value);
        }

        internal IList<ListDataItem> GetDataItems(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException("必须是枚举类型", "enumType");

            IList<ListDataItem> result;
            if (ListSet.TryGetValue(enumType, out result))
            {
                return result;
            }

            result = new List<ListDataItem>();
            foreach(var name in Enum.GetNames(enumType))
            {
                var item = new ListDataItem(name, name);
                var attribute = enumType.GetMember(name)[0].GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attribute != null)
                {
                    item.Text = attribute.Description;
                }
                result.Add(item);
            }
            ListSet.TryAdd(enumType, result);
            return result;
        }

        internal IList<ListDataItem> GetDataItems(Type type, IList<ListDataItem> list)
        {
            if (list != null) return list;
            if (type.IsEnum) return GetDataItems(type);
            if (IsNullableEnum(type))
            {
                var result = new List<ListDataItem>();
                result.Insert(0, new ListDataItem(" ", ""));
                foreach(var item in GetDataItems(type.GenericTypeArguments[0]))
                {
                    result.Add(item);
                }
                return result;
            }
            if (type.IsGenericType && type.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)))
            {
                var itemType = type.GetGenericArguments()[0];
                if (itemType.IsEnum)
                {
                    return GetDataItems(itemType);
                }
                if (IsNullableEnum(itemType))
                {
                    return GetDataItems(itemType.GenericTypeArguments[0]);
                }
            }
            return new List<ListDataItem>();
        }

        /// <summary>
        /// 是否为可空枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsNullableEnum(Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                && type.GenericTypeArguments[0].IsEnum;
        }

        /// <summary>
        /// 是否为枚举或可空枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsEnumOrNullable(Type type)
        {
            return type.IsEnum || IsNullableEnum(type);
        }
    }
}
