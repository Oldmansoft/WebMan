using Oldmansoft.Html.WebMan.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 定制输入
    /// </summary>
    public class CustomInputAttribute : Attribute
    {
        /// <summary>
        /// 输入组件
        /// </summary>
        public Type InputType { get; set; }

        /// <summary>
        /// 输入组件的参数
        /// </summary>
        public object[] Parameter { get; set; }

        /// <summary>
        /// 创建定制输入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="paramters"></param>
        public CustomInputAttribute(Type type, params object[] paramters)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (!type.IsClass) throw new ArgumentException("必须是类", "type");
            if (type.IsAbstract) throw new ArgumentException("不能是虚拟类", "type");

            if (!type.GetInterfaces().Contains(typeof(ICustomInput)))
            {
                throw new ArgumentException(string.Format("{0} 不支持 ICustomInput 接口。", type.FullName), "type");
            }
            
            InputType = type;
        }
    }
}
