using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extends
    {
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
                if (string.IsNullOrEmpty(error.ErrorMessage) && error.Exception != null)
                {
                    outer.AppendLine(error.Exception.Message);
                }
                else
                {
                    outer.AppendLine(error.ErrorMessage);
                }
            }
            return outer.ToString();
        }
        
        /// <summary>
        /// 配置位置内容
        /// </summary>
        /// <param name="source"></param>
        public static void ConfigLocation(this Panel source)
        {
            var option = ControllerHelper.GetLastMethodLocation();
            source.Caption = option.Display;
            source.Icon = option.Icon;
        }

        /// <summary>
        /// 处理上传
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="upload"></param>
        /// <param name="delete"></param>
        /// <param name="expression"></param>
        public static void DealUpload<TModel>(this TModel source, Action<HttpPostedFileBase> upload, Action delete, Expression<Func<TModel, HttpPostedFileBase>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            if (source == null) return;

            if (delete != null)
            {
                if (HttpContext.Current.Request.Form[string.Format("{0}_DeleteMark", expression.GetProperty().Name)] == "1") delete();
            }

            if (upload != null)
            {
                var httpPostedFile = expression.Compile().Invoke(source);
                if (httpPostedFile == null || httpPostedFile.ContentLength == 0) return;
                upload(httpPostedFile);
            }
        }

        /// <summary>
        /// 处理上传
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="upload"></param>
        /// <param name="expression"></param>
        public static void DealUpload<TModel>(this TModel source, Action<HttpPostedFileBase> upload, Expression<Func<TModel, HttpPostedFileBase>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            DealUpload(source, upload, null, expression);
        }

        /// <summary>
        /// 处理上传
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="upload"></param>
        /// <param name="delete"></param>
        /// <param name="expression"></param>
        public static void DealUpload<TModel>(this TModel source, Action<HttpPostedFileBase> upload, Action<int> delete, Expression<Func<TModel, List<HttpPostedFileBase>>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            if (source == null) return;

            if (delete != null)
            {
                var deleteMarks = HttpContext.Current.Request.Form.GetValues(string.Format("{0}_DeleteMark", expression.GetProperty().Name));
                if (deleteMarks == null) deleteMarks = new string[0];
                for (var i = deleteMarks.Length - 1; i > -1; i--)
                {
                    if (deleteMarks[i] == "1") delete(i);
                }
            }

            if (upload != null)
            {
                var httpPostedFiles = expression.Compile().Invoke(source);
                if (httpPostedFiles == null) return;
                foreach (var httpPostedFile in httpPostedFiles)
                {
                    if (httpPostedFile == null || httpPostedFile.ContentLength == 0) continue;
                    upload(httpPostedFile);
                }
            }
        }

        /// <summary>
        /// 处理上传
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="upload"></param>
        /// <param name="expression"></param>
        public static void DealUpload<TModel>(this TModel source, Action<HttpPostedFileBase> upload, Expression<Func<TModel, List<HttpPostedFileBase>>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            DealUpload(source, upload, null, expression);
        }
    }
}