using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    /// <summary>
    /// 表单验证器
    /// </summary>
    public class FormValidator
    {
        private Dictionary<string, ValidatorManager> Fields { get; set; }

        /// <summary>
        /// 创建验证器
        /// </summary>
        public FormValidator()
        {
            Fields = new Dictionary<string, ValidatorManager>();
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ValidatorManager this[string name]
        {
            get
            {
                if (!Fields.ContainsKey(name))
                {
                    Fields.Add(name, new ValidatorManager());
                }
                return Fields[name];
            }
        }

        /// <summary>
        /// 创建 Json
        /// </summary>
        /// <returns></returns>
        internal string Create()
        {
            var result = new JsonObject();
            foreach (var item in Fields)
            {
                var validators = item.Value.CreateJson();
                if (validators == null) continue;
                result.Set(item.Key, validators);
            }
            return result.ToString();
        }
    }
}
