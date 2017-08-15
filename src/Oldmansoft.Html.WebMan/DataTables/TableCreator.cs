using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格创建者
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TableCreator<TModel>
        where TModel : class
    {
        private Expression<Func<TModel, object>> PrimaryKey { get; set; }

        /// <summary>
        /// 创建表格创建者
        /// </summary>
        /// <param name="primaryKey"></param>
        public TableCreator(Expression<Func<TModel, object>> primaryKey)
        {
            PrimaryKey = primaryKey;
        }

        /// <summary>
        /// 创建静态表格
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public StaticTable<TModel> Create(IEnumerable<TModel> source)
        {
            return new StaticTable<TModel>(PrimaryKey, source);
        }

        /// <summary>
        /// 创建数据源表格
        /// </summary>
        /// <param name="dataSource">数据源地址</param>
        /// <returns></returns>
        public DataTableDefinition<TModel> Create(ILocation dataSource)
        {
            if (dataSource == null) throw new ArgumentNullException("dataSource");
            return new DataTableDefinition<TModel>(PrimaryKey, dataSource.Path);
        }
    }
}
