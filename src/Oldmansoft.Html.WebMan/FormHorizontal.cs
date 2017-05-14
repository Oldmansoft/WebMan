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
        /// 脚本
        /// </summary>
        public Input.ScriptRegister Script { get; private set; }

        /// <summary>
        /// 验证器
        /// </summary>
        public FormValidate.FormValidator Validator { get; private set; }

        /// <summary>
        /// 使用按钮组
        /// </summary>
        public bool UseButtonGroup { get; set; }

        /// <summary>
        /// 查看模式
        /// </summary>
        private bool ViewMode { get; set; }

        /// <summary>
        /// 创建横表单
        /// </summary>
        public FormHorizontal()
            :base(HtmlTag.Form)
        {
            AddClass("form-horizontal");
            Attribute(HtmlAttribute.Method, "post");

            Script = new Input.ScriptRegister();
            Validator = new FormValidate.FormValidator();
            UseButtonGroup = true;
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
            var name = outer.Generator.GetGeneratorName();
            AddClass(name);
            if (ViewMode)
            {
                AddClass("view-mode");
            }

            if (UseButtonGroup)
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
            }
            base.Format(outer);
            if (ViewMode)
            {
                outer.AddEvent(Script.ToString());
            }
            else
            {
                outer.AddEvent(string.Format("oldmansoft.webman.setFormValidate(view, '{0}', {1});{2}", name, Validator.Create(), Script.ToString()));
            }
        }

        private static FormHorizontal CreateForm<TModel>(TModel model, ILocation action, ListDataSource source, bool inputMode)
        {
            var result = new FormHorizontal();
            result.ViewMode = !inputMode;
            result.UseButtonGroup = inputMode;
            if (action != null)
            {
                result.Attribute(HtmlAttribute.Action, action.Path);
                action.Behave.SetTargetAttribute(result);
            }
            foreach (var item in ModelProvider.Instance.GetItems(typeof(TModel)))
            {
                if (item.Hidden)
                {
                    var hidden = new FormInputCreator.Inputs.Hidden();
                    hidden.Init(item.Name, model != null ? item.Property.GetValue(model) : string.Empty, null, null);
                    hidden.SetInputMode(item.Disabled, item.ReadOnly, item.Description);
                    result.Append(hidden);
                    continue;
                }

                var parameter = new FormInputCreator.HandlerParameter();
                parameter.ModelItem = item;
                if (model != null) parameter.Value = item.Property.GetValue(model);
                parameter.Source = source;
                parameter.Script = result.Script;

                var input = FormInputCreator.InputCreator.Instance.Handle(parameter);
                if (inputMode)
                {
                    input.SetInputMode(item.Disabled, item.ReadOnly, item.Description);
                }
                else
                {
                    input.SetViewMode();
                }
                result.Add(item.Display, input.CreateGrid(Column.Sm9 | Column.Md10));
                item.SetValidate(result.Validator);
            }
            return result;
        }

        /// <summary>
        /// 根据模型创建提交表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static FormHorizontal Create<TModel>(TModel model, ILocation action, ListDataSource source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return CreateForm(model, action, source, true);
        }

        /// <summary>
        /// 根据模型创建提交表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static FormHorizontal Create<TModel>(TModel model, ILocation action)
        {
            return Create(model, action, new ListDataSource());
        }

        /// <summary>
        /// 根据模型创建查看表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static FormHorizontal Create<TModel>(TModel model, ListDataSource source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return CreateForm(model, null, source, false);
        }

        /// <summary>
        /// 根据模型创建查看表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static FormHorizontal Create<TModel>(TModel model)
        {
            return Create(model, new ListDataSource());
        }
    }
}