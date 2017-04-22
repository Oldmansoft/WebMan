using Oldmansoft.ClassicDomain.Util;
using Oldmansoft.Html.Mvc;
using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class DataTablesController : Controller
    {

        private static List<Models.DataTableItemModel> DataSource { get; set; }

        private static List<Models.DataTableItemModel> GetDataSource()
        {
            if (DataSource != null) return DataSource;
            var list = new List<Models.DataTableItemModel>();
            for (var i = 0; i < 100; i++)
            {
                var item = new Models.DataTableItemModel() { Name = "Hello" + (i + 1), IsGood = true, Content = "### heading text" };
                item.Id = i + 1;
                item.States = new List<Models.DataTableItemState>();
                item.States.Add(Models.DataTableItemState.Low);
                item.States.Add(Models.DataTableItemState.Hight);
                item.Time = DateTime.Now;
                item.Date = DateTime.Now;
                item.CreateTime = DateTime.UtcNow;
                list.Add(item);
            }
            DataSource = list;
            return DataSource;
        }

        public ActionResult Index()
        {
            var panel = new Panel();
            panel.Caption = "表格";
            panel.Icon = FontAwesome.Tablet;

            var table = DataTable.Definition<Models.DataTableItemModel>(o => o.Id, Url.Location(IndexDataSource));
            table.AddActionTable("添加", Url.Location(Create), LinkBehave.Open);
            table.AddActionTable("删除", Url.Location(Delete), LinkBehave.Call).SupportParameter().Confirm("是否删除").NeedSelected();
            table.AddActionItem("查看", Url.Location(Details), LinkBehave.Open);
            table.AddActionItem("修改", Url.Location(Edit), LinkBehave.Link);
            table.AddActionItem("删除", Url.Location(Delete), LinkBehave.Call).Confirm("是否删除");
            panel.Append(table);
            return new HtmlResult(panel.CreateGrid());
        }

        public JsonResult IndexDataSource(DataTableRequest request)
        {
            var list = GetDataSource().Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return Json(DataTable.Source(list, request, GetDataSource().Count));
        }

        public ActionResult Create()
        {
            var model = new Models.DataTableItemModel();
            model.States = new List<Models.DataTableItemState>();
            model.States.Add(Models.DataTableItemState.Hight);
            model.Time = DateTime.Now;
            model.Date = DateTime.Now;
            model.CreateTime = DateTime.UtcNow;

            var source = new ListDataSource();
            source["Age"].Add(new ListDataItem("1", "1"));
            source["Age"].Add(new ListDataItem("2", "2"));

            var panel = new Panel();
            panel.Caption = "hello";
            panel.Icon = FontAwesome.Anchor;
            var form = FormHorizontal.Create(model, Url.Location(new Func<Models.DataTableItemModel, JsonResult>(Create)), source);
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        [HttpPost]
        public JsonResult Create(Models.DataTableItemModel model)
        {
            if (ModelState.ValidateFail())
            {
                return Json(DealResult.CreateWrong(ModelState.ValidateMessage()));
            }
            model.Id = GetDataSource().Max(o => o.Id) + 1;
            GetDataSource().Insert(0, model);
            return Json(DealResult.Location(Url.Location(new Func<ActionResult>(Index)), "添加成功"));
        }

        public ActionResult Edit(int selectedId)
        {
            var model = GetDataSource().FirstOrDefault(o => o.Id == selectedId);
            var source = new ListDataSource();
            source["Age"].Add(new ListDataItem("1", "1"));
            source["Age"].Add(new ListDataItem("2", "2"));

            var panel = new Panel();
            panel.Caption = "hello";
            panel.Icon = FontAwesome.Anchor;
            var form = FormHorizontal.Create(model, Url.Location(new Func<Models.DataTableItemModel, JsonResult>(Edit)), source);
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        [HttpPost]
        public JsonResult Edit(Models.DataTableItemModel model)
        {
            if (ModelState.ValidateFail())
            {
                return Json(DealResult.CreateWrong(ModelState.ValidateMessage()));
            }
            var data = GetDataSource().FirstOrDefault(o => o.Id == model.Id);
            if (data != null)
            {
                model.CopyTo(data);
            }

            return Json(DealResult.Refresh("修改成功"));
        }

        public JsonResult Delete(params int[] selectedId)
        {
            foreach (var id in selectedId)
            {
                var model = GetDataSource().FirstOrDefault(o => o.Id == id);
                if (model == null) return Json(DealResult.CreateWrong("没有删除项"), JsonRequestBehavior.AllowGet);
                GetDataSource().Remove(model);
            }

            return Json(DealResult.Refresh("删除成功"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int selectedId)
        {
            var model = GetDataSource().FirstOrDefault(o => o.Id == selectedId);

            var panel = new Panel();
            panel.Caption = "hello";
            panel.Icon = FontAwesome.Anchor;
            var form = FormHorizontal.Create(model);
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }
    }
}