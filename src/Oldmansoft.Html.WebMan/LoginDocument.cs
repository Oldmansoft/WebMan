using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    public class LoginDocument : HtmlDocument
    {
        public LoginDocument(string seedPath)
        {
            var container = new HtmlElement(HtmlTag.Div).AddClass("container-fluid");
            Body.Append(container);
            var row = new HtmlElement(HtmlTag.Div).AddClass("row");
            container.Append(row);
            var col = new HtmlElement(HtmlTag.Div).AddClass("col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3");
            row.Append(col);
            var form = new HtmlElement(HtmlTag.Form).Attribute(HtmlAttribute.Method, "post");
            col.Append(form);
            var inputDoubleHash = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Name, "DoubleHash").Attribute(HtmlAttribute.Type, "hidden");
            form.Append(inputDoubleHash);
            var inputAccount = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Name, "Account");
            form.Append(inputAccount);
            var inputPassword = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Name, "Password").Attribute(HtmlAttribute.Type, "password");
            form.Append(inputPassword);
            var inputSubmit = new HtmlElement(HtmlTag.Input).Attribute(HtmlAttribute.Type, "submit").Attribute(HtmlAttribute.Value, "提交");
            form.Append(inputSubmit);

            var script = new Element.Script("window.oldmansoft.webman.setLoginSubmit('form', '" + seedPath + "', 'input[name=Account]', 'input[name=Password]', 'input[name=DoubleHash]');");
            Body.Append(script);
        }
    }
}
