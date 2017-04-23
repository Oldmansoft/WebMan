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
            var document = new MainDocument(Url.Location(Welcome));
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/Scripts/oldmansoft-webman.cn.js")));
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource("//cdn.bootcss.com/bootstrap-validator/0.5.3/js/language/zh_CN.min.js"));
            document.Resources.Markdown.Enabled = true;
            document.Title = "WebMan";
            document.Menu.Add(new TreeListBranch(new LinkContent("表格", Url.Location<DataTablesController>(o => o.Index), FontAwesome.Tablet)));
            document.Menu.Add(
                new TreeListBranch(new LinkContent("组合", FontAwesome.Suitcase)).Add(
                    new TreeListBranch(new LinkContent("组合", FontAwesome.Home)).Add(
                        new TreeListLeaf(new LinkContent("欢迎", Url.Location(Welcome), FontAwesome.Home))
                    )
                )
            );

            document.Taskbar.Add(new LinkContent(FontAwesome.Male));
            document.Taskbar.Add(new LinkContent(FontAwesome.Envelope));
            document.Account = new QuickMenu();
            document.Account.Image = "http://wx.qlogo.cn/mmopen/Q3auHgzwzM6SkLNbjL7Vq3koXuoq1PSv6Nlhp7AmfQjsB02hYG37blVecGupjK3GXm1iaYUKNh2z3PU8R6mo2HRTGfo066Fc2PCibT5B0asBo/0";
            document.Account.Text = "Oldman 老人";
            document.SetSearchAction("/Home/Search");
            return new HtmlResult(document);
        }

        [AllowAnonymous]
        public ActionResult Seed()
        {
            string seed = Guid.NewGuid().ToString().Substring(0, 4);
            TempData["HashSeed"] = seed;
            return Content(seed);
        }

        public ActionResult Login()
        {
            var document = new LoginDocument(Url.Location(Seed), Url.Location(new Func<Models.LoginModel, JsonResult>(Login)));
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/Scripts/oldmansoft-webman.cn.js")));
            document.Title = "WebMan";
            return new HtmlResult(document);
        }

        [HttpPost]
        public JsonResult Login(Models.LoginModel model)
        {
            if (model.Account == "root" && !string.IsNullOrEmpty(model.Hash))
            {
                return Json(DealResult.Location(Url.Location(new Func<ActionResult>(Index))));
            }
            else
            {
                return Json(DealResult.CreateWrong("帐号或密码错误"));
            }
        }

        public ActionResult Welcome()
        {
            var panel = new Panel();
            panel.Caption = "hello";
            panel.Icon = FontAwesome.Anchor;
            var form = new FormHorizontal();
            form.Add("名称", new HtmlElement(HtmlTag.Input).AddClass("form-control").CreateGrid(Column.Sm9 | Column.Md10));
            form.Add("内容", new HtmlElement(HtmlTag.Input).AddClass("form-control").CreateGrid(Column.Sm9 | Column.Md10));
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }
    }
}