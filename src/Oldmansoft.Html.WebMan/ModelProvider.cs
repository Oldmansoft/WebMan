using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class ModelProvider
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly ModelProvider Instance = new ModelProvider();

        private ConcurrentDictionary<Type, ModelItemInfo[]> Models { get; set; }

        private ModelProvider()
        {
            Models = new ConcurrentDictionary<Type, ModelItemInfo[]>();
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ModelItemInfo[] GetItems(Type type)
        {
            ModelItemInfo[] result;
            if (Models.TryGetValue(type, out result))
            {
                return result;
            }

            var list = new List<ModelItemInfo>();
            foreach (var item in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var info = new ModelItemInfo(item);
                SetModelType(info, item.PropertyType);
                foreach (var attribute in item.GetCustomAttributes(typeof(Attribute), true))
                {
                    SetAttribute(info, attribute);
                }
                list.Add(info);
            }
            result = list.ToArray();
            Models[type] = result;
            return result;
        }

        private void SetModelType(ModelItemInfo info, Type modelType)
        {
            if (modelType == typeof(string))
            {
                return;
            }
            if (modelType == typeof(bool))
            {
                return;
            }
            if (modelType.IsGenericType && modelType.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)))
            {
                return;
            }
            if (modelType.IsGenericType
                && modelType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return;
            }
            if (modelType.IsClass)
            {
                return;
            }
            info.Required = true;
        }

        private void SetAttribute(ModelItemInfo info, object attribute)
        {
            if (attribute is Annotations.HiddenAttribute)
            {
                info.Hidden = true;
                return;
            }
            
            if (attribute is RequiredAttribute)
            {
                info.Required = true;
                return;
            }

            if (attribute is DisplayAttribute)
            {
                info.Display = (attribute as DisplayAttribute).Name;
                return;
            }

            if (attribute is DataTypeAttribute)
            {
                info.DataType = (attribute as DataTypeAttribute).DataType;
                return;
            }

            if (attribute is EditableAttribute)
            {
                info.Disabled = !(attribute as EditableAttribute).AllowEdit;
                return;
            }

            if (attribute is Annotations.ReadOnlyAttribute)
            {
                info.ReadOnly = true;
                return;
            }

            if (attribute is Annotations.FileOptionAttribute)
            {
                info.FileCount = (attribute as Annotations.FileOptionAttribute).Count;
                info.FileCanDelete = (attribute as Annotations.FileOptionAttribute).CanDelete;
                return;
            }

            if (attribute is RangeAttribute)
            {
                info.Range = attribute as RangeAttribute;
                return;
            }

            if (attribute is StringLengthAttribute)
            {
                var stringLength = attribute as StringLengthAttribute;
                info.MinimumLength = stringLength.MinimumLength;
                info.MaximumLength = stringLength.MaximumLength;
                return;
            }

            if (attribute is CompareAttribute)
            {
                info.Compare = (attribute as CompareAttribute).OtherProperty;
                return;
            }

            if (attribute is RegularExpressionAttribute)
            {
                info.RegularPattern = (attribute as RegularExpressionAttribute).Pattern;
                return;
            }
        }
    }
}
