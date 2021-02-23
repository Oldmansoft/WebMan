using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    /// <summary>
    /// 验证器管理
    /// </summary>
    public class ValidatorManager
    {
        internal Dictionary<Type, Validator> Store { get; private set; }

        internal ValidatorManager()
        {
            Store = new Dictionary<Type, Validator>();
        }

        private string FirstCharToLower(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            return text.Substring(0, 1).ToLower() + text.Substring(1);
        }

        internal JsonObject CreateJson()
        {
            if (Store.Count == 0) return null;

            var result = new JsonObject();
            var value = new JsonObject();
            foreach (var type in Store.Keys)
            {
                value.Set(FirstCharToLower(type.Name), Store[type].CreateValue());
            }
            result.Set("validators", value);

            return result;
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        public ValidatorManager Set(Validator validator)
        {
            if (validator == null) throw new ArgumentNullException("validator");
            var type = validator.GetType();
            Store[type] = validator;
            return this;
        }
    }
}
