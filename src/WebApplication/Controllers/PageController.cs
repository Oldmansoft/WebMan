using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Index()
        {
            var document = new Oldmansoft.Html.WebMan.Document.SimpleDocument(Url.Location(IndexLink));
            document.Resources.Markdown.Enabled = true;
            document.Resources.Select2.Enabled = true;
            return new HtmlResult(document);
        }

        public ActionResult IndexLink()
        {
            var model = new Models.DataTableItemModel();
            model.CreateTime = DateTime.UtcNow;

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, Url.Location(new Func<Models.DataTableItemModel, JsonResult>(Create)));
            panel.Append(form);

            return new HtmlResult(panel);
        }

        [HttpPost]
        public JsonResult Create(Models.DataTableItemModel model)
        {
            return Json(DealResult.Refresh("添加成功"));
        }
    }
}