using Microsoft.AspNetCore.Mvc;
using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var document = new ManageDocument(Url.Location(Empty));
            document.Resources.JQuery.Script = new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/lib/jquery/dist/jquery.min.js"));
            document.Resources.WebApp.Script.Src = Url.Content("~/js/oldmansoft-webapp.js");
            document.Resources.WebApp.Link.Href = Url.Content("~/css/oldmansoft-webapp.css");
            document.Resources.WebMan.Script.Src = Url.Content("~/js/oldmansoft-webman.js");
            document.Resources.WebMan.Link.Href = Url.Content("~/css/oldmansoft-webman.css");
            document.Resources.PluginFix.Script.Src = Url.Content("~/js/oldmansoft-plugin-fix.js");
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/js/oldmansoft-webman.cn.js")));

            document.Title = "WebMan";
            document.Menu.Add(new TreeListItem(Url.Location<TableController>(o => o.Index)));
            return new HtmlResult(document);
        }

        public IActionResult Empty()
        {
            return new EmptyResult();
        }

        public IActionResult Test()
        {
            var content = Url.Action("Test", "Home", new { area = "" });

            return Content(content);
        }
    }
}
