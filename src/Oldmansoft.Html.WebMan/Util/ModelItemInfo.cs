using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Util
{
    class ModelItemInfo
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
        public string Display { get; set; }

        /// <summary>
        /// 是否必须
        /// </summary>
        public RequiredAttribute Required { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// 数据类型不匹配错误消息
        /// </summary>
        public string DataTypeErrorMessage { get; set; }

        /// <summary>
        /// 字符串长度
        /// </summary>
        public StringLengthAttribute StringLength { get; set; }

        /// <summary>
        /// 比较
        /// </summary>
        public CompareAttribute Compare { get; set; }
        
        /// <summary>
        /// 正则表达式
        /// </summary>
        public RegularExpressionAttribute Regular { get; set; }
        
        /// <summary>
        /// 两值之间
        /// </summary>
        public RangeAttribute Range { get; set; }
        
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
        /// 上传文件扩展名
        /// </summary>
        public Annotations.FileOptionAttribute FileOption { get; set; }

        /// <summary>
        /// 定制输入
        /// </summary>
        public Annotations.CustomInputAttribute CustomInput { get; set; }

        /// <summary>
        /// 设置 Html data 属性
        /// </summary>
        public Annotations.HtmlDataAttribute HtmlData { get; set; }

        public ModelItemInfo(PropertyInfo property)
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
        public static bool IsType(Type sourceType, Type targetType)
        {
            if (sourceType == targetType) return true;
            if (!targetType.IsClass && sourceType == typeof(Nullable<>).MakeGenericType(targetType)) return true;
            return false;
        }

        /// <summary>
        /// 设置验证
        /// </summary>
        /// <param name="form"></param>
        public void SetValidate(FormValidate.FormValidator form)
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
        }
    }
}
