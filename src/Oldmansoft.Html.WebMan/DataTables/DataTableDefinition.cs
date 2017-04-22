using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;
using System.Reflection;
using Oldmansoft.Html.WebMan.Util;

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
        /// 前置节点
        /// </summary>
        private List<IHtmlNode> FrontNodes { get; set; }

        /// <summary>
        /// 后置节点
        /// </summary>
        private List<IHtmlNode> AfterNodes { get; set; }

        /// <summary>
        /// 表格操作
        /// </summary>
        private List<DataTableAction> TableActions { get; set; }

        /// <summary>
        /// 数据项操作
        /// </summary>
        private List<DataTableAction> ItemActions { get; set; }

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

            FrontNodes = new List<IHtmlNode>();
            AfterNodes = new List<IHtmlNode>();
            TableActions = new List<DataTableAction>();
            ItemActions = new List<DataTableAction>();
            PrimaryKeyName = primaryKeyProperty.Name;
            DataSourceLoation = dataSource;
            InitColumns();
        }
        
        private void InitColumns()
        {
            Columns = new List<DataTableColumn>();
            foreach (var item in ModelProvider.Instance.GetItems( typeof(TModel)))
            {
                var column = new DataTableColumn() { Name = item.Property.Name, Text = item.Display, Visible = true };
                
                if (item.Property.Name == PrimaryKeyName)
                {
                    column.Visible = false;
                }
                Columns.Add(column);
            }
        }

        private HtmlElement CreateDefinitionColumns()
        {
            var result = new HtmlElement(HtmlTag.Tr);
            if (IsDisplayCheckboxColumn)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Append(new HtmlRaw("<input class='webman-datatables-checkbox' type='checkbox'>")));
            }
            if (IsDisplayIndexColumn)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text("序数"));
            }
            foreach (var item in Columns)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text(item.Text));
            }
            if (ItemActions.Count > 0)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text("操作"));
            }
            return result;
        }

        private string GetColumnContent()
        {
            var result = new JsonArray();

            if (IsDisplayCheckboxColumn)
            {
                var checkboxObject = new JsonObject();
                checkboxObject.Set("data", PrimaryKeyName);
                checkboxObject.Set("render", new JsonRaw("oldmansoft.webman.setDataTableColumnCheckbox"));
                result.Add(checkboxObject);
            }

            if (IsDisplayIndexColumn)
            {
                var indexObject = new JsonObject();
                indexObject.Set("data", null);
                indexObject.Set("render", new JsonRaw("oldmansoft.webman.setDataTableColumnIndex"));
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

            if (ItemActions.Count > 0)
            {
                var operateObject = new JsonObject();
                operateObject.Set("data", PrimaryKeyName);
                var operates = new JsonArray();
                foreach(var item in ItemActions)
                {
                    var operate = new JsonObject();
                    operate.Set("text", item.Text);
                    operate.Set("path", item.Location);
                    operate.Set("behave", ((int)item.Behave).ToString());
                    operate.Set("tips", item.ConfirmContent);
                    operates.Add(operate);
                }
                operateObject.Set("render", new JsonRaw(string.Format("oldmansoft.webman.setDataTableColumnOperate({0})", operates.ToString())));
                result.Add(operateObject);
            }
            
            return result.ToString();
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            if (TableActions.Count > 0)
            {
                var tableAction = new HtmlElement(HtmlTag.Div);
                tableAction.AddClass("dataTable-action btn-group");
                foreach(var item in TableActions)
                {
                    var a = new HtmlElement(HtmlTag.A);
                    tableAction.Append(a);
                    a.AddClass("btn");
                    a.Data("path", item.Location);
                    a.Data("behave", ((int)item.Behave).ToString());
                    a.Data("action", ((item.IsSupportParameter ? 1 : 0) + (item.IsNeedSelected ? 2 : 0)).ToString());
                    if (!string.IsNullOrEmpty(item.ConfirmContent)) a.Data("tips", item.ConfirmContent);
                    var span = new HtmlElement(HtmlTag.Span);
                    a.Append(span);
                    span.Text(item.Text);
                }
                ((IHtmlElement)tableAction).Format(outer);
                IsDisplayCheckboxColumn = true;
            }

            foreach(var item in FrontNodes)
            {
                item.Format(outer);
            }

            var name = outer.Generator.GetGeneratorName();
            AddClass(name);
            AddClass("dataTable");

            var header = new HtmlElement(HtmlTag.THead);
            base.Append(header);
            base.Append(new HtmlElement(HtmlTag.TBody));
            var footer = new HtmlElement(HtmlTag.TFoot);
            base.Append(footer);

            header.Append(CreateDefinitionColumns());
            footer.Append(CreateDefinitionColumns());
            base.Format(outer);

            foreach (var item in AfterNodes)
            {
                item.Format(outer);
            }
            outer.AddEvent(string.Format("oldmansoft.webman.setDataTable(view, '{0}', '{1}', {2});", name, DataSourceLoation, GetColumnContent()));
        }

        /// <summary>
        /// 是否显示序数
        /// </summary>
        /// <param name="value"></param>
        public void DisplayIndexColumn(bool value)
        {
            IsDisplayIndexColumn = value;
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override IHtmlElement Append(IHtmlNode node)
        {
            node.Parent.GetNodes().Remove(node);
            node.Parent = this;
            AfterNodes.Add(node);
            return this;
        }

        /// <summary>
        /// 插入无素
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override IHtmlElement Prepend(IHtmlNode node)
        {
            node.Parent.GetNodes().Remove(node);
            node.Parent = this;
            FrontNodes.Insert(0, node);
            return this;
        }

        /// <summary>
        /// 添加操作表格
        /// </summary>
        /// <param name="text"></param>
        /// <param name="location"></param>
        /// <param name="behave"></param>
        /// <returns></returns>
        public ITableAction AddActionTable(string text, ILocation location, LinkBehave behave)
        {
            var action = new DataTableAction(text, location.Path, behave);
            TableActions.Add(action);
            return action;
        }

        /// <summary>
        /// 添加操作数据项
        /// 数据项的主键将用变量 SelectedId 传递
        /// </summary>
        /// <param name="text"></param>
        /// <param name="location"></param>
        /// <param name="behave"></param>
        public IItemAction AddActionItem(string text, ILocation location, LinkBehave behave)
        {
            var action = new DataTableAction(text, location.Path, behave);
            ItemActions.Add(action);
            return action;
        }
    }
}
