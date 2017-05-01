using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Mvc
{
    class ControllerHelper
    {
        /// <summary>
        /// 获取最后一个方法的信息
        /// </summary>
        /// <returns></returns>
        public static MethodBase GetLastMethod()
        {
            var frames = new System.Diagnostics.StackTrace(1, false).GetFrames();
            var result = frames[0].GetMethod();
            Type controllerType = null;
            for (var i = 0; i < frames.Length; i++)
            {
                var method = frames[i].GetMethod();
                if (method.DeclaringType == null) break;
                if (!method.DeclaringType.IsSubclassOf(typeof(System.Web.Mvc.Controller))) continue;

                controllerType = method.DeclaringType;
                result = method;
            }
            return result;
        }

        /// <summary>
        /// 获取最后一个位置配置
        /// </summary>
        /// <returns></returns>
        public static LocationAttribute GetLastMethodLocation()
        {
            var method = GetLastMethod();
            return GetMethodLocation(method);
        }

        /// <summary>
        /// 获取位置配置
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationAttribute GetMethodLocation(MethodBase method)
        {
            var result = method.GetCustomAttribute<LocationAttribute>();
            if (result == null)
            {
                var title = method.Name;
                var indexOfController = title.IndexOf("Controller");
                if (indexOfController > -1 && indexOfController == title.Length - 10)
                {
                    title = title.Substring(0, title.Length - 10);
                }
                result = new LocationAttribute(title);
            }
            return result;
        }
    }
}
