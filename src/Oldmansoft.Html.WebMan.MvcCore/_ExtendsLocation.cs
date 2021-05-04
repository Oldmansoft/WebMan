using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Oldmansoft.Html.WebMan
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
            return dataSourceClassName[0..^10];
        }

        /// <summary>
        /// 获取方法信息里的路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="controllerType"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetMethodLocation(this MethodBase source, Type controllerType, IUrlHelper url)
        {
            var area = controllerType.GetCustomAttribute<AreaAttribute>();
            if (area == null) area = source.GetCustomAttribute<AreaAttribute>();
            object values;
            if (area != null)
            {
                values = new { area = area.RouteValue };
            }
            else
            {
                values = new { area = string.Empty };
            }
            return url.Action(source.Name, GetControllerName(controllerType), values);
        }


        private static ILocation CreateLocation(this IUrlHelper url, MethodInfo method, Type controllerType)
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
        public static LocationBind<object> Location(this IUrlHelper source, Delegate method)
        {
            return new LocationBind<object>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this IUrlHelper source, Func<IActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location<TModel>(this IUrlHelper source, Func<TModel, IActionResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location(this IUrlHelper source, Func<DataTable.Request, JsonResult> method)
        {
            return CreateLocation(source, method.Method, method.Target.GetType());
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<int> Location(this IUrlHelper source, Func<int, DataTable.Request, JsonResult> method)
        {
            return new LocationBind<int>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<int?> LocationNullable(this IUrlHelper source, Func<int?, DataTable.Request, JsonResult> method)
        {
            return new LocationBind<int?>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<long> Location(this IUrlHelper source, Func<long, DataTable.Request, JsonResult> method)
        {
            return new LocationBind<long>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<long?> LocationNullable(this IUrlHelper source, Func<long?, DataTable.Request, JsonResult> method)
        {
            return new LocationBind<long?>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<string> Location(this IUrlHelper source, Func<string, DataTable.Request, JsonResult> method)
        {
            return new LocationBind<string>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<Guid> Location(this IUrlHelper source, Func<Guid, DataTable.Request, JsonResult> method)
        {
            return new LocationBind<Guid>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<Guid?> LocationNullable(this IUrlHelper source, Func<Guid?, DataTable.Request, JsonResult> method)
        {
            return new LocationBind<Guid?>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<int> Location(this IUrlHelper source, Func<int, IActionResult> method)
        {
            return new LocationBind<int>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<int?> LocationNullable(this IUrlHelper source, Func<int?, IActionResult> method)
        {
            return new LocationBind<int?>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<int[]> Location(this IUrlHelper source, Func<int[], IActionResult> method)
        {
            return new LocationBind<int[]>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<long> Location(this IUrlHelper source, Func<long, IActionResult> method)
        {
            return new LocationBind<long>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<long?> LocationNullable(this IUrlHelper source, Func<long?, IActionResult> method)
        {
            return new LocationBind<long?>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<long[]> Location(this IUrlHelper source, Func<long[], IActionResult> method)
        {
            return new LocationBind<long[]>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<string> Location(this IUrlHelper source, Func<string, IActionResult> method)
        {
            return new LocationBind<string>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<string[]> Location(this IUrlHelper source, Func<string[], IActionResult> method)
        {
            return new LocationBind<string[]>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<Guid> Location(this IUrlHelper source, Func<Guid, IActionResult> method)
        {
            return new LocationBind<Guid>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<Guid?> LocationNullable(this IUrlHelper source, Func<Guid?, IActionResult> method)
        {
            return new LocationBind<Guid?>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<Guid[]> Location(this IUrlHelper source, Func<Guid[], IActionResult> method)
        {
            return new LocationBind<Guid[]>(CreateLocation(source, method.Method, method.Target.GetType()));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ILocation Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<IActionResult>>> method) where TController : Controller
        {
            return CreateLocation(source, method.GetMethod(), typeof(TController));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<int> Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<int, IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<int>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<int?> LocationNullable<TController>(this IUrlHelper source, Expression<Func<TController, Func<int?, IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<int?>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<int[]> Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<int[], IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<int[]>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<long> Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<long, IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<long>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<long?> LocationNullable<TController>(this IUrlHelper source, Expression<Func<TController, Func<long?, IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<long?>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<long[]> Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<long[], IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<long[]>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<string> Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<string, IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<string>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<string[]> Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<string[], IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<string[]>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<Guid> Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<Guid, IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<Guid>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<Guid?> LocationNullable<TController>(this IUrlHelper source, Expression<Func<TController, Func<Guid?, IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<Guid?>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static LocationBind<Guid[]> Location<TController>(this IUrlHelper source, Expression<Func<TController, Func<Guid[], IActionResult>>> method) where TController : Controller
        {
            return new LocationBind<Guid[]>(CreateLocation(source, method.GetMethod(), typeof(TController)));
        }
    }
}
