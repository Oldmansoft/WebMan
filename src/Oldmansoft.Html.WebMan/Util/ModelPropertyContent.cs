using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 实体属性内容
    /// </summary>
    public class ModelPropertyContent
    {
        /// <summary>
        /// 名称
        /// </summary>
        internal string Name { get; private set; }

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
        public string DataTypeErrorMessage { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

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
        /// 字符串长度
        /// </summary>
        public StringLengthAttribute StringLength { get; private set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public MaxLengthAttribute MaxLength { get; private set; }

        /// <summary>
        /// 最小长度
        /// </summary>
        public MinLengthAttribute MinLength { get; private set; }

        /// <summary>
        /// 比较
        /// </summary>
        public CompareAttribute Compare { get; private set; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public RegularExpressionAttribute Regular { get; private set; }

        /// <summary>
        /// 两值之间
        /// </summary>
        public RangeAttribute Range { get; private set; }

        /// <summary>
        /// 上传文件扩展名
        /// </summary>
        public Annotations.FileOptionAttribute FileOption { get; private set; }

        /// <summary>
        /// 定制输入
        /// </summary>
        public Annotations.CustomInputAttribute CustomInput { get; private set; }

        /// <summary>
        /// 设置 Html data 属性
        /// </summary>
        public Annotations.HtmlDataAttribute HtmlData { get; private set; }

        /// <summary>
        /// 固定数量
        /// </summary>
        public Annotations.FixedCountAttribute FixedCount { get; private set; }

        /// <summary>
        /// 范围数量
        /// </summary>
        public Annotations.RangeCountAttribute RangeCount { get; private set; }

        /// <summary>
        /// 输入限制长度
        /// </summary>
        public Annotations.InputMaxLengthAttribute InputMaxLength { get; private set; }

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
            HtmlData = Annotations.HtmlDataAttribute.Empty;

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

            if (attribute is RequiredAttribute)
            {
                Required = attribute as RequiredAttribute;
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

            if (attribute is Annotations.FileOptionAttribute)
            {
                FileOption = attribute as Annotations.FileOptionAttribute;
                return;
            }

            if (attribute is RangeAttribute)
            {
                Range = attribute as RangeAttribute;
                return;
            }

            if (attribute is StringLengthAttribute)
            {
                StringLength = attribute as StringLengthAttribute; ;
                return;
            }

            if (attribute is MaxLengthAttribute)
            {
                MaxLength = attribute as MaxLengthAttribute;
                return;
            }

            if (attribute is MinLengthAttribute)
            {
                MinLength = attribute as MinLengthAttribute;
                return;
            }

            if (attribute is CompareAttribute)
            {
                Compare = attribute as CompareAttribute;
                return;
            }

            if (attribute is RegularExpressionAttribute)
            {
                Regular = attribute as RegularExpressionAttribute;
                return;
            }

            if (attribute is RangeAttribute)
            {
                Range = attribute as RangeAttribute;
                return;
            }

            if (attribute is System.ComponentModel.DescriptionAttribute)
            {
                Description = (attribute as System.ComponentModel.DescriptionAttribute).Description;
                return;
            }

            if (attribute is Annotations.CustomInputAttribute)
            {
                CustomInput = attribute as Annotations.CustomInputAttribute;
                return;
            }

            if (attribute is Annotations.HtmlDataAttribute)
            {
                HtmlData = attribute as Annotations.HtmlDataAttribute;
                return;
            }

            if (attribute is Annotations.FixedCountAttribute)
            {
                FixedCount = attribute as Annotations.FixedCountAttribute;
                return;
            }

            if (attribute is Annotations.RangeCountAttribute)
            {
                RangeCount = attribute as Annotations.RangeCountAttribute;
                return;
            }

            if (attribute is Annotations.InputMaxLengthAttribute)
            {
                InputMaxLength = attribute as Annotations.InputMaxLengthAttribute;
                return;
            }

            if (attribute is Annotations.ExpansionAttribute)
            {
                Expansion = true;
                return;
            }
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

            if (StringLength != null)
            {
                if (StringLength.MinimumLength > 0 && StringLength.MaximumLength == int.MaxValue)
                {
                    validator.Set(Validator.StringLength(StringLength.MinimumLength).SetMessage(StringLength));
                }
                else
                {
                    validator.Set(Validator.StringLength(StringLength.MinimumLength, StringLength.MaximumLength).SetMessage(StringLength));
                }
            }
            else if (MinLength != null && MaxLength != null)
            {
                validator.Set(Validator.StringLength(MinLength.Length, MaxLength.Length).SetMessage(MaxLength));
            }
            else if (MinLength != null)
            {
                validator.Set(Validator.StringLength(MinLength.Length).SetMessage(MinLength));
            }
            else if (MaxLength != null)
            {
                validator.Set(Validator.StringLength(0, MaxLength.Length).SetMessage(MaxLength));
            }

            if (Regular != null && !string.IsNullOrEmpty(Regular.Pattern))
            {
                validator.Set(Validator.Regexp(Regular.Pattern).SetMessage(Regular));
            }

            if (DataType == DataType.EmailAddress)
            {
                validator.Set(Validator.EmailAddress().Message(DataTypeErrorMessage));
            }

            if (Compare != null && !string.IsNullOrEmpty(Compare.OtherProperty))
            {
                validator.Set(Validator.Identical(Compare.OtherProperty).SetMessage(Compare));
                form[Compare.OtherProperty].Set(Validator.Identical(inputName).SetMessage(Compare));
            }

            if (Range != null)
            {
                validator.Set(Validator.GreaterThan(Range.Minimum).SetMessage(Range));
                validator.Set(Validator.LessThan(Range.Maximum).SetMessage(Range));
            }

            if (FixedCount != null)
            {
                validator.Set(Validator.FixedCount(FixedCount.Value).Message(string.Format(FixedCount.ErrorMessage == null ? "数量限定 {0} 个" : FixedCount.ErrorMessage, FixedCount.Value)));
            }

            if (RangeCount != null)
            {
                validator.Set(Validator.RangeCount(RangeCount.MinCount, RangeCount.MaxCount, RangeCount.Inclusive).Message(RangeCount.ErrorMessage == null ? "数量限定有误" : RangeCount.ErrorMessage));
            }
        }
    }
}
