using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格定义
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class DataTableDefinition<TModel> : HtmlElement where TModel : class
    {
        /// <summary>
        /// 主键名称
        /// </summary>
        private string PrimaryKeyName { get; set; }

        /// <summary>
        /// 是否显示序数列
        /// </summary>
        private bool IsDisplayIndexColumn { get; set; }

        /// <summary>
        /// 是否显示多选框列
        /// </summary>
        private bool IsDisplayCheckboxColumn { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        private IList<DataTableColumn> Columns { get; set; }

        /// <summary>
        /// 请求数据源路径
        /// </summary>
        private string DataSourceLoation { get; set; }

        /// <summary>
        /// 分页长度
        /// </summary>
        public uint? PageLength { get; set; }

        /// <summary>
        /// 创建表格定义
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <param name="dataSource">数据源路径</param>
        internal DataTableDefinition(Expression<Func<TModel, object>> primaryKey, string dataSource)
            : base(HtmlTag.Table)
        {
            if (primaryKey == null) throw new ArgumentNullException("primaryKey");
            var primaryKeyProperty = primaryKey.GetProperty();
            if (primaryKeyProperty == null) throw new ArgumentException("指定的属性不存在，请确认不是字段或方法。", "primaryKey");
            if (dataSource == null) throw new ArgumentNullException("dataSource");
            PrimaryKeyName = primaryKeyProperty.Name.ToLower();
            DataSourceLoation = dataSource;
            InitColumns();
        }
        
        private void InitColumns()
        {
            Columns = new List<DataTableColumn>();
            foreach (var property in typeof(TModel).GetProperties())
            {
                var column = new DataTableColumn() { Name = property.Name, Text = property.Name, Visible = true };
                foreach (var attribute in property.GetCustomAttributes(typeof(Attribute), true))
                {
                    if (attribute is DisplayAttribute)
                    {
                        column.Text = ((DisplayAttribute)attribute).Name.JavaScriptEncode();
                    }
                }
                if (property.Name.ToLower() == PrimaryKeyName)
                {
                    column.Visible = false;
                }
                Columns.Add(column);
            }
        }

        private HtmlElement CreateDefinitionColumns()
        {
            var result = new HtmlElement(HtmlTag.Tr);
            result.Append(new HtmlElement(HtmlTag.Th).Append(new HtmlRaw("<input class='webman-datatables-checkbox' type='checkbox'>")));
            if (IsDisplayIndexColumn)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text("序数"));
            }
            foreach (var item in Columns)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text(item.Text));
            }
            return result;
        }

        private string GetColumnContent()
        {
            var result = new JsonArray();

            var checkboxObject = new JsonObject();
            checkboxObject.Set("data", PrimaryKeyName);
            checkboxObject.Set("render", new JsonRaw("window.oldmansoft.webman.setDataTableColumnCheckbox"));
            result.Add(checkboxObject);

            if (IsDisplayIndexColumn)
            {
                var indexObject = new JsonObject();
                indexObject.Set("data", null);
                indexObject.Set("render", new JsonRaw("window.oldmansoft.webman.setDataTableColumnIndex"));
                result.Add(indexObject);
            }

            for (var i = 0; i < Columns.Count; i++)
            {
                var item = new JsonObject();
                item.Set("data", Columns[i].Name);
                if (!Columns[i].Visible)
                {
                    item.Set("visible", false);
                }
                result.Add(item);
            }
            
            return result.ToString();
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            var name = outer.Generator.GetGeneratorName();
            AddClass(name);
            AddClass("dataTable");

            var header = new HtmlElement(HtmlTag.THead);
            Append(header);
            Append(new HtmlElement(HtmlTag.TBody));
            var footer = new HtmlElement(HtmlTag.TFoot);
            Append(footer);

            header.Append(CreateDefinitionColumns());
            footer.Append(CreateDefinitionColumns());

            outer.AddEvent(string.Format("window.oldmansoft.webman.setDataTable(view, '{0}', '{1}', {2});", name, DataSourceLoation, GetColumnContent()));
            base.Format(outer);
        }

        /// <summary>
        /// 是否显示序数
        /// </summary>
        /// <param name="value"></param>
        public void DisplayIndexColumn(bool value)
        {
            IsDisplayIndexColumn = value;
        }
    }
}
