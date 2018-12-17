using Oldmansoft.Html.WebMan.Annotations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Oldmansoft.Html.WebMan.Util
{
    class ModelProvider
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

        private PropertyInfo[] GetProperties(Type type)
        {
            PropertyInfo[] result;
            if (Properties.TryGetValue(type, out result))
            {
                return result;
            }

            var store = new Dictionary<Type, List<PropertyInfo>>();
            foreach (var item in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!store.ContainsKey(item.DeclaringType)) store.Add(item.DeclaringType, new List<PropertyInfo>());
                store[item.DeclaringType].Add(item);
            }
            var list = new List<PropertyInfo>();
            foreach(var item in store.Reverse())
            {
                list.AddRange(item.Value);
            }
            result = list.ToArray();
            Properties.TryAdd(type, result);
            return result;
        }

        /// <summary>
        /// 获取实体属性内容
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<ModelPropertyContent> GetItems(Type type)
        {
            List<ModelPropertyContent> list = new List<ModelPropertyContent>();
            foreach (var item in GetProperties(type))
            {
                list.Add(new ModelPropertyContent(item));
            }
            return list;
        }
    }
}
