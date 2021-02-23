using Oldmansoft.Html.WebMan.FormInputCreator;
using Oldmansoft.Html.WebMan.FormInputCreator.Handlers;
using Oldmansoft.Html.WebMan.Input;
using System;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 全局设定
    /// </summary>
    public static class GlobalOption
    {
        /// <summary>
        /// 表格动作参数名称
        /// </summary>
        public static string TableSelectedParameterName { get; set; }

        static GlobalOption()
        {
            TableSelectedParameterName = "selectedId";
        }

        /// <summary>
        /// 注册表单输入创建处理
        /// </summary>
        /// <param name="handler"></param>
        public static void Register(Handler handler)
        {
            InputCreator.Register(handler);
        }

        /// <summary>
        /// 注册表单输入创建列表项处理
        /// </summary>
        /// <param name="type"></param>
        /// <param name="func"></param>
        public static void Register(Type type, Func<HandlerParameter, ModelPropertyContent, IFormInput> func)
        {
            ListHandler.Register(type, func);
        }
    }
}
