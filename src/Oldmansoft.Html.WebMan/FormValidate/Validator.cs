using Oldmansoft.Html.Util;
using System;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 验证器
    /// </summary>
    public abstract class Validator
    {
        /// <summary>
        /// 提示消息
        /// </summary>
        private string MessageContent { get; set; }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="json"></param>
        protected abstract void Set(JsonObject json);

        /// <summary>
        /// 创建值
        /// </summary>
        /// <returns></returns>
        internal JsonObject CreateValue()
        {
            var result = new JsonObject();
            if(!string.IsNullOrWhiteSpace(MessageContent))
            {
                result.Set("message", MessageContent.Trim());
            }
            Set(result);
            return result;
        }

        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Validator Message(string content)
        {
            MessageContent = content;
            return this;
        }

        internal Validator SetMessage(System.ComponentModel.DataAnnotations.ValidationAttribute attribute)
        {
            if (!string.IsNullOrEmpty(attribute.ErrorMessage))
            {
                MessageContent = attribute.ErrorMessage;
            }
            return this;
        }

        /// <summary>
        /// 创建非空
        /// </summary>
        /// <returns></returns>
        public static Validator NoEmpty()
        {
            return new FormValidate.NotEmpty();
        }

        /// <summary>
        /// 创建字符串长度设置
        /// </summary>
        /// <param name="min">至少</param>
        /// <returns></returns>
        public static Validator StringLength(int min)
        {
            if (min < 1) throw new ArgumentOutOfRangeException("min");
            var result = new FormValidate.StringLength
            {
                Min = min
            };
            return result;
        }
        
        /// <summary>
        /// 创建字符串长度设置
        /// </summary>
        /// <param name="min">至少</param>
        /// <param name="max">至多</param>
        /// <returns></returns>
        public static Validator StringLength(int min, int max)
        {
            if (max < 1) throw new ArgumentOutOfRangeException("max");
            var result = new FormValidate.StringLength();
            if (min > 0)
            {
                result.Min = min;
            }
            result.Max = max;
            return result;
        }

        /// <summary>
        /// 创建正则表达式
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Validator Regexp(string pattern)
        {
            var result = new FormValidate.Regexp
            {
                Pattern = pattern
            };
            return result;
        }

        /// <summary>
        /// 创建邮件地址
        /// </summary>
        /// <returns></returns>
        public static Validator EmailAddress()
        {
            return new FormValidate.EmailAddress();
        }

        /// <summary>
        /// 相同
        /// </summary>
        /// <param name="otherProperty">属性名称</param>
        /// <returns></returns>
        public static Validator Identical(string otherProperty)
        {
            var result = new FormValidate.Identical
            {
                OtherProperty = otherProperty
            };
            return result;
        }

        /// <summary>
        /// 少于
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Validator LessThan(object value)
        {
            var result = new FormValidate.LessThan
            {
                Value = value
            };
            return result;
        }

        /// <summary>
        /// 多于
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Validator GreaterThan(object value)
        {
            var result = new FormValidate.GreaterThan
            {
                Value = value
            };
            return result;
        }

        /// <summary>
        /// 列表固定数量
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Validator FixedCount(uint count)
        {
            var result = new FormValidate.ListCount
            {
                Fixed = count
            };
            return result;
        }

        /// <summary>
        /// 列表范围数量
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="inclusive"></param>
        /// <returns></returns>
        public static Validator RangeCount(uint min, uint max, bool inclusive)
        {
            var result = new FormValidate.ListCount
            {
                Inclusive = inclusive
            };
            if (min > 0) result.Min = min;
            if (max > 0) result.Max = max;
            return result;
        }

        /// <summary>
        /// 文件内容长度限制
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Validator FileLimitContentLength(uint length)
        {
            var result = new FormValidate.FileLimitContentLength
            {
                Length = length
            };
            return result;
        }
    }
}
