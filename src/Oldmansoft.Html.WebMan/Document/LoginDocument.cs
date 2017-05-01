using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 登录文档
    /// </summary>
    public class LoginDocument : HtmlDocument
    {
        /// <summary>
        /// 创建登录文档
        /// </summary>
        /// <param name="seed">加密种子路径</param>
        /// <param name="action">登录处理地址</param>
        public LoginDocument(ILocation seed, ILocation action)
        {
            var container = new HtmlElement(HtmlTag.Div).AddClass("container-fluid");
            Body.Append(container);

            var row = new HtmlElement(HtmlTag.Div).AddClass("row");
            container.Append(row);

            var col = new HtmlElement(HtmlTag.Div).AddClass("col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3");
            row.Append(col);

            var panel = new Panel();
            col.Append(panel);
            panel.Caption = "登录";
            panel.Icon = FontAwesome.Unlock_Alt;
            panel.AddClass("hide-webapp-close");
            panel.Append(CreateForm(action.Path));
            
            var scriptContent = string.Format("oldmansoft.webman.setLoginSubmit('form', '{0}', 'input[name=Account]', 'input[name=Password]');", seed.Path);
            var script = new Element.Script(scriptContent);
            Body.Append(script);
        }

        private IHtmlElement CreateForm(string action)
        {
            var form = new HtmlElement(HtmlTag.Form).Attribute(HtmlAttribute.Method, "post").Attribute(HtmlAttribute.Action, action);
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
