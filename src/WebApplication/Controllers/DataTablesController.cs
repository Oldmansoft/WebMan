using Oldmansoft.ClassicDomain;
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
                item.CreateTime = DateTime.UtcNow;
                item.File = new HttpPostedFileCustom("file.jpg", "image/jpg", "https://avatars.githubusercontent.com/u/1279501");
                item.Files = new List<HttpPostedFileBase>();
                item.Files.Add(new HttpPostedFileCustom("file1.jpg", "image/jpg", "https://avatars.githubusercontent.com/u/1279501"));
                item.Files.Add(new HttpPostedFileCustom("file2.jpg", "image/jpg", "https://avatars.githubusercontent.com/u/1279501"));
                if (i == 0)
                {
                    item.Tags = new List<string>();
                    item.Tags.Add("hello");
                    item.Tags.Add("world");
                    item.Tags.Add("hello");
                }
                item.Sub = new Models.SubClass() { First = "1", Second = "2" };
                list.Add(item);
            }
            DataSource = list;
            return DataSource;
        }

        [Location("动态表格", Icon = FontAwesome.Tablet)]
        public ActionResult Index()
        {
            var panel = new Panel();
            panel.ConfigLocation();

            var table = DataTable.Define<Models.DataTableItemModel>(o => o.Id).Create(Url.Location(IndexDataSource));
            table.SetSelectedParameterName("id");
            table.AddActionTable(Url.Location(Create));
            table.AddActionTable(Url.Location(Delete)).SupportParameter().Confirm("是否删除").NeedSelected();
            table.AddActionTable("提示", "$app.alert(id)");
            table.AddActionItem(Url.Location(Details)).OnClientCondition(ItemActionClient.Hide, "data.Id < 3").OnClientCondition(ItemActionClient.Disable, "data.Id > 5");
            table.AddActionItem(Url.Location(Edit));
            table.AddActionItem(Url.Location(Delete)).Confirm("是否删除");
            table.AddActionItem(Url.Location<DataTablesItemController>(o => o.Index));
            table.AddActionItem("提示", "$app.alert(id)");
            table.AddActionItem(Url.Location(Show));
            table.AddActionItem(Url.Location(ShowAction));

            table.SetRowClassNameWhenClientCondition("alert-danger", "data.Id < 3");
            table.SetPageSize(20);
            table.AddSearchPanel(Url.Location(Index), "key", "");
            panel.Append(table);
            var result = new HtmlResult(panel.CreateGrid());
            result.SetQuickSearch(Url.Location(Index));
            return result;
        }
        
        public JsonResult IndexDataSource(DataTableRequest request)
        {
            var list = GetDataSource().Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return Json(DataTable.Source(list, request, GetDataSource().Count));
        }

        [Location("静态表格", Icon = FontAwesome.Tablet)]
        public ActionResult StaticIndex()
        {
            var panel = new Panel();
            panel.ConfigLocation();

            var table = DataTable.Define<Models.DataTableItemModel>(o => o.Id).Create(GetDataSource());
            table[o => o.Id].Visible = true;
            table.SetSelectedParameterName("id");
            table.AddActionTable(Url.Location(Create));
            table.AddActionTable(Url.Location(Delete)).SupportParameter().Confirm("是否删除").NeedSelected();
            table.AddActionTable("提示", "$app.alert(id)");
            table.AddActionItem(Url.Location(Details)).OnClientCondition(ItemActionClient.Hide, o => o.Id < 3).OnClientCondition(ItemActionClient.Disable, o => o.Id > 5);
            table.AddActionItem(Url.Location(Edit));
            table.AddActionItem(Url.Location(Delete)).Confirm("是否删除");
            table.AddActionItem(Url.Location<DataTablesItemController>(o => o.Index));
            table.AddActionItem("提示", "$app.alert(id)");

            table.SetRowClassNameWhenCondition("alert-danger", o => o.Id < 3);
            panel.Append(table);
            return new HtmlResult(panel.CreateGrid());
        }

        private ListDataSource GetListSource()
        {
            var source = new ListDataSource();
            source["Age"].Add(new ListDataItem("1", "1"));
            source["Age"].Add(new ListDataItem("2", "2"));
            return source;
        }

        [Location("添加", Icon = FontAwesome.Anchor, Behave = LinkBehave.Open)]
        public ActionResult Create()
        {
            var model = new Models.DataTableItemModel();
            model.CreateTime = DateTime.UtcNow;
            
            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, Url.Location(new Func<Models.DataTableItemModel, JsonResult>(Create)), GetListSource());
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
            var data = new Models.DataTableItemModel();
            model.MapTo(data);

            data.Id = GetDataSource().Max(o => o.Id) + 1;
            model.DealUpload((file) =>
            {
                data.File = new HttpPostedFileCustom(file.FileName, file.ContentType, "");
            }, o => o.File);
            data.Files = new List<HttpPostedFileBase>();
            model.DealUpload((file) =>
            {
                data.Files.Add(new HttpPostedFileCustom(file.FileName, file.ContentType, ""));
            }, o => o.Files);
            GetDataSource().Insert(0, data);
            return Json(DealResult.Location(Url.Location(Index), "添加成功"));
        }

        [Location("修改", Icon = FontAwesome.Anchor)]
        public ActionResult Edit(int id)
        {
            var model = GetDataSource().FirstOrDefault(o => o.Id == id);
            var panel = new Panel();
            panel.Description = "hello, world";
            panel.ConfigLocation();
            var form = FormHorizontal.Define(model, Url.Location(new Func<Models.DataTableItemModel, JsonResult>(EditResult)), GetListSource());
            panel.Append(form.Create());

            return new HtmlResult(panel.CreateGrid());
        }

        [HttpPost]
        public JsonResult EditResult(Models.DataTableItemModel model)
        {
            if (ModelState.ValidateFail())
            {
                return Json(DealResult.Wrong(ModelState.ValidateMessage()));
            }
            var data = GetDataSource().FirstOrDefault(o => o.Id == model.Id);
            if (data != null)
            {
                model.MapTo(data);
                model.DealUpload((file) =>
                {
                    data.File = new HttpPostedFileCustom(file.FileName, file.ContentType, "");
                }, () =>
                {
                    data.File = null;
                }, o => o.File);
                model.DealUpload((file) =>
                {
                    data.Files.Add(new HttpPostedFileCustom(file.FileName, file.ContentType, ""));
                }, (index) =>
                {
                    data.Files.RemoveAt(index);
                }, o => o.Files);
            }
            return Json(DealResult.Refresh());
        }

        [Location("删除")]
        public JsonResult Delete(params int[] id)
        {
            foreach (var item in id)
            {
                var model = GetDataSource().FirstOrDefault(o => o.Id == item);
                if (model == null) return Json(DealResult.Wrong("没有删除项"), JsonRequestBehavior.AllowGet);
                GetDataSource().Remove(model);
            }

            return Json(DealResult.Refresh("删除成功"), JsonRequestBehavior.AllowGet);
        }

        [Location("详情", Icon = FontAwesome.Anchor, Behave = LinkBehave.Open)]
        public ActionResult Details(int id)
        {
            var model = GetDataSource().FirstOrDefault(o => o.Id == id);

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, GetListSource());
            panel.Append(form);

            return new HtmlResult(panel);
        }

        [Location("显示", Behave = LinkBehave.Open)]
        public ActionResult Show(int id)
        {
            var data = GetDataSource().FirstOrDefault(o => o.Id == id);

            var model = data.MapTo(new Models.ShowModel());
            model.Id = data.Id;
            model.Name = data.Name;
            model.File = new HttpPostedFileCustom("file.jpg", "image/jpg", "https://avatars.githubusercontent.com/u/1279501");
            model.Files = new List<HttpPostedFileBase>();
            model.Files.Add(new HttpPostedFileCustom("file1.jpg", "image/jpg", "https://avatars.githubusercontent.com/u/1279501"));
            model.Files.Add(new HttpPostedFileCustom("file2.jpg", "image/jpg", "https://avatars.githubusercontent.com/u/1279501"));
            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, Url.Location(new Func<Models.ShowModel, JsonResult>(ShowResult)), GetListSource());
            panel.Append(form);

            return new HtmlResult(panel);
        }

        public JsonResult ShowResult(Models.ShowModel model)
        {
            var data = GetDataSource().FirstOrDefault(o => o.Id == model.Id);
            if (data == null) return Json(DealResult.WrongRefresh("无效操作"));
            return Json(DealResult.Show(string.Format("显示 {0}", data.Name)));
        }

        [Location("动作")]
        public JsonResult ShowAction(int id)
        {
            return Json(DealResult.Show(string.Format("显示 {0}", id)));
        }
    }
}