using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Oldmansoft.Html.WebMan.Util
{
    /// <summary>
    /// 模型内容提供者
    /// </summary>
    public class ModelProvider
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly ModelProvider Instance = new ModelProvider();

        private ConcurrentDictionary<Type, PropertyInfo[]> Properties { get; set; }

        private ModelProvider()
        {
            Properties = new ConcurrentDictionary<Type, PropertyInfo[]>();
        }

        private PropertyInfo[] GetPropertiesFromCache(Type type)
        {
            if (Properties.TryGetValue(type, out PropertyInfo[] result))
            {
                return result;
            }

            var list = GetProperties(type);
            result = list.ToArray();
            Properties.TryAdd(type, result);
            return result;
        }

        private List<PropertyInfo> GetProperties(Type type)
        {
            var store = new Dictionary<Type, List<PropertyInfo>>();
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!store.ContainsKey(property.DeclaringType)) store.Add(property.DeclaringType, new List<PropertyInfo>());
                store[property.DeclaringType].Add(property);
            }
            var list = new List<PropertyInfo>();
            foreach (var item in store.Reverse())
            {
                list.AddRange(item.Value);
            }
            return list;
        }

        /// <summary>
        /// 获取实体属性内容
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<ModelPropertyContent> GetItems(Type type)
        {
            List<ModelPropertyContent> list = new List<ModelPropertyContent>();
            foreach (var item in GetPropertiesFromCache(type))
            {
                list.Add(new ModelPropertyContent(item));
            }
            return list;
        }
    }
}
