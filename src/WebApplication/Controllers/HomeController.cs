using Oldmansoft.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            document.Menu.Add(new TreeListItem(Url.Location<DataTablesController>(o => o.Index).Set("id", new Guid[] { Guid.Empty, Guid.Empty })));
            document.Menu.Add(
                new TreeListItem("一级菜单", FontAwesome.Suitcase)
                .Add(
                    new TreeListItem(Url.Location(Male))
                )
                .Add(
                    new TreeListItem("二级菜单", FontAwesome.Home)
                    .Add(
                        new TreeListItem(Url.Location(Welcome))
                    )
                )
                .Add(
                    new TreeListItem(Url.Location(Envelope))
                )
            );

            document.Taskbar.Add(Url.Location(Male));
            document.Taskbar.Add(Url.Location(Envelope));

            document.Quick.Avatar.Photo = "http://wx.qlogo.cn/mmopen/Q3auHgzwzM6SkLNbjL7Vq3koXuoq1PSv6Nlhp7AmfQjsB02hYG37blVecGupjK3GXm1iaYUKNh2z3PU8R6mo2HRTGfo066Fc2PCibT5B0asBo/0";
            document.Quick.Avatar.Display = "Oldman 老人";
            document.Quick.Add(Url.Location(Male));
            document.Quick.Add(Url.Location(Logoff));

            document.SetSearchAction(Url.Location(Search));
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
                return Json(DealResult.Location(Url.Location(Index)));
            }
            else
            {
                return Json(DealResult.Wrong("帐号或密码错误"));
            }
        }

        [Location("退出", Behave = LinkBehave.Self)]
        public ActionResult Logoff()
        {
            return RedirectToAction("Login");
        }

        [Location("Search key...")]
        public ActionResult Search(string keyword)
        {
            return Content("Search" + keyword);
        }
        
        public ActionResult Welcome()
        {
            var panel = new Panel();
            panel.ConfigLocation();
            var form = new FormHorizontal();
            form.Add("名称", new HtmlElement(HtmlTag.Input).AddClass("form-control").CreateGrid(Column.Sm9 | Column.Md10));
            form.Add("内容", new HtmlElement(HtmlTag.Input).AddClass("form-control").CreateGrid(Column.Sm9 | Column.Md10));
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        [Location("人员", Icon = FontAwesome.Male)]
        public ActionResult Male()
        {
            return Content("人员");
        }

        [Location("邮件", Icon = FontAwesome.Envelope, Behave = LinkBehave.Open)]
        public ActionResult Envelope()
        {
            return Content("邮件");
        }
    }
}