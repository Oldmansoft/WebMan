using Oldmansoft.ClassicDomain.Util;
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
                var item = new Models.DataTableItemModel();
                item.Id = i + 1;
                item.Name = "Hello" + (i + 1);
                item.Password = "^_^";
                item.Content = "### heading text";
                item.States = new List<Models.DataTableItemState>();
                item.States.Add(Models.DataTableItemState.Low);
                item.States.Add(Models.DataTableItemState.Hight);
                item.Time = DateTime.Now;
                item.Date = DateTime.Now;
                item.CreateTime = DateTime.UtcNow;
                item.File = new HttpPostedFileCustom("file.jpg", "image/jpg", "http://oldman.im/Content/Images/head.jpg");
                list.Add(item);
            }
            DataSource = list;
            return DataSource;
        }

        [Location("表格", Icon = FontAwesome.Tablet)]
        public ActionResult Index()
        {
            var panel = new Panel();
            panel.ConfigLocation();

            var table = DataTable.Definition<Models.DataTableItemModel>(o => o.Id, Url.Location(IndexDataSource));
            table.AddActionTable(Url.Location(Create));
            table.AddActionTable(Url.Location(Delete)).SupportParameter().Confirm("是否删除").NeedSelected();
            table.AddActionItem(Url.Location(Details)).OnClientCondition(ItemActionClient.Hide, "data.Id < 3").OnClientCondition(ItemActionClient.Disable, "data.Id > 5");
            table.AddActionItem(Url.Location(Edit));
            table.AddActionItem(Url.Location(Delete)).Confirm("是否删除");
            table.AddActionItem(Url.Location<DataTablesItemController>(o => o.Index));
            panel.Append(table);
            return new HtmlResult(panel.CreateGrid());
        }

        public JsonResult IndexDataSource(DataTableRequest request)
        {
            var list = GetDataSource().Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return Json(DataTable.Source(list, request, GetDataSource().Count));
        }

        [Location("添加", Icon = FontAwesome.Anchor, Behave = LinkBehave.Open)]
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
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, Url.Location(new Func<Models.DataTableItemModel, JsonResult>(Create)), source);
            panel.Append(form);

            return new HtmlResult(panel);
        }

        [HttpPost]
        public JsonResult Create(Models.DataTableItemModel model)
        {
            if (ModelState.ValidateFail())
            {
                return Json(DealResult.Wrong(ModelState.ValidateMessage()));
            }
            model.Id = GetDataSource().Max(o => o.Id) + 1;
            model.DealUpload((file) =>
            {
                model.File = new HttpPostedFileCustom(file.FileName, file.ContentType, "");
            }, o => o.File);
            GetDataSource().Insert(0, model);
            return Json(DealResult.Location(Url.Location(Index), "添加成功"));
        }

        [Location("修改", Icon = FontAwesome.Anchor)]
        public ActionResult Edit(int selectedId)
        {
            var model = GetDataSource().FirstOrDefault(o => o.Id == selectedId);
            var source = new ListDataSource();
            source["Age"].Add(new ListDataItem("1", "1"));
            source["Age"].Add(new ListDataItem("2", "2"));

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, Url.Location(new Func<Models.DataTableItemModel, JsonResult>(Edit)), source);
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        [HttpPost]
        public JsonResult Edit(Models.DataTableItemModel model)
        {
            if (ModelState.ValidateFail())
            {
                return Json(DealResult.Wrong(ModelState.ValidateMessage()));
            }
            var data = GetDataSource().FirstOrDefault(o => o.Id == model.Id);
            if (data != null)
            {
                var mapper = new DataMapper();
                mapper.SetIgnore<Models.DataTableItemModel>().Add(o => o.File);
                mapper.CopyTo(model, data);
                model.DealUpload((file) =>
                {
                    data.File = new HttpPostedFileCustom(file.FileName, file.ContentType, "");
                }, () =>
                {
                    data.File = null;
                }, o => o.File);
            }

            return Json(DealResult.Refresh("修改成功"));
        }

        [Location("删除")]
        public JsonResult Delete(params int[] selectedId)
        {
            foreach (var id in selectedId)
            {
                var model = GetDataSource().FirstOrDefault(o => o.Id == id);
                if (model == null) return Json(DealResult.Wrong("没有删除项"), JsonRequestBehavior.AllowGet);
                GetDataSource().Remove(model);
            }

            return Json(DealResult.Refresh("删除成功"), JsonRequestBehavior.AllowGet);
        }

        [Location("详情", Icon = FontAwesome.Anchor, Behave = LinkBehave.Open)]
        public ActionResult Details(int selectedId)
        {
            var model = GetDataSource().FirstOrDefault(o => o.Id == selectedId);

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model);
            panel.Append(form);

            return new HtmlResult(panel);
        }
    }
}