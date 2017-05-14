using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 路径提供者设置参数
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public class LocationParameter<TParameter> : ILocation
    {
        /// <summary>
        /// 路径
        /// </summary>
        protected ILocation Location { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        protected string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        protected TParameter Value { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="location"></param>
        public LocationParameter(ILocation location)
        {
            Location = location;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public LocationParameter<TParameter> Set(string key, TParameter value)
        {
            Key = key;
            Value = value;
            return this;
        }

        /// <summary>
        /// 行为
        /// </summary>
        public LinkBehave Behave
        {
            get
            {
                return Location.Behave;
            }
            set
            {
                Location.Behave = value;
            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        public string Display
        {
            get
            {
                return Location.Display;
            }
            set
            {
                Location.Display = value;
            }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public FontAwesome Icon
        {
            get
            {
                return Location.Icon;
            }
            set
            {
                Location.Icon = value;
            }
        }

        /// <summary>
        /// 路径所指向的方法
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return Location.Method;
            }
            set
            {
                Location.Method = value;
            }
        }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Key) || Value == null)
                {
                    return Location.Path;
                }
                
                var result = new StringBuilder();
                result.Append(Location.Path);
                if (Location.Path.IndexOf("?") > -1)
                {
                    result.Append("&");
                }
                else
                {
                    result.Append("?");
                }
                if (Value is System.Collections.IEnumerable)
                {
                    var isFirst = true;
                    foreach (var item in Value as System.Collections.IEnumerable)
                    {
                        if (item == null) continue;
                        if (!isFirst)
                        {
                            result.Append("&");
                        }
                        else
                        {
                            isFirst = false;
                        }
                        SetKeyValue(result, item);
                    }
                }
                else
                {
                    SetKeyValue(result, Value);
                }

                return result.ToString();
            }
            set
            {
                Location.Path = value;
            }
        }

        private void SetKeyValue(StringBuilder result, object value)
        {
            result.Append(System.Web.HttpUtility.UrlEncode(Key));
            result.Append("=");
            if (value is string)
            {
                result.Append(System.Web.HttpUtility.UrlEncode(value as string));
            }
            else
            {
                result.Append(value);
            }
        }

        /// <summary>
        /// 路径所指向对象的类型
        /// </summary>
        public Type TargetType
        {
            get
            {
                return Location.TargetType;
            }
            set
            {
                Location.TargetType = value;
            }
        }
    }
}
