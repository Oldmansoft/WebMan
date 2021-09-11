using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Models;

namespace WebApplicationCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var document = new ManageDocument(Url.Location(Empty));
            document.Resources.WebMan.Script.Src = Url.Content("~/js/oldmansoft-webman.js");
            document.Resources.WebMan.Link.Href = Url.Content("~/css/oldmansoft-webman.css");
            document.Resources.PluginFix.Script.Src = Url.Content("~/js/oldmansoft-plugin-fix.js");
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/js/oldmansoft-webman.cn.js")));
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource("//cdn.jsdelivr.net/npm/bootstrapvalidator@0.5.4/src/js/language/zh_CN.min.js"));
            document.Resources.WebApp.Script = new Oldmansoft.Html.Element.ScriptResource("https://cdn.jsdelivr.net/gh/Oldmansoft/webapp/dist/oldmansoft-webapp.js");

            document.Title = "WebMan";
            document.Menu.Add(new TreeListItem(Url.Location<Areas.Manage.Controllers.TableController>(o => o.Index)));
            return new HtmlResult(document);
        }

        public IActionResult Login()
        {
            var document = new LoginDocument(Url.Location(Seed), Url.Location(new Func<string, string, JsonResult>(Login)));
            document.Resources.WebMan.Link.Href = Url.Content("~/css/oldmansoft-webman.css");
            document.Resources.WebMan.Script.Src = Url.Content("~/js/oldmansoft-webman.js");
            document.Resources.PluginFix.Script.Src = Url.Content("~/js/oldmansoft-plugin-fix.js");
            document.Title = "登录";
            var form = (Oldmansoft.Html.IHtmlElement)document.LoginPanel.Body.Children().First();
            form.Prepend(new Oldmansoft.Html.HtmlElement(Oldmansoft.Html.HtmlTag.Input).Attribute(Oldmansoft.Html.HtmlAttribute.Type, "hidden"));
            return new HtmlResult(document);
        }

        [AllowAnonymous]
        public IActionResult Seed()
        {
            string seed = Guid.NewGuid().ToString().Substring(0, 4);
            TempData["HashSeed"] = seed;
            return Content(seed);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(string account, string hash)
        {
            if (!(TempData["HashSeed"] is string hashSeed))
            {
                return Json(DealResult.Wrong("脚本运行不正确"));
            }
            if (account == "test")
            {
                var claims = new List<System.Security.Claims.Claim>()
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "test"),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, Guid.Empty.ToString("N")),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Test"),
                };
                var identity = new System.Security.Claims.ClaimsIdentity(claims, "ApplicationCookie");
                HttpContext.SignInAsync(new System.Security.Claims.ClaimsPrincipal(new[] { identity }));
                return Json(DealResult.Location(Url.Location<HomeController>(o => o.Index)));
            }
            else
            {
                return Json(DealResult.Wrong("帐号或密码错误"));
            }
        }

        public IActionResult Empty()
        {
            return new EmptyResult();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
