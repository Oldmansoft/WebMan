using Oldmansoft.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oldmansoft.Html.Mvc;
using Oldmansoft.Html.WebMan;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var document = new MainDocument("/Home/Welcome");
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/Scripts/oldmansoft-webapp.cn.js")));
            document.Title = "WebMan";
            document.Menu.Add(new TreeListBranch(new LinkContent("欢迎", "/Home/Welcome", FontAwesome.Home)));
            document.Taskbar.Add(new LinkContent(FontAwesome.Male));
            document.Taskbar.Add(new LinkContent(FontAwesome.Envelope));
            document.Account = new QuickMenu();
            document.Account.Image = "http://wx.qlogo.cn/mmopen/Q3auHgzwzM6SkLNbjL7Vq3koXuoq1PSv6Nlhp7AmfQjsB02hYG37blVecGupjK3GXm1iaYUKNh2z3PU8R6mo2HRTGfo066Fc2PCibT5B0asBo/0";
            document.Account.Text = "Oldman 老人";
            document.SearchAction = "/Home/Search";
            return new HtmlResult(document);
        }

        [AllowAnonymous]
        public string Seed()
        {
            string seed = Guid.NewGuid().ToString().Substring(0, 4);
            TempData["HashSeed"] = seed;
            return seed;
        }

        public ActionResult Login(string tips)
        {
            var document = new LoginDocument("/Home/Seed");
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/Scripts/oldmansoft-webapp.cn.js")));
            document.Title = "WebMan";
            if (!string.IsNullOrEmpty(tips))
            {
                document.Body.Append(new Oldmansoft.Html.Element.Script("$app.alert('" + tips + "')"));
            }
            return new HtmlResult(document);
        }

        [HttpPost]
        public ActionResult Login(Models.LoginModel model)
        {
            if (model.Account == "root" && !string.IsNullOrEmpty(model.DoubleHash))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Login("帐号或密码错误");
            }
        }

        public ActionResult Welcome()
        {
            var panel = new Panel();
            panel.Caption = "hello";
            panel.Icon = FontAwesome.Anchor;

            panel.Text("hello, world");
            
            return new HtmlResult(panel.CreateLayout());
        }
    }
}