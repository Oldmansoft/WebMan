using System;
using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan.Util
{
    /// <summary>
    /// 属性管理员
    /// </summary>
    public class AttributeManager
    {
        private readonly Dictionary<Type, Attribute> Attributes = new Dictionary<Type, Attribute>();

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public TAttribute Get<TAttribute>()
            where TAttribute : Attribute
        {
            var type = typeof(TAttribute);
            if (Attributes.TryGetValue(type, out Attribute value)) return (TAttribute)value;
            return null;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="attribute"></param>
        public void Add(object attribute)
        {
            var type = attribute.GetType();
            if (Attributes.ContainsKey(type))
            {
                Attributes[type] = (Attribute)attribute;
            }
            else
            {
                Attributes.Add(type, (Attribute)attribute);
            }
        }
    }
}
