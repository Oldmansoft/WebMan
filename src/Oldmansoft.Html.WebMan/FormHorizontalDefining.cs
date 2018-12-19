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
        /// <summary>
        /// 实体
        /// </summary>
        private TModel Model { get; set; }

        /// <summary>
        /// 提交路径
        /// </summary>
        private ILocation Action { get; set; }

        /// <summary>
        /// 列表源
        /// </summary>
        private ListDataSource Source { get; set; }

        /// <summary>
        /// 输入模式
        /// </summary>
        private bool InputMode { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        private Dictionary<PropertyInfo, ModelPropertyContent> Items { get; set; }

        /// <summary>
        /// 重置按钮
        /// </summary>
        public IHtmlElement ResetButton { get; set; }

        /// <summary>
        /// 提交按钮
        /// </summary>
        public IHtmlElement SubmitButton { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        private IList<IHtmlElement> Buttons { get; set; }

        internal FormHorizontalDefining(TModel model, ILocation action, ListDataSource source, bool inputMode)
        {
            Model = model;
            Action = action;
            Source = source;
            InputMode = inputMode;
            Items = new Dictionary<PropertyInfo, ModelPropertyContent>();
            InitItems(typeof(TModel));

            Buttons = new List<IHtmlElement>();
            ResetButton = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "reset").AddClass("btn btn-default");
            SubmitButton = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "submit").AddClass("btn btn-primary");
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
        /// 添加按钮
        /// </summary>
        /// <param name="button"></param>
        public void AddButton(IHtmlElement button)
        {
            if (button == null) throw new ArgumentNullException();
            Buttons.Add(button);
        }

        /// <summary>
        /// 创建表单
        /// </summary>
        /// <returns></returns>
        public FormHorizontal Create()
        {
            if (SubmitButton != null) Buttons.Insert(0, SubmitButton);
            if (ResetButton != null) Buttons.Insert(0, ResetButton);
            var form = new FormHorizontal(!InputMode, Buttons);
            if (Action != null)
            {
                form.Attribute(HtmlAttribute.Action, Action.Path);
                Action.Behave.SetTargetAttribute(form);
            }
            CreateInput(form, typeof(TModel), Model, new List<string>());
            return form;
        }
    }
}
