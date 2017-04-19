using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Oldmansoft.Html.Mvc
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extends
    {
        /// <summary>
        /// 获取控制器的名称
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetControllerName(this MethodBase method)
        {
            var dataSourceClassName = method.ReflectedType.Name;
            return dataSourceClassName.Substring(0, dataSourceClassName.Length - 10);
        }

        /// <summary>
        /// 获取方法信息里的路径
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetMethodLocation(this MethodBase method)
        {
            return string.Format("/{0}/{1}", GetControllerName(method), method.Name);
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static ILocation Location(this Controller source, Func<DataTableRequest, JsonResult> dataSource)
        {
            return new DataTableLocation(dataSource);
        }

        /// <summary>
        /// 验证失败
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool ValidateFail(this ModelStateDictionary source)
        {
            return !source.IsValid;
        }

        /// <summary>
        /// 验证消息
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ValidateMessage(this ModelStateDictionary source)
        {
            var outer = new StringBuilder();
            foreach (var error in source.Values.SelectMany(v => v.Errors))
            {
                outer.AppendLine(error.ErrorMessage);
            }
            return outer.ToString();
        }
    }
}