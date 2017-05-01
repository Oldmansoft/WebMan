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
        /// <param name="source"></param>
        /// <returns></returns>
        internal static string GetControllerName(this MethodBase source)
        {
            var dataSourceClassName = source.ReflectedType.Name;
            return dataSourceClassName.Substring(0, dataSourceClassName.Length - 10);
        }

        /// <summary>
        /// 获取方法信息里的路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        internal static string GetMethodLocation(this MethodBase source, UrlHelper url)
        {
            return url.Action(source.Name, GetControllerName(source));
        }

        /// <summary>
        /// 获取表达式中的方法
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static MethodInfo GetMethod(this LambdaExpression source)
        {
            if (!(source.Body is UnaryExpression)) return null;
            var unaryExpression = (UnaryExpression)source.Body;

            if (!(unaryExpression.Operand is MethodCallExpression)) return null;
            var methodCallExpression = (MethodCallExpression)unaryExpression.Operand;

            if (!(methodCallExpression.Object is ConstantExpression)) return null;
            var constantExpression = (ConstantExpression)methodCallExpression.Object;

            if (!(constantExpression.Value is MethodInfo)) return null;
            return (MethodInfo)constantExpression.Value;
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
        
        /// <summary>
        /// 配置位置内容
        /// </summary>
        /// <param name="source"></param>
        public static void ConfigLoation(this Panel source)
        {
            var option = ControllerHelper.GetLastMethodLocation();
            source.Caption = option.Display;
            source.Icon = option.Icon;
        }
    }
}