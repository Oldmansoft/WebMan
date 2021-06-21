using System;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 登录文档
    /// </summary>
    public class LoginDocument : HtmlDocument
    {
        /// <summary>
        /// 登录面板
        /// </summary>
        public Panel LoginPanel { get; private set; }

        /// <summary>
        /// 创建登录文档
        /// </summary>
        /// <param name="seed">加密种子路径</param>
        /// <param name="action">登录处理地址</param>
        /// <param name="returnUrl">返回地址</param>
        public LoginDocument(ILocation seed, ILocation action, string returnUrl = null)
        {
            if (seed == null) throw new ArgumentNullException("seed");
            if (action == null) throw new ArgumentNullException("action");
            if (action.Behave != LinkBehave.Call) throw new ArgumentException("需要返回 DealResult JSON", "action");

            var container = new HtmlElement(HtmlTag.Div).AddClass("container-full");
            Body.Append(container);

            var center = new HtmlElement(HtmlTag.Div).AddClass("layout-center");
            container.Append(center);

            var content = new HtmlElement(HtmlTag.Div).AddClass("layout-center-content");
            center.Append(content);

            LoginPanel = new Panel();
            content.Append(LoginPanel);
            LoginPanel.Caption = "登录";
            LoginPanel.Icon = FontAwesome.Unlock_Alt;
            LoginPanel.AddClass("hide-webapp-close");
            LoginPanel.Append(CreateForm(action.Path, returnUrl));

            var scriptContent = string.Format("oldmansoft.webman.setLoginSubmit('form', '{0}', 'input[name=Account]', 'input[name=Password]');", seed.Path);
            var script = new Element.Script(scriptContent);
            Body.Append(script);
        }

        /// <summary>
        /// 格式化之前
        /// </summary>
        protected override void BeforeFormat()
        {
            base.BeforeFormat();

            foreach (var item in InitAfterScripts)
            {
                Body.Append(item);
            }
        }

        private IHtmlElement CreateForm(string action, string returnUrl)
        {
            var form = new HtmlElement(HtmlTag.Form).Attribute(HtmlAttribute.Method, "post").Attribute(HtmlAttribute.Action, action);
            if (returnUrl != null)
            {
                var hidden = new HtmlElement(HtmlTag.Input);
                hidden.Attribute(HtmlAttribute.Name, "ReturnUrl");
                hidden.Attribute(HtmlAttribute.Type, "hidden");
                hidden.Attribute(HtmlAttribute.Value, returnUrl);
                form.Append(hidden);
            }

            form.AddClass("form-horizontal");
            form.Append(CreateFormGroup("帐号", "Account", "text"));
            form.Append(CreateFormGroup("密码", "Password", "password"));

            var group = new HtmlElement(HtmlTag.Div);
            form.Append(group);
            group.AddClass("form-group btn-group-center");

            var submit = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "submit").Attribute(HtmlAttribute.Value, "提交");
            submit.AddClass("btn btn-primary");
            group.Append(submit);

            return form;
        }

        private IHtmlElement CreateFormGroup(string text, string name, string type)
        {
            var group = new HtmlElement(HtmlTag.Div);
            group.AddClass("form-group");

            var label = new HtmlElement(HtmlTag.Label);
            group.Append(label);
            label.AddClass("col-sm-3 col-md-2 control-label");
            label.Text(text);

            var div = new HtmlElement(HtmlTag.Div);
            group.Append(div);
            div.AddClass("col-sm-9 col-md-10");

            var input = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Name, name);
            div.Append(input);
            input.Attribute(HtmlAttribute.Type, type);
            input.AddClass("form-control");
            return group;
        }
    }
}
