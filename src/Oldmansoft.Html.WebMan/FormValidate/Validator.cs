﻿using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result = new FormValidate.StringLength();
            result.Min = min;
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
            var result = new FormValidate.StringLength();
            result.Min = min;
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
            var result = new FormValidate.Regexp();
            result.Pattern = pattern;
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
    }
}
