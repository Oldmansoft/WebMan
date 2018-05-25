﻿using Oldmansoft.Html.WebMan.Annotations;
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

            List<ModelItemInfo> list = new List<ModelItemInfo>();
            GetItemInfos(list, type);
            result = list.ToArray();
            Models.TryAdd(type, result);
            return result;
        }

        private void GetItemInfos(List<ModelItemInfo> list, Type type)
        {
            foreach (var item in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var info = new ModelItemInfo(item);
                SetModelType(info, item.PropertyType);
                info.Attributes = new List<Attribute>();
                foreach (var attribute in item.GetCustomAttributes(typeof(Attribute), true))
                {
                    info.Attributes.Add(attribute as Attribute);
                    SetAttribute(info, attribute);
                }
                list.Add(info);
            }
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
            if (modelType.IsEnum)
            {
                return;
            }
            info.Required = new RequiredAttribute();
        }

        private void SetAttribute(ModelItemInfo info, object attribute)
        {
            if (attribute is HiddenAttribute)
            {
                info.Hidden = true;
                return;
            }
            
            if (attribute is RequiredAttribute)
            {
                info.Required = attribute as RequiredAttribute;
                return;
            }

            if (attribute is DisplayAttribute)
            {
                info.Display = (attribute as DisplayAttribute).Name;
                return;
            }

            if (attribute is DataTypeAttribute)
            {
                var dataType = attribute as DataTypeAttribute;
                info.DataType = dataType.DataType;
                info.DataTypeErrorMessage = dataType.ErrorMessage;
                return;
            }

            if (attribute is EditableAttribute)
            {
                info.Disabled = !(attribute as EditableAttribute).AllowEdit;
                return;
            }

            if (attribute is System.ComponentModel.ReadOnlyAttribute)
            {
                info.ReadOnly = (attribute as System.ComponentModel.ReadOnlyAttribute).IsReadOnly;
                return;
            }

            if (attribute is FileOptionAttribute)
            {
                info.FileOption = attribute as FileOptionAttribute;
                return;
            }

            if (attribute is RangeAttribute)
            {
                info.Range = attribute as RangeAttribute;
                return;
            }

            if (attribute is StringLengthAttribute)
            {
                info.StringLength = attribute as StringLengthAttribute; ;
                return;
            }

            if (attribute is CompareAttribute)
            {
                info.Compare = attribute as CompareAttribute;
                return;
            }

            if (attribute is RegularExpressionAttribute)
            {
                info.Regular = attribute as RegularExpressionAttribute;
                return;
            }

            if (attribute is RangeAttribute)
            {
                info.Range = attribute as RangeAttribute;
                return;
            }

            if (attribute is System.ComponentModel.DescriptionAttribute)
            {
                info.Description = (attribute as System.ComponentModel.DescriptionAttribute).Description;
                return;
            }

            if (attribute is CustomInputAttribute)
            {
                info.CustomInput = attribute as CustomInputAttribute;
                return;
            }

            if (attribute is HtmlDataAttribute)
            {
                info.HtmlData = attribute as HtmlDataAttribute;
                return;
            }

            if (attribute is FixedCountAttribute)
            {
                info.FixedCount = attribute as FixedCountAttribute;
                return;
            }

            if (attribute is RangeCountAttribute)
            {
                info.RangeCount = attribute as RangeCountAttribute;
                return;
            }

            if (attribute is InputMaxLengthAttribute)
            {
                info.InputMaxLength = attribute as InputMaxLengthAttribute;
                return;
            }
        }
    }
}
