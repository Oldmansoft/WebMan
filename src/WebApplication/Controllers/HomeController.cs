﻿using Oldmansoft.Html;
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
            var document = new ManageDocument(Url.Location(Welcome));
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource("//cdn.jsdelivr.net/gh/Oldmansoft/WebMan/src/WebApplicationCore/wwwroot/js/oldmansoft-webman.cn.js"));
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource("//cdn.jsdelivr.net/npm/bootstrapvalidator@0.5.4/src/js/language/zh_CN.min.js"));

            document.Resources.AddLink(new Oldmansoft.Html.Element.Link("//cdn.jsdelivr.net/npm/lity@2.4.1/dist/lity.min.css"));
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource("//cdn.jsdelivr.net/npm/lity@2.4.1/dist/lity.min.js"));

            document.Resources.Markdown.Enabled = true;
            document.Resources.Select2.Enabled = true;
            document.Title = "WebMan";
            document.Menu.Add(new TreeListItem(Url.Location<DataTablesController>(o => o.Index)));
            document.Menu.Add(new TreeListItem(Url.Location<DataTablesController>(o => o.StaticIndex)));
            document.Menu.Add(
                new TreeListItem("一级菜单", FontAwesome.Suitcase)
                .Add(
                    new TreeListItem(Url.Location(Male))
                )
                .Add(
                    new TreeListItem("二级菜单", FontAwesome.Home)
                    .Add(
                        new TreeListItem(Url.Location<StepController>(o => o.Index))
                    )
                )
                .Add(
                    new TreeListItem(Url.Location(Envelope))
                )
            ).Add(new TreeListItem(Url.Location(MoreLevel)));

            document.Taskbar.Add(Url.Location(Male));
            document.Taskbar.Add(Url.Location(Envelope));

            document.Quick.Avatar.Photo = "https://avatars.githubusercontent.com/u/1279501";
            document.Quick.Avatar.Display = "Oldman";
            document.Quick.Add(Url.Location(Male));
            document.Quick.Add(Url.Location(Logoff));

            document.SetQuickSearch(Url.Location(Search));
            document.AddScript(new Badge(Url.Location<StepController>(o => o.Index), 1));
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
        public ActionResult Search(string key)
        {
            return Content("Search" + key);
        }
        
        public ActionResult Welcome()
        {
            return Content("welcome");
        }

        [Location("人员", Icon = FontAwesome.Male)]
        public ActionResult Male()
        {
            return new HtmlResult(new Badge(Url.Location(Male), 0));
        }

        [Location("邮件", Icon = FontAwesome.Envelope, Behave = LinkBehave.Open)]
        public ActionResult Envelope()
        {
            return Content("邮件");
        }

        private static Models.MoreLevelModel LevelModel = new Models.MoreLevelModel();

        [Location("多层对象", Icon = FontAwesome.Envelope)]
        public ActionResult MoreLevel()
        {
            var model = LevelModel;
            var form = FormHorizontal.Define(model, Url.Location(new Func<Models.MoreLevelModel, JsonResult>(MoreLevelResult))).Create();
            var panel = new Panel();
            panel.Append(form);
            panel.ConfigLocation();
            return new HtmlResult(panel.CreateGrid());
        }

        public JsonResult MoreLevelResult(Models.MoreLevelModel model)
        {
            LevelModel = model;
            return Json(DealResult.Location(Url.Location(Male)));
        }

        public ActionResult Test()
        {
            var html = new HtmlElement(HtmlTag.Html);
            var body = new HtmlElement(HtmlTag.Body);
            html.Append(body);
            body.Text("hello, world");
            return new HtmlResult(html);
        }
    }
}