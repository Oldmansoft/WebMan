using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 横表单定义
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class FormHorizontalDefining<TModel>
    {
        private TModel Model { get; set; }

        private ILocation Action { get; set; }

        private ListDataSource Source { get; set; }

        private bool InputMode { get; set; }

        private Dictionary<string, ModelPropertyContent> Items { get; set; }

        internal FormHorizontalDefining(TModel model, ILocation action, ListDataSource source, bool inputMode)
        {
            Model = model;
            Action = action;
            Source = source;
            InputMode = inputMode;
            Items = new Dictionary<string, ModelPropertyContent>();
            foreach (var item in Util.ModelProvider.Instance.GetItems(typeof(TModel)))
            {
                Items.Add(item.Name, item);
            }
        }

        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public ModelPropertyContent this[System.Linq.Expressions.Expression<Func<TModel, object>> property]
        {
            get
            {
                var name = property.GetProperty().Name;
                if (!Items.ContainsKey(name)) return null;
                return Items[name];
            }
        }

        /// <summary>
        /// 创建表单
        /// </summary>
        /// <returns></returns>
        public FormHorizontal Create()
        {
            var form = new FormHorizontal();
            form.ViewMode = !InputMode;
            form.UseButtonGroup = InputMode;
            if (Action != null)
            {
                form.Attribute(HtmlAttribute.Action, Action.Path);
                Action.Behave.SetTargetAttribute(form);
            }
            foreach (var item in Items.Values)
            {
                if (item.Hidden)
                {
                    var hidden = new FormInputCreator.Inputs.Hidden();
                    hidden.Init(item, Model != null ? item.Property.GetValue(Model) : string.Empty, null);
                    hidden.SetInputMode();
                    hidden.AppendTo(form);
                    continue;
                }

                var parameter = new FormInputCreator.HandlerParameter(item, Model, Source, form.Script, form.Validator, item.HtmlData);
                var input = FormInputCreator.InputCreator.Instance.Handle(parameter);
                if (InputMode)
                {
                    input.SetInputMode();
                }
                else
                {
                    input.SetViewMode();
                }
                form.Add(item.Display, input.CreateGrid(Column.Sm9 | Column.Md10));
                item.SetValidate(form.Validator);
            }
            return form;
        }
    }
}
