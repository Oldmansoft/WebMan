using Oldmansoft.Html.Util;
using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 静态表格
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class StaticTable<TModel> : DataTables.Table<TModel>
        where TModel : class
    {
        private IEnumerable<TModel> Source { get; set; }
        
        /// <summary>
        /// 行样式条件集
        /// </summary>
        private List<KeyValuePair<string, Func<TModel, bool>>> RowClassNameConditions { get; set; }

        /// <summary>
        /// 表格操作
        /// </summary>
        private List<StaticTableAction<TModel>> TableActions { get; set; }

        /// <summary>
        /// 数据项操作
        /// </summary>
        private List<StaticTableAction<TModel>> ItemActions { get; set; }

        /// <summary>
        /// 是否显示表格信息
        /// </summary>
        private bool IsDisplayTableInfo { get; set; }

        private Func<int, TModel, HtmlElement> RenderRowBeforeContent { get; set; }

        private Func<int, TModel, HtmlElement> RenderRowAfterContent { get; set; }

        /// <summary>
        /// 创建静态表格
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="source"></param>
        internal StaticTable(Expression<Func<TModel, object>> primaryKey, IEnumerable<TModel> source)
            : base(primaryKey)
        {
            Tag = HtmlTag.Div;
            AddClass("dataTables_wrapper");

            Source = source;
            RowClassNameConditions = new List<KeyValuePair<string, Func<TModel, bool>>>();
            TableActions = new List<StaticTableAction<TModel>>();
            ItemActions = new List<StaticTableAction<TModel>>();
            IsDisplayTableInfo = true;
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
            foreach (var item in TableActions)
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

        private string GetOptionScript()
        {
            var result = new JsonObject();
            result.Set("tableActions", GetTableActionScript());
            result.Set("itemActions", GetItemActionScript());
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
                if (!column.Visible) continue;
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

            var table = new HtmlElement(HtmlTag.Table);
            table.AddClass(name);
            table.AddClass("dataTable");
            table.AddClass("hover");
            table.Data("parameter", GetSelectedParameterName());

            var header = new HtmlElement(HtmlTag.THead);
            table.Append(header);
            var tbody = new HtmlElement(HtmlTag.TBody);
            var count = SetBody(tbody);
            table.Append(tbody);
            var footer = new HtmlElement(HtmlTag.TFoot);
            table.Append(footer);

            header.Append(CreateDefinitionColumns());
            footer.Append(CreateDefinitionColumns());

            base.Append(table);
            if (IsDisplayTableInfo) base.Append(GetBodyTableInfo(count));
            base.Format(outer);

            foreach (var item in AfterNodes)
            {
                item.Format(outer);
            }

            outer.AddEvent(AppEvent.Load,
                string.Format(
                    "oldmansoft.webman.setStaticTable(view, '{0}', {1});",
                    name,
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
                var action = TableActions[i];
                var a = new HtmlElement(HtmlTag.A);
                tableAction.Append(a);
                a.AddClass("btn");
                a.Data("index", i.ToString());
                a.Data("path", action.Location);
                a.Data("behave", ((int)action.Behave).ToString());
                a.Data("other", ((action.IsSupportParameter ? 1 : 0) + (action.IsNeedSelected ? 2 : 0)).ToString());
                if (!string.IsNullOrEmpty(action.ConfirmContent)) a.Data("tips", action.ConfirmContent);
                var span = new HtmlElement(HtmlTag.Span);
                a.Append(span);
                span.Text(action.Text);

                if (action.IsSupportParameter)
                {
                    IsDisplayCheckboxColumn = true;
                }
            }
            ((IHtmlElement)tableAction).Format(outer);
        }

        private void SetBodyEmpty(HtmlElement tbody)
        {
            var columnLength = 0;
            if (IsDisplayCheckboxColumn) columnLength++;
            if (IsDisplayIndexColumn) columnLength++;
            foreach (var column in Columns.Values)
            {
                if (!column.Visible) continue;
                columnLength++;
            }
            if (ItemActions.Count > 0) columnLength++;

            var tr = new HtmlElement(HtmlTag.Tr);
            tr.AppendTo(tbody);
            var td = new HtmlElement(HtmlTag.Td);
            td.Attribute(HtmlAttribute.Colspan, columnLength.ToString());
            td.Attribute(HtmlAttribute.VAlign, "top");
            td.AddClass("dataTables_empty");
            td.Text("空");
            td.AppendTo(tr);
        }

        private IHtmlElement GetBodyTableInfo(int count)
        {
            var content = new HtmlElement(HtmlTag.Div);
            content.AddClass("table-content");
            
            var col = new HtmlElement(HtmlTag.Div);
            col.AddClass(Column.Sm12);
            col.AppendTo(content);

            var info = new HtmlElement(HtmlTag.Div);
            info.AddClass("dataTables_info");
            info.Text(string.Format("共 {0} 条数据", count));
            info.AppendTo(col);
            return content;
        }

        private int SetBody(HtmlElement tbody)
        {
            if (Source == null)
            {
                SetBodyEmpty(tbody);
                return 0;
            }
            var index = 0;
            foreach (var model in Source)
            {
                index++;
                if (model == null) continue;
                var modelType = model.GetType();
                var id = PrimaryKeyFunc(model);

                var tr = new HtmlElement(HtmlTag.Tr);
                foreach (var condition in RowClassNameConditions)
                {
                    if (condition.Value(model)) tr.AddClass(condition.Key);
                }
                if (RenderRowBeforeContent != null) RenderRowBeforeContent(index, model).AppendTo(tbody);
                tr.AppendTo(tbody);
                if (RenderRowAfterContent != null) RenderRowAfterContent(index, model).AppendTo(tbody);

                if (IsDisplayCheckboxColumn)
                {
                    var td = new HtmlElement(HtmlTag.Td);
                    td.AppendTo(tr);
                    var input = new HtmlElement(HtmlTag.Input);
                    input.Attribute(HtmlAttribute.Type, "checkbox");
                    input.Attribute(HtmlAttribute.Value, id.GetNotNullString());
                    input.AppendTo(td);
                }
                if (IsDisplayIndexColumn)
                {
                    var td = new HtmlElement(HtmlTag.Td);
                    td.AppendTo(tr);
                    td.Text(index.ToString());
                }
                SetColumns(model, modelType, new List<string>(), tr);
                if (ItemActions.Count > 0)
                {
                    var td = new HtmlElement(HtmlTag.Td);
                    td.AppendTo(tr);
                    var div = new HtmlElement(HtmlTag.Div);
                    div.AddClass("dataTable-item-action");
                    div.Data("id", id.GetNotNullString());
                    div.AppendTo(td);
                    for (var i = 0; i < ItemActions.Count; i++)
                    {
                        var action = ItemActions[i];
                        if (action.HideCondition != null && action.HideCondition(model)) continue;
                        var a = new HtmlElement(HtmlTag.A);
                        a.Data("index", i.ToString());
                        a.Text(action.Text);
                        if (action.DisableCondition != null && action.DisableCondition(model)) a.AddClass("disabled");
                        a.AppendTo(div);
                    }
                }
            }

            if (index == 0)
            {
                SetBodyEmpty(tbody);
            }
            return index;
        }

        private void SetColumns(object model, Type modelType, List<string> parents, HtmlElement tr)
        {
            foreach (var item in ModelProvider.Instance.GetItems(modelType))
            {
                var parentsAndCurrent = new List<string>();
                parentsAndCurrent.AddRange(parents);
                parentsAndCurrent.Add(item.Name);
                if (item.Expansion)
                {
                    SetColumns(GetValueFromModel(model, item.Property), item.Property.PropertyType, parentsAndCurrent, tr);
                    continue;
                }
                var itemName = string.Join("-", parentsAndCurrent);
                if (!Columns[itemName].Visible) continue;

                var node = CreateNodeFromModel(model, item);
                var td = new HtmlElement(HtmlTag.Td);
                td.Append(node);
                td.AppendTo(tr);
            }
        }

        private object GetValueFromModel(object model, PropertyInfo property)
        {
            if (model == null) return null;
            if (!property.CanRead) return null;
            return property.GetValue(model);
        }

        private HtmlNode CreateNodeFromModel(object model, ModelPropertyContent propertyContent)
        {
            var value = GetValueFromModel(model, propertyContent.Property);
            if (value == null) return new HtmlText(string.Empty);
            return ValueDisplay.Instance.Convert(propertyContent.Property.PropertyType, value, propertyContent);
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
        public void SetRowClassNameWhenCondition(string className, Func<TModel, bool> condition)
        {
            if (string.IsNullOrWhiteSpace(className)) throw new ArgumentNullException("className");
            if (condition == null) throw new ArgumentNullException("condition");

            RowClassNameConditions.Add(new KeyValuePair<string, Func<TModel, bool>>(className, condition));
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

            var action = new StaticTableAction<TModel>(location.Display, location.Path, location.Behave);
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

            var action = new StaticTableAction<TModel>(display, script, LinkBehave.Script);
            TableActions.Add(action);
            return action;
        }

        /// <summary>
        /// 添加操作数据项
        /// 数据项的主键将用变量 (默认 selectedId) 传递
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public IStaticTableItemAction<TModel> AddActionItem(ILocation location)
        {
            if (location == null) throw new ArgumentNullException("location");
            if (location.Behave == LinkBehave.Script) throw new ArgumentException("路径不能设置 LinkBehave.Script", "location.Behave");

            var action = new StaticTableAction<TModel>(location.Display, location.Path, location.Behave);
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
        public IStaticTableItemAction<TModel> AddActionItem(string display, string script)
        {
            if (string.IsNullOrWhiteSpace(display)) throw new ArgumentNullException("display");
            if (string.IsNullOrWhiteSpace(script)) throw new ArgumentNullException("script");

            var action = new StaticTableAction<TModel>(display, script, LinkBehave.Script);
            ItemActions.Add(action);
            return action;
        }

        /// <summary>
        /// 显示表格信息
        /// </summary>
        /// <param name="value"></param>
        public void DisplayTableInfo(bool value)
        {
            IsDisplayTableInfo = value;
        }

        /// <summary>
        /// 宣染行前
        /// </summary>
        /// <param name="func"></param>
        public void RenderRowBefore(Func<int, TModel, HtmlElement> func)
        {
            RenderRowBeforeContent = func;
        }

        /// <summary>
        /// 宣染行后
        /// </summary>
        /// <param name="func"></param>
        public void RenderRowAfter(Func<int, TModel, HtmlElement> func)
        {
            RenderRowAfterContent = func;
        }
    }
}