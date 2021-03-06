﻿using System;
using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 横表单
    /// </summary>
    public class FormHorizontal : HtmlElement
    {
        private IList<IHtmlElement> Buttons { get; set; }

        /// <summary>
        /// 脚本
        /// </summary>
        public Input.ScriptRegister Script { get; private set; }

        /// <summary>
        /// 验证器
        /// </summary>
        public FormValidate.FormValidator Validator { get; private set; }
        
        /// <summary>
        /// 查看模式
        /// </summary>
        public bool ViewMode { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="viewMode"></param>
        /// <param name="buttons"></param>
        public FormHorizontal(bool viewMode, IList<IHtmlElement> buttons)
            :base(HtmlTag.Form)
        {
            AddClass("form-horizontal");
            Attribute(HtmlAttribute.Method, "post");

            Script = new Input.ScriptRegister();
            Validator = new FormValidate.FormValidator();
            ViewMode = viewMode;
            Buttons = buttons ?? new List<IHtmlElement>();
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

            AddButtonGroup();
            base.Format(outer);
            if (ViewMode)
            {
                outer.AddEvent(AppEvent.Load, Script.ToString());
            }
            else
            {
                outer.AddEvent(AppEvent.Load, string.Format("oldmansoft.webman.setFormValidate(view, '{0}', {1});{2}", name, Validator.Create(), Script.ToString()));
            }
        }

        private void AddButtonGroup()
        {
            if (Buttons.Count == 0) return;

            var group = new HtmlElement(HtmlTag.Div);
            Append(group);
            group.AddClass("form-group");
            for (var i = 0; i < Buttons.Count; i++)
            {
                var container = new HtmlElement(HtmlTag.Div);
                if (i == 0) container.AddClass(ColumnOffset.Sm3 | ColumnOffset.Md2);
                container.AddClass("col-container");
                container.Append(Buttons[i]);
                container.AppendTo(group);
            }
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
            return new FormHorizontalDefining<TModel>(model, action, source, true).Create();
        }

        /// <summary>
        /// 根据模型定义查看表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static FormHorizontalDefining<TModel> Define<TModel>(TModel model, ILocation action, ListDataSource source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return new FormHorizontalDefining<TModel>(model, action, source, true);
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
        /// 根据模型定义查看表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static FormHorizontalDefining<TModel> Define<TModel>(TModel model, ILocation action)
        {
            return Define(model, action, new ListDataSource());
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
            return new FormHorizontalDefining<TModel>(model, null, source, false).Create();
        }

        /// <summary>
        /// 根据模型定义查看表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static FormHorizontalDefining<TModel> Define<TModel>(TModel model, ListDataSource source)
        {
            if (source == null) throw new ArgumentNullException("source");
            return new FormHorizontalDefining<TModel>(model, null, source, false);
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

        /// <summary>
        /// 根据模型定义查看表单
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static FormHorizontalDefining<TModel> Define<TModel>(TModel model)
        {
            return Define(model, new ListDataSource());
        }
    }
}