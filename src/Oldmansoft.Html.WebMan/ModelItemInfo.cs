using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
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
        public bool Required { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// 最小长度
        /// </summary>
        public int MinimumLength { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaximumLength { get; set; }

        /// <summary>
        /// 比较
        /// </summary>
        public string Compare { get; set; }

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        public string RegularPattern { get; set; }

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
        /// 文件数量
        /// </summary>
        public uint FileCount { get; set; }

        /// <summary>
        /// 文件可以删除
        /// </summary>
        public bool FileCanDelete { get; set; }

        /// <summary>
        /// 两值之间
        /// </summary>
        public RangeAttribute Range { get; set; }

        public ModelItemInfo(PropertyInfo property)
        {
            Property = property;
            Name = property.Name;
            Display = property.Name;
            Hidden = property.Name.ToLower() == "id";
        }
    }
}
