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
        private string SelectedParameterName { get; set; }

        /// <summary>
        /// 主键方法
        /// </summary>
        protected Func<TModel, object> PrimaryKeyFunc { get; private set; }

        /// <summary>
        /// 主键属性
        /// </summary>
        protected System.Reflection.PropertyInfo PrimaryKeyProperty { get; private set; }

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
        internal Dictionary<string, DataTableColumn> Columns { get; private set; }

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
            PrimaryKeyProperty = primaryKey.GetProperty();
            if (PrimaryKeyProperty == null) throw new ArgumentException("指定的属性不存在，请确认不是字段或方法。", "primaryKey");

            PrimaryKeyName = PrimaryKeyProperty.Name;
            PrimaryKeyFunc = primaryKey.Compile();
            FrontNodes = new List<IHtmlNode>();
            AfterNodes = new List<IHtmlNode>();
            InitColumns();
        }
        
        private void InitColumns()
        {
            Columns = new Dictionary<string, DataTableColumn>();
            SetItems(typeof(TModel), new List<string>());
        }

        private void SetItems(Type type, List<string> parents)
        {
            foreach (var item in ModelProvider.Instance.GetItems(type))
            {
                var parentsAndCurrent = new List<string>();
                parentsAndCurrent.AddRange(parents);
                parentsAndCurrent.Add(item.Name);
                if (item.Expansion)
                {
                    SetItems(item.Property.PropertyType, parentsAndCurrent);
                    continue;
                }
                var itemName = string.Join("-", parentsAndCurrent);
                var column = new DataTableColumn(itemName, item.Property, item.Display, item.Property != PrimaryKeyProperty && !item.Hidden);
                Columns.Add(itemName, column);
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

        /// <summary>
        /// 设置表格的动作参数名称
        /// </summary>
        /// <param name="name"></param>
        public void SetSelectedParameterName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            SelectedParameterName = name.Trim();
        }

        /// <summary>
        /// 获取表格的动作参数名称
        /// </summary>
        /// <returns></returns>
        public string GetSelectedParameterName()
        {
            if (!string.IsNullOrWhiteSpace(SelectedParameterName)) return SelectedParameterName;
            if (!string.IsNullOrWhiteSpace(GlobalOption.TableSelectedParameterName)) return GlobalOption.TableSelectedParameterName;
            return "selectedId";
        }
    }
}
