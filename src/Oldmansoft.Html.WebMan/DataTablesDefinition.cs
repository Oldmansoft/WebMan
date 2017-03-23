using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格定义
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class DataTablesDefinition<TModel> : HtmlElement where TModel : class
    {
        /// <summary>
        /// 列名称
        /// </summary>
        private IList<DataTablesColumn> Columns { get; set; }

        /// <summary>
        /// 请求数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 分页长度
        /// </summary>
        public uint? PageLength { get; set; }

        /// <summary>
        /// 创建表格定义
        /// </summary>
        public DataTablesDefinition()
            : base(HtmlTag.Table)
        {
            Columns = new List<DataTablesColumn>();
            InitColumns();
        }

        private void InitColumns()
        {
            foreach (var property in typeof(TModel).GetProperties())
            {
                var column = new DataTablesColumn() { Name = property.Name, Text = property.Name, Visible = true };
                foreach (var attribute in property.GetCustomAttributes(typeof(Attribute), true))
                {
                    if (attribute is DisplayAttribute)
                    {
                        column.Text = ((DisplayAttribute)attribute).Name.JavaScriptEncode();
                    }
                }
                if (property.Name.ToLower() == "id")
                {
                    column.Visible = false;
                }
                Columns.Add(column);
            }
        }

        private HtmlElement CreateDefinitionColumns()
        {
            var result = new HtmlElement(HtmlTag.Tr);
            foreach (var item in Columns)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text(item.Text));
            }
            return result;
        }

        private string GetColumnContent()
        {
            var result = new StringBuilder();
            result.Append("[");
            for (var i = 0; i < Columns.Count; i++)
            {
                if (i > 0) result.Append(",");
                result.Append("{\"data\":\"");
                result.Append(Columns[i].Name.Replace("\"", "\\\"").Replace("\r", "").Replace("\n", " "));
                result.Append("\"");
                if (!Columns[i].Visible)
                {
                    result.Append(",\"visible\":false");
                }
                result.Append("}");
            }
            result.Append("]");
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

            outer.AddEvent(string.Format("window.oldmansoft.webman.setDataTable(view, '{0}', '{1}', {2});", name, DataSource, GetColumnContent()));
            base.Format(outer);
        }
    }
}
