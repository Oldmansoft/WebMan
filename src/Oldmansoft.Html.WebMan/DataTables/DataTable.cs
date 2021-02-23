using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格辅助
    /// </summary>
    public static class DataTable
    {
        /// <summary>
        /// 注册值显示
        /// </summary>
        /// <param name="valueDisplay"></param>
        public static void Register(IValueDisplay valueDisplay)
        {
            ValueDisplay.Instance.Add(valueDisplay);
        }

        /// <summary>
        /// 创建表格数据源
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public static DataTableSource<TModel> Source<TModel>(IEnumerable<TModel> source, Request request)
            where TModel : class
        {
            return new DataTableSource<TModel>(source, request, 0);
        }

        /// <summary>
        /// 创建表格数据源
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="request">请求参数</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns></returns>
        public static DataTableSource<TModel> Source<TModel>(IEnumerable<TModel> source, Request request, int totalCount)
            where TModel : class
        {
            return new DataTableSource<TModel>(source, request, totalCount);
        }

        /// <summary>
        /// 定义表格
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="primaryKey"></param>
        /// <returns>表格创建者</returns>
        public static DataTableDefining<TModel> Define<TModel>(Expression<Func<TModel, object>> primaryKey)
            where TModel : class
        {
            return new DataTableDefining<TModel>(primaryKey);
        }

        /// <summary>
        /// 请求参数
        /// </summary>
        public class Request
        {
            /// <summary>
            /// 绘制计数器
            /// </summary>
            public int draw { get; set; }

            /// <summary>
            /// 第一条数据的起始位置，比如0代表第一条数据
            /// </summary>
            public int start { get; set; }

            /// <summary>
            /// 告诉服务器每页显示的条数
            /// </summary>
            public int length { get; set; }

            /// <summary>
            /// 获取页码
            /// </summary>
            public int PageIndex
            {
                get
                {
                    if (length == 0) return 1;
                    return start / length + 1;
                }
            }

            /// <summary>
            /// 获取页大小
            /// </summary>
            public int PageSize
            {
                get { return length; }
            }
        }
    }
}
