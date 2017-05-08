using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Oldmansoft.Html.Mvc
{
    /// <summary>
    /// 路径扩展
    /// </summary>
    public static class LocationExtends
    {
        private static ILocation CreateLocation(this UrlHelper url, System.Reflection.MethodInfo method)
        {
            var location = ControllerHelper.GetMethodLocation(method);
            var behave = location.Behave;
            if (method.ReturnType == typeof(JsonResult) && behave != LinkBehave.Call)
            {
                behave = LinkBehave.Call;
            }
            var result = WebMan.Location.Create(location.Display, method.GetMethodLocation(url), location.Icon, behave);
            result.Method = method;
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
            return CreateLocation(source, method.Method);
        }
        
        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<DataTableRequest, JsonResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<int, ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<int[], ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<long, ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<long[], ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<string, ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<string[], ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<Guid, ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<Guid[], ActionResult> method)
        {
            return CreateLocation(source, method.Method);
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location<TController>(this UrlHelper source, Expression<Func<TController, Func<ActionResult>>> method) where TController : Controller
        {
            return CreateLocation(source, method.GetMethod());
        }
    }
}
