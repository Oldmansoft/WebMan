using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 路径提供者绑定参数
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public class LocationBind<TParameter> : ILocation
    {
        /// <summary>
        /// 路径
        /// </summary>
        protected ILocation Location { get; private set; }
        
        /// <summary>
        /// 键值对
        /// </summary>
        protected Dictionary<string, TParameter> KeyValues { get; private set; }
        
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="location"></param>
        public LocationBind(ILocation location)
        {
            Location = location;
            KeyValues = new Dictionary<string, TParameter>();
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public LocationBind<TParameter> Set(string key, TParameter value)
        {
            KeyValues[key] = value;
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
                var isFirst = true;
                var result = new StringBuilder();
                result.Append(Location.Path);
                foreach (var item in KeyValues)
                {
                    if (string.IsNullOrWhiteSpace(item.Key)) continue;
                    if (item.Value == null) continue;
                    
                    if (item.Value is System.Collections.IEnumerable && item.Value.GetType() != typeof(string))
                    {
                        foreach (var value in item.Value as System.Collections.IEnumerable)
                        {
                            if (value == null) continue;

                            SetLocationPrefix(isFirst, result);
                            SetKeyValue(result, item.Key, value);
                            isFirst = false;
                        }
                    }
                    else
                    {
                        SetLocationPrefix(isFirst, result);
                        SetKeyValue(result, item.Key, item.Value);
                        isFirst = false;
                    }
                }
                return result.ToString();
            }
            set
            {
                Location.Path = value;
            }
        }

        private void SetLocationPrefix(bool isFirst, StringBuilder result)
        {
            if (isFirst)
            {
                if (Location.Path.IndexOf("?") > -1)
                {
                    result.Append("&");
                }
                else
                {
                    result.Append("?");
                }
            }
            else
            {
                result.Append("&");
            }
        }

        private void SetKeyValue(StringBuilder result, string key, object value)
        {
            result.Append(System.Web.HttpUtility.UrlEncode(key));
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
