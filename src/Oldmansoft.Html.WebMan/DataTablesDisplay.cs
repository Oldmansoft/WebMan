using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格显示
    /// </summary>
    public class DataTablesDisplay : HtmlElement
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public List<string> Columns { get; private set; }

        /// <summary>
        /// 请求数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 分页长度
        /// </summary>
        public uint? PageLength { get; set; }
        
        /// <summary>
        /// 创建表格显示
        /// </summary>
        public DataTablesDisplay()
            : base(HtmlTag.Table)
        {
            Columns = new List<string>();
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            var name = outer.Generator.GetGeneratorName();
            AddClass(name).AddClass("dataTable");

            var header = new HtmlElement(HtmlTag.THead);
            Append(header);
            Append(new HtmlElement(HtmlTag.TBody));
            var footer = new HtmlElement(HtmlTag.TFoot);
            Append(footer);

            header.Append(CreateColumns());
            footer.Append(CreateColumns());

            outer.AddEvent(string.Format("view.node.find('.{0}').DataTable();", name));
            base.Format(outer);
        }

        private HtmlElement CreateColumns()
        {
            var result = new HtmlElement(HtmlTag.Tr);
            foreach (var item in Columns)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text(item));
            }

            return result;
        }
    }
}
