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
        /// <param name="seedPath">加密种子路径</param>
        /// <param name="action">登录处理地址</param>
        public LoginDocument(string seedPath, string action)
        {
            var container = new HtmlElement(HtmlTag.Div).AddClass("container-fluid");
            Body.Append(container);

            var row = new HtmlElement(HtmlTag.Div).AddClass("row");
            container.Append(row);

            var col = new HtmlElement(HtmlTag.Div).AddClass("col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3");
            row.Append(col);

            var form = new HtmlElement(HtmlTag.Form).Attribute(HtmlAttribute.Method, "post").Attribute(HtmlAttribute.Action, action);
            col.Append(form);
            
            var inputAccount = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Name, "Account");
            form.Append(inputAccount);

            var inputPassword = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Name, "Password").Attribute(HtmlAttribute.Type, "password");
            form.Append(inputPassword);

            var inputSubmit = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "submit").Attribute(HtmlAttribute.Value, "提交");
            form.Append(inputSubmit);
            
            var scriptContent = string.Format("window.oldmansoft.webman.setLoginSubmit('form', '{0}', 'input[name=Account]', 'input[name=Password]');", seedPath);
            var script = new Element.Script(scriptContent);
            Body.Append(script);
        }
    }
}
