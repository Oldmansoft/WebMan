using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        private Dictionary<PropertyInfo, ModelPropertyContent> Items { get; set; }

        internal FormHorizontalDefining(TModel model, ILocation action, ListDataSource source, bool inputMode)
        {
            Model = model;
            Action = action;
            Source = source;
            InputMode = inputMode;
            Items = new Dictionary<PropertyInfo, ModelPropertyContent>();
            InitItems(typeof(TModel));
        }

        private void InitItems(Type type)
        {
            foreach (var item in Util.ModelProvider.Instance.GetItems(type))
            {
                if (item.Expansion)
                {
                    InitItems(item.Property.PropertyType);
                    continue;
                }
                Items.Add(item.Property, item);
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
                var key = property.GetProperty();
                if (!Items.ContainsKey(key)) return null;
                return Items[key];
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
            CreateInput(form, typeof(TModel), Model, new List<string>());
            return form;
        }

        private void CreateInput(FormHorizontal form, Type type, object model, List<string> parents)
        {
            foreach (var item in Util.ModelProvider.Instance.GetItems(type))
            {
                var parentsAndCurrent = new List<string>();
                parentsAndCurrent.AddRange(parents);
                parentsAndCurrent.Add(item.Name);
                if (item.Expansion)
                {
                    CreateInput(form, item.Property.PropertyType, model == null ? null : item.Property.GetValue(model), parentsAndCurrent);
                    continue;
                }
                var name = string.Join(".", parentsAndCurrent);
                var content = Items[item.Property];
                var value = model == null ? null : content.Property.GetValue(model);
                if (content.Hidden)
                {
                    var hidden = new FormInputCreator.Inputs.Hidden();
                    hidden.Init(content, name, value != null ? value : string.Empty, null);
                    hidden.SetInputMode();
                    hidden.AppendTo(form);
                    continue;
                }
                
                var parameter = new FormInputCreator.HandlerParameter(content, name, value, Source, form.Script, form.Validator, content.HtmlData);
                var input = FormInputCreator.InputCreator.Instance.Handle(parameter);
                if (InputMode)
                {
                    input.SetInputMode();
                }
                else
                {
                    input.SetViewMode();
                }
                form.Add(content.Display, input.CreateGrid(Column.Sm9 | Column.Md10));
                content.SetValidate(form.Validator, name);
            }
        }
    }
}
