using Oldmansoft.Html.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 动态数据源表格
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class DynamicTable<TModel> : DataTables.Table<TModel> where TModel : class
    {
        /// <summary>
        /// 请求数据源路径
        /// </summary>
        private string DataSourceLoation { get; set; }

        /// <summary>
        /// 行样式条件集
        /// </summary>
        private List<KeyValuePair<string, string>> RowClassNameClientConditions { get; set; }

        /// <summary>
        /// 分页长度
        /// </summary>
        private uint? PageSize { get; set; }
        
        /// <summary>
        /// 表格操作
        /// </summary>
        private List<DynamicTableAction> TableActions { get; set; }

        /// <summary>
        /// 数据项操作
        /// </summary>
        private List<DynamicTableAction> ItemActions { get; set; }

        /// <summary>
        /// 创建表格定义
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <param name="dataSource">数据源路径</param>
        internal DynamicTable(Expression<Func<TModel, object>> primaryKey, string dataSource)
            : base(primaryKey)
        {
            DataSourceLoation = dataSource ?? throw new ArgumentNullException("dataSource");
            RowClassNameClientConditions = new List<KeyValuePair<string, string>>();
            TableActions = new List<DynamicTableAction>();
            ItemActions = new List<DynamicTableAction>();
        }
        
        private JsonArray GetColumnContentScript()
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

            foreach (var column in Columns.Values)
            {
                var item = new JsonObject();
                item.Set("data", column.Name);
                if (!column.Visible)
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
                    if (item.HideCondition != null)
                    {
                        operate.Set("hide", new JsonRaw(string.Format("function(data){{ return {0}; }}", item.HideCondition)));
                    }
                    if (item.DisableCondition != null)
                    {
                        operate.Set("disabled", new JsonRaw(string.Format("function(data){{ return {0}; }}", item.DisableCondition)));
                    }
                    if (item.Behave == LinkBehave.Script)
                    {
                        operate.Set("script", new JsonRaw(string.Format("function(id){{ {0} }}", item.Location.FixScriptTail())));
                    }
                    operates.Add(operate);
                }
                operateObject.Set("render", new JsonRaw(string.Format("oldmansoft.webman.setDataTableColumnOperate({0})", operates.ToString())));
                result.Add(operateObject);
            }
            
            return result;
        }

        private JsonArray GetItemActionScript()
        {
            var result = new JsonArray();
            foreach (var item in ItemActions)
            {
                var operate = new JsonObject();
                if (item.Behave == LinkBehave.Script)
                {
                    operate.Set("script", new JsonRaw(string.Format("function({0}){{ {1} }}", GetSelectedParameterName(), item.Location.FixScriptTail())));
                }
                else
                {
                    operate.Set("path", item.Location);
                }
                operate.Set("behave", (int)item.Behave);
                operate.Set("tips", item.ConfirmContent);
                result.Add(operate);
            }
            return result;
        }

        private JsonArray GetTableActionScript()
        {
            var result = new JsonArray();
            foreach(var item in TableActions)
            {
                var operate = new JsonObject();
                if (item.Behave == LinkBehave.Script)
                {
                    operate.Set("script", new JsonRaw(string.Format("function({0}){{ {1} }}", GetSelectedParameterName(), item.Location.FixScriptTail())));
                }
                else
                {
                    operate.Set("path", item.Location);
                }
                operate.Set("behave", (int)item.Behave);
                operate.Set("tips", item.ConfirmContent);
                operate.Set("other", (item.IsSupportParameter ? 1 : 0) + (item.IsNeedSelected ? 2 : 0));
                result.Add(operate);
            }
            return result;
        }

        private JsonRaw GetCreatedRowScript()
        {
            var outer = new StringBuilder();
            outer.AppendLine("function(row, data, dataIndex){");
            foreach (var item in RowClassNameClientConditions)
            {
                outer.Append("if(");
                outer.Append(item.Value);
                outer.AppendLine("){");
                outer.AppendFormat("$(row).addClass('{0}');", item.Key);
                outer.AppendLine();
                outer.AppendLine("}");
            }
            outer.Append("}");
            return new JsonRaw(outer.ToString());
        }

        private string GetOptionScript()
        {
            var result = new JsonObject();
            result.Set("tableActions", GetTableActionScript());
            result.Set("itemActions", GetItemActionScript());
            result.Set("columns", GetColumnContentScript());
            if (PageSize.HasValue) result.Set("size", PageSize.Value);
            if (RowClassNameClientConditions.Count > 0) result.Set("createdRow", GetCreatedRowScript());
            return result.ToString();
        }

        /// <summary>
        /// 创建头列
        /// </summary>
        /// <returns></returns>
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
            foreach (var column in Columns.Values)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text(column.Text));
            }
            if (ItemActions.Count > 0)
            {
                result.Append(new HtmlElement(HtmlTag.Th).Text("操作"));
            }
            return result;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            var name = outer.Generator.GetGeneratorName();
            SetTableAction(outer, name);

            foreach (var item in FrontNodes)
            {
                item.Format(outer);
            }

            AddClass(name);
            AddClass("dataTable");
            AddClass("hover");
            Data("parameter", GetSelectedParameterName());

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
            outer.AddEvent(AppEvent.Load,
                string.Format(
                    "oldmansoft.webman.setDataTable(view, '{0}', '{1}', {2});",
                    name,
                    DataSourceLoation,
                    GetOptionScript()
                )
            );
        }

        private void SetTableAction(IHtmlOutput outer, string name)
        {
            if (TableActions.Count == 0) return;

            var tableAction = new HtmlElement(HtmlTag.Div);
            tableAction.AddClass("dataTable-action btn-group");
            tableAction.Data("target", name);
            for (var i = 0; i < TableActions.Count; i++)
            {
                var item = TableActions[i];
                var a = new HtmlElement(HtmlTag.A);
                tableAction.Append(a);
                a.AddClass("btn");
                a.Data("index", i.ToString());
                a.Data("path", item.Location);
                a.Data("behave", ((int)item.Behave).ToString());
                a.Data("other", ((item.IsSupportParameter ? 1 : 0) + (item.IsNeedSelected ? 2 : 0)).ToString());
                if (!string.IsNullOrEmpty(item.ConfirmContent)) a.Data("tips", item.ConfirmContent);
                var span = new HtmlElement(HtmlTag.Span);
                a.Append(span);
                span.Text(item.Text);

                if (item.IsSupportParameter)
                {
                    IsDisplayCheckboxColumn = true;
                }
            }
            ((IHtmlElement)tableAction).Format(outer);
        }

        /// <summary>
        /// 设置页大小
        /// </summary>
        /// <param name="size"></param>
        public void SetPageSize(uint size)
        {
            if (size == 0) return;
            PageSize = size;
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
        /// 根据客户脚本条件设置行样式
        /// </summary>
        /// <param name="className">样式名</param>
        /// <param name="condition">脚本条件</param>
        public void SetRowClassNameWhenClientCondition(string className, string condition)
        {
            if (string.IsNullOrWhiteSpace(className)) throw new ArgumentNullException("className");
            if (string.IsNullOrWhiteSpace(condition)) throw new ArgumentNullException("condition");

            RowClassNameClientConditions.Add(new KeyValuePair<string, string>(className, condition));
        }

        /// <summary>
        /// 添加操作表格
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public ITableAction AddActionTable(ILocation location)
        {
            if (location == null) throw new ArgumentNullException("location");
            if (location.Behave == LinkBehave.Script) throw new ArgumentException("路径不能设置 LinkBehave.Script", "location.Behave");

            var action = new DynamicTableAction(location.Display, location.Path, location.Behave);
            TableActions.Add(action);
            return action;
        }

        /// <summary>
        /// 添加操作表格
        /// 被选择的数据项的主键将用脚本参数 (默认 selectedId) 传递
        /// </summary>
        /// <param name="display"></param>
        /// <param name="script">脚本参数 (默认 selectedId)</param>
        /// <returns></returns>
        public ITableAction AddActionTable(string display, string script)
        {
            if (string.IsNullOrWhiteSpace(display)) throw new ArgumentNullException("display");
            if (string.IsNullOrWhiteSpace(script)) throw new ArgumentNullException("script");

            var action = new DynamicTableAction(display, script, LinkBehave.Script);
            TableActions.Add(action);
            return action;
        }

        /// <summary>
        /// 添加操作数据项
        /// 数据项的主键将用变量 (默认 selectedId) 传递
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public IDynamicTableItemAction AddActionItem(ILocation location)
        {
            if (location == null) throw new ArgumentNullException("location");
            if (location.Behave == LinkBehave.Script) throw new ArgumentException("路径不能设置 LinkBehave.Script", "location.Behave");

            var action = new DynamicTableAction(location.Display, location.Path, location.Behave);
            ItemActions.Add(action);
            return action;
        }

        /// <summary>
        /// 添加操作数据项
        /// 数据项的主键将用脚本参数 (默认 selectedId) 传递
        /// </summary>
        /// <param name="display">显示文字</param>
        /// <param name="script">脚本参数 (默认 selectedId)</param>
        /// <returns></returns>
        public IDynamicTableItemAction AddActionItem(string display, string script)
        {
            if (string.IsNullOrWhiteSpace(display)) throw new ArgumentNullException("display");
            if (string.IsNullOrWhiteSpace(script)) throw new ArgumentNullException("script");

            var action = new DynamicTableAction(display, script, LinkBehave.Script);
            ItemActions.Add(action);
            return action;
        }
    }
}
