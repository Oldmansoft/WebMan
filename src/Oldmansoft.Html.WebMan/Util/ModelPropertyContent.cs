using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 实体属性内容
    /// </summary>
    public class ModelPropertyContent
    {
        /// <summary>
        /// 属性管理器
        /// </summary>
        public readonly Util.AttributeManager Attributes = new Util.AttributeManager();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType { get; private set; }

        /// <summary>
        /// 显示
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 数据类型不匹配错误消息
        /// </summary>
        public string DataTypeErrorMessage { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 格式化输出
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// 是否不可用
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 展开
        /// </summary>
        public bool Expansion { get; private set; }

        /// <summary>
        /// 是否必须
        /// </summary>
        public RequiredAttribute Required { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="property"></param>
        internal ModelPropertyContent(PropertyInfo property)
        {
            Property = property;
            Name = property.Name;
            Display = property.Name;
            Hidden = property.Name.ToLower() == "id";
            Attributes.Add(Annotations.HtmlDataAttribute.Empty);

            if (IsType(property.PropertyType, typeof(DateTime)))
            {
                DataType = DataType.DateTime;
            }
            SetRequiredAttribute(property.PropertyType);
            foreach (var attribute in property.GetCustomAttributes(typeof(Attribute), true))
            {
                SetAttribute(attribute);
            }
        }

        private void SetRequiredAttribute(Type modelType)
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
            Required = new RequiredAttribute();
        }

        private void SetAttribute(object attribute)
        {
            if (attribute is Annotations.HiddenAttribute)
            {
                Hidden = true;
                return;
            }

            if (attribute is DisplayAttribute)
            {
                Display = (attribute as DisplayAttribute).Name;
                return;
            }

            if (attribute is DataTypeAttribute)
            {
                var dataType = attribute as DataTypeAttribute;
                DataType = dataType.DataType;
                DataTypeErrorMessage = dataType.ErrorMessage;
                return;
            }

            if (attribute is Annotations.FormatAttribute)
            {
                Format = (attribute as Annotations.FormatAttribute).Format;
                return;
            }

            if (attribute is EditableAttribute)
            {
                Disabled = !(attribute as EditableAttribute).AllowEdit;
                return;
            }

            if (attribute is System.ComponentModel.ReadOnlyAttribute)
            {
                ReadOnly = (attribute as System.ComponentModel.ReadOnlyAttribute).IsReadOnly;
                return;
            }

            if (attribute is System.ComponentModel.DescriptionAttribute)
            {
                Description = (attribute as System.ComponentModel.DescriptionAttribute).Description;
                return;
            }

            if (attribute is Annotations.ExpansionAttribute)
            {
                Expansion = true;
                return;
            }

            if (attribute is RequiredAttribute)
            {
                Required = attribute as RequiredAttribute;
                return;
            }
            Attributes.Add(attribute);
        }
        
        /// <summary>
        /// 源类型像目标类型
        /// 指源类型也可以是 Nullable 的泛型为目标类型
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        internal static bool IsType(Type sourceType, Type targetType)
        {
            if (sourceType == targetType) return true;
            if (!targetType.IsClass && sourceType == typeof(Nullable<>).MakeGenericType(targetType)) return true;
            return false;
        }

        /// <summary>
        /// 设置验证
        /// </summary>
        /// <param name="form"></param>
        /// <param name="inputName"></param>
        internal void SetValidate(FormValidate.FormValidator form, string inputName)
        {
            if (ReadOnly || Disabled) return;
            var validator = form[inputName];

            if (Required != null && !Required.AllowEmptyStrings)
            {
                validator.Set(Validator.NoEmpty().SetMessage(Required));
            }

            var stringLength = Attributes.Get<StringLengthAttribute>();
            var minLength = Attributes.Get<MinLengthAttribute>();
            var maxLength = Attributes.Get<MaxLengthAttribute>();
            if (stringLength != null)
            {
                if (stringLength.MinimumLength > 0 && stringLength.MaximumLength == int.MaxValue)
                {
                    validator.Set(Validator.StringLength(stringLength.MinimumLength).SetMessage(stringLength));
                }
                else
                {
                    validator.Set(Validator.StringLength(stringLength.MinimumLength, stringLength.MaximumLength).SetMessage(stringLength));
                }
            }
            else if (minLength != null && maxLength != null)
            {
                validator.Set(Validator.StringLength(minLength.Length, maxLength.Length).SetMessage(maxLength));
            }
            else if (minLength != null)
            {
                validator.Set(Validator.StringLength(minLength.Length).SetMessage(minLength));
            }
            else if (maxLength != null)
            {
                validator.Set(Validator.StringLength(0, maxLength.Length).SetMessage(maxLength));
            }

            var regular = Attributes.Get<RegularExpressionAttribute>();
            if (regular != null && !string.IsNullOrEmpty(regular.Pattern))
            {
                validator.Set(Validator.Regexp(regular.Pattern).SetMessage(regular));
            }

            if (DataType == DataType.EmailAddress)
            {
                validator.Set(Validator.EmailAddress().Message(DataTypeErrorMessage));
            }

            var compare = Attributes.Get<CompareAttribute>();
            if (compare != null && !string.IsNullOrEmpty(compare.OtherProperty))
            {
                validator.Set(Validator.Identical(compare.OtherProperty).SetMessage(compare));
                form[compare.OtherProperty].Set(Validator.Identical(inputName).SetMessage(compare));
            }

            var range = Attributes.Get<RangeAttribute>();
            if (range != null)
            {
                validator.Set(Validator.GreaterThan(range.Minimum).SetMessage(range));
                validator.Set(Validator.LessThan(range.Maximum).SetMessage(range));
            }

            var fixedCount = Attributes.Get<Annotations.FixedCountAttribute>();
            if (fixedCount != null)
            {
                validator.Set(Validator.FixedCount(fixedCount.Value).Message(string.Format(fixedCount.ErrorMessage ?? "数量限定 {0} 个", fixedCount.Value)));
            }

            var rangeCount = Attributes.Get<Annotations.RangeCountAttribute>();
            if (rangeCount != null)
            {
                validator.Set(Validator.RangeCount(rangeCount.MinCount, rangeCount.MaxCount, rangeCount.Inclusive).Message(rangeCount.ErrorMessage ?? "数量限定有误"));
            }
        }
    }
}
