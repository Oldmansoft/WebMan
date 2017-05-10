using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Reflection;

namespace Oldmansoft.Html.Mvc
{
    /// <summary>
    /// 路径扩展
    /// </summary>
    public static class LocationExtends
    {
        /// <summary>
        /// 获取控制器的名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static string GetControllerName(Type source)
        {
            var dataSourceClassName = source.Name;
            return dataSourceClassName.Substring(0, dataSourceClassName.Length - 10);
        }

        /// <summary>
        /// 获取方法信息里的路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="controllerType"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetMethodLocation(this MethodBase source, Type controllerType, UrlHelper url)
        {
            return url.Action(source.Name, GetControllerName(controllerType));
        }


        private static ILocation CreateLocation(this UrlHelper url, MethodInfo method, Type controllerType)
        {
            var location = ControllerHelper.GetMethodLocation(method);
            var behave = location.Behave;
            if (method.ReturnType == typeof(JsonResult) && behave != LinkBehave.Call)
            {
                behave = LinkBehave.Call;
            }
            var result = WebMan.Location.Create(location.Display, method.GetMethodLocation(controllerType, url), location.Icon, behave);
            result.Method = method;
            result.TargetType = controllerType;
            return result;
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Delegate method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }
        
        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<DataTableRequest, JsonResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<int, ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<int[], ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<long, ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<long[], ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<string, ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<string[], ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<Guid, ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<Guid[], ActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location<TController>(this UrlHelper source, Expression<Func<TController, Func<ActionResult>>> method) where TController : Controller
        {
            return CreateLocation(source, method.GetMethod(), typeof(TController));
        }
    }
}
