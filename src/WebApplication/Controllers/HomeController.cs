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
            var document = new MainDocument();
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/Scripts/oldmansoft-webapp.cn.js")));
            document.Title = "WebMan";
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
    }
}