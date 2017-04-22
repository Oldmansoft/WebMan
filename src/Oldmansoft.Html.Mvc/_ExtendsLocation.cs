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
        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Delegate method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }
        
        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<DataTableRequest, JsonResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<int, ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<int[], ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<long, ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<long[], ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<string, ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<string[], ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<Guid, ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this UrlHelper source, Func<Guid[], ActionResult> method)
        {
            return WebMan.Location.Create(method.Method.GetMethodLocation(source));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location<TController>(this UrlHelper source, Expression<Func<TController, Func<ActionResult>>> method) where TController : Controller
        {
            return WebMan.Location.Create(method.GetMethod().GetMethodLocation(source));
        }
    }
}
