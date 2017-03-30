using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格辅助
    /// </summary>
    public static class DataTable
    {
        /// <summary>
        /// 创建表格定义
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="primaryKey">主键</param>
        /// <param name="dataSource">数据源路径</param>
        /// <returns></returns>
        public static DataTableDefinition<TModel> Definition<TModel>(Expression<Func<TModel, object>> primaryKey, string dataSource)
            where TModel : class
        {
            return new DataTableDefinition<TModel>(primaryKey, dataSource);
        }

        /// <summary>
        /// 创建表格定义
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="primaryKey">主键</param>
        /// <param name="dataSource">数据源路径</param>
        /// <returns></returns>
        public static DataTableDefinition<TModel> Definition<TModel>(Expression<Func<TModel, object>> primaryKey, ILocation dataSource)
            where TModel : class
        {
            if (dataSource == null) throw new ArgumentNullException("dataSource");
            return new DataTableDefinition<TModel>(primaryKey, dataSource.Location);
        }

        /// <summary>
        /// 创建表格数据源
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public static DataTableSource<TModel> Source<TModel>(IEnumerable<TModel> source, DataTableRequest request)
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
        public static DataTableSource<TModel> Source<TModel>(IEnumerable<TModel> source, DataTableRequest request, int totalCount)
            where TModel : class
        {
            return new DataTableSource<TModel>(source, request, totalCount);
        }
    }
}
