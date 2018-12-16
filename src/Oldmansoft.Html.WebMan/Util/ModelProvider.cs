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

            List<PropertyInfo> list = new List<PropertyInfo>();
            foreach (var item in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                list.Add(item);
            }
            result = list.ToArray();
            Properties.TryAdd(type, result);
            return result;
        }

        /// <summary>
        /// 获取实体信息
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
