using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 横表单
    /// </summary>
    public class FormHorizontal : HtmlElement
    {
        /// <summary>
        /// 创建横表单
        /// </summary>
        public FormHorizontal()
            :base(HtmlTag.Form)
        {
            AddClass("form-horizontal");
            Attribute(HtmlAttribute.Method, "post");
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="text"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public FormHorizontal Add(string text, GridOption grid)
        {
            if (grid == null) throw new ArgumentNullException("grid");
            var group = new HtmlElement(HtmlTag.Div);
            Append(group);
            group.AddClass("form-group");

            var label = new HtmlElement(HtmlTag.Label);
            group.Append(label);
            label.AddClass(Column.Sm3 | Column.Md2);
            label.AddClass("control-label");
            label.Text(text);

            group.Append(grid);
            return this;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        protected override void Format(IHtmlOutput outer)
        {
            var group = new HtmlElement(HtmlTag.Div);
            Append(group);
            group.AddClass("form-group");

            var buttons = new GridOption(Column.Sm9 | Column.Md10);
            group.Append(buttons);
            buttons.AddClass(ColumnOffset.Sm3 | ColumnOffset.Md2);

            var reset = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "reset");
            group.Append(reset.CreateGrid(Column.Sm2 | Column.Xs4 | Column.Md1).AddClass(ColumnOffset.Sm3 | ColumnOffset.Md2));
            reset.AddClass("btn btn-default");

            var submit = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "submit");
            group.Append(submit.CreateGrid(Column.Sm2 | Column.Xs4 | Column.Md1));
            submit.AddClass("btn btn-primary");
            base.Format(outer);
        }

        /// <summary>
        /// 根据模型创建表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static FormHorizontal Create<TModel>(TModel model, string action, ListDataSource source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var result = new FormHorizontal();
            result.Attribute(HtmlAttribute.Action, action);
            foreach(var item in ModelProvider.Instance.GetItems(typeof(TModel)))
            {
                if (item.Hidden)
                {
                    var hidden = new FormInputCreator.Inputs.Hidden(item.Name, model != null ? item.Property.GetValue(model) : string.Empty);
                    hidden.SetInputMode();
                    result.Append(hidden);
                    continue;
                }

                var parameter = new FormInputCreator.HandlerParameter();
                parameter.ModelItem = item;
                if (model != null) parameter.Value = item.Property.GetValue(model);
                parameter.Source = source;

                var input = FormInputCreator.InputCreator.Instance.Handle(parameter);
                input.Disabled = item.Disabled;
                input.ReadOnly = item.ReadOnly;
                input.SetInputMode();
                result.Add(item.Display, input.CreateGrid(Column.Sm9 | Column.Md10));
            }
            return result;
        }

        /// <summary>
        /// 根据模型创建表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static FormHorizontal Create<TModel>(TModel model, string action)
        {
            return Create(model, action, new ListDataSource());
        }
    }
}