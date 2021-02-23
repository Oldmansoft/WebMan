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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
