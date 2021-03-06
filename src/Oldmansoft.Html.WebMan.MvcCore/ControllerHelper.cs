﻿using System.Reflection;

namespace Oldmansoft.Html.WebMan
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
            for (var i = 0; i < frames.Length; i++)
            {
                var method = frames[i].GetMethod();
                if (method.DeclaringType == null) break;
                if (!method.DeclaringType.IsSubclassOf(typeof(Microsoft.AspNetCore.Mvc.Controller))) continue;

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
                    title = title[0..^10];
                }
                result = new LocationAttribute(title);
            }
            return result;
        }
    }
}
