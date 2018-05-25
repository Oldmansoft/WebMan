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
    /// 实体项信息
    /// </summary>
    public class ModelItemInfo
    {
        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 显示
        /// </summary>
        public string Display { get; internal set; }

        /// <summary>
        /// 是否必须
        /// </summary>
        public RequiredAttribute Required { get; internal set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType { get; internal set; }

        /// <summary>
        /// 数据类型不匹配错误消息
        /// </summary>
        public string DataTypeErrorMessage { get; internal set; }

        /// <summary>
        /// 字符串长度
        /// </summary>
        public StringLengthAttribute StringLength { get; internal set; }

        /// <summary>
        /// 比较
        /// </summary>
        public CompareAttribute Compare { get; internal set; }
        
        /// <summary>
        /// 正则表达式
        /// </summary>
        public RegularExpressionAttribute Regular { get; internal set; }
        
        /// <summary>
        /// 两值之间
        /// </summary>
        public RangeAttribute Range { get; internal set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 是否不可用
        /// </summary>
        public bool Disabled { get; internal set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly { get; internal set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hidden { get; internal set; }

        /// <summary>
        /// 属性集
        /// </summary>
        public IList<Attribute> Attributes { get; internal set; }
        
        /// <summary>
        /// 上传文件扩展名
        /// </summary>
        public Annotations.FileOptionAttribute FileOption { get; internal set; }

        /// <summary>
        /// 定制输入
        /// </summary>
        public Annotations.CustomInputAttribute CustomInput { get; internal set; }

        /// <summary>
        /// 设置 Html data 属性
        /// </summary>
        public Annotations.HtmlDataAttribute HtmlData { get; internal set; }

        /// <summary>
        /// 固定数量
        /// </summary>
        public Annotations.FixedCountAttribute FixedCount { get; internal set; }

        /// <summary>
        /// 范围数量
        /// </summary>
        public Annotations.RangeCountAttribute RangeCount { get; internal set; }

        /// <summary>
        /// 输入限制长度
        /// </summary>
        public Annotations.InputMaxLengthAttribute InputMaxLength { get; internal set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="property"></param>
        internal ModelItemInfo(PropertyInfo property)
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
        internal void SetValidate(FormValidate.FormValidator form)
        {
            if (ReadOnly || Disabled) return;
            var validator = form[Name];
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
                form[Compare.OtherProperty].Set(Validator.Identical(Name).SetMessage(Compare));
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
