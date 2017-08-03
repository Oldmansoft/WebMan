using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class StepController : Controller
    {
        [Location("第一步", Icon = FontAwesome.Table)]
        public ActionResult Index()
        {
            var model = new StepIndexModel();

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, Url.Location(new Func<StepIndexModel, ActionResult>(Next)));
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        [Location("下一步", Icon = FontAwesome.Table)]
        public ActionResult Next(StepIndexModel model)
        {
            var nextModel = new StepNextModel();
            nextModel.Name = model.Name;

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(nextModel, Url.Location(new Func<StepNextModel, ActionResult>(Finish)));
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        [Location("完成", Icon = FontAwesome.Table)]
        public ActionResult Finish(StepNextModel model)
        {
            var finishModel = new StepFinishModel();
            finishModel.Name = Request.Form["Name"];
            finishModel.Content = model.Content;

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(finishModel);
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }
    }
}