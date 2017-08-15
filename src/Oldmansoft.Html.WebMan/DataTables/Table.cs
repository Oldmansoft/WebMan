using Oldmansoft.Html.Util;
using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.DataTables
{
    /// <summary>
    /// 表格
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class Table<TModel> : HtmlElement
        where TModel : class
    {
        /// <summary>
        /// 主键方法
        /// </summary>
        protected Func<TModel, object> PrimaryKeyFunc { get; private set; }

        /// <summary>
        /// 主键名称
        /// </summary>
        protected string PrimaryKeyName { get; private set; }

        /// <summary>
        /// 是否显示序数列
        /// </summary>
        protected bool IsDisplayIndexColumn { get; private set; }

        /// <summary>
        /// 是否显示多选框列
        /// </summary>
        protected bool IsDisplayCheckboxColumn { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        internal IList<DataTableColumn> Columns { get; private set; }

        /// <summary>
        /// 前置节点
        /// </summary>
        protected List<IHtmlNode> FrontNodes { get; private set; }

        /// <summary>
        /// 后置节点
        /// </summary>
        protected List<IHtmlNode> AfterNodes { get; private set; }

        /// <summary>
        /// 创建表格
        /// </summary>
        public Table(Expression<Func<TModel, object>> primaryKey)
            : base(HtmlTag.Table)
        {
            if (primaryKey == null) throw new ArgumentNullException("primaryKey");
            var primaryKeyProperty = primaryKey.GetProperty();
            if (primaryKeyProperty == null) throw new ArgumentException("指定的属性不存在，请确认不是字段或方法。", "primaryKey");

            PrimaryKeyName = primaryKeyProperty.Name;
            PrimaryKeyFunc = primaryKey.Compile();
            FrontNodes = new List<IHtmlNode>();
            AfterNodes = new List<IHtmlNode>();
            InitColumns();
        }
        
        private void InitColumns()
        {
            Columns = new List<DataTableColumn>();
            foreach (var item in ModelProvider.Instance.GetItems(typeof(TModel)))
            {
                var column = new DataTableColumn() { Name = item.Property.Name, Property = item.Property, Text = item.Display, Visible = true };

                if (item.Property.Name == PrimaryKeyName)
                {
                    column.Visible = false;
                }
                Columns.Add(column);
            }
        }
        
        /// <summary>
        /// 是否显示序数列
        /// </summary>
        /// <param name="value"></param>
        public void DisplayIndexColumn(bool value)
        {
            IsDisplayIndexColumn = value;
        }

        /// <summary>
        /// 是否显示多选项框列
        /// </summary>
        public void SupportParameter()
        {
            IsDisplayCheckboxColumn = true;
        }
    }
}
